using CVideoAPI.Models;
using CVideoAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CVideoAPI.Context
{
    public class CVideoContext : DbContext
    {
        private readonly int _userId;
        public CVideoContext(DbContextOptions options, IGetClaimsProvider userData) : base(options)
        {
            _userId = userData.UserId;
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CV> CV { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<SectionType> SectionType { get; set; }
        public DbSet<SectionField> SectionField { get; set; }
        public DbSet<QuestionSet> QuestionSet { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<Style> Style { get; set; }
        public DbSet<Major> Major { get; set; }
        public DbSet<RecruitmentPost> RecruitmentPost { get; set; }
        public DbSet<UserDevice> UserDevice { get; set; }
        public DbSet<NewsFeedSection> NewsFeedSection { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<AccessKey> AccessKey { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>()
                .HasData
                (
                    new Role() { RoleId = 1, RoleName = "admin" },
                    new Role() { RoleId = 2, RoleName = "employee" },
                    new Role() { RoleId = 3, RoleName = "employer" }
                );
            modelBuilder.Entity<Translation>()
                .HasIndex(item => new { item.Language, item.NewsFeedSectionId })
                .IsUnique();
            modelBuilder.Entity<Translation>()
                .HasIndex(item => new { item.Language, item.SectionTypeId })
                .IsUnique();
            modelBuilder.Entity<CV>()
                .HasIndex(item => new { item.Title, item.EmployeeId })
                .IsUnique();
            modelBuilder.Entity<Application>()
                .HasAlternateKey(item => new { item.PostId, item.CVId });
            modelBuilder.Entity<Application>()
                .HasOne(a => a.CV)
                .WithMany(cv => cv.Applications)
                .HasForeignKey(a => a.CVId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Application>()
                .HasOne(a => a.RecruitmentPost)
                .WithMany(post => post.Applications)
                .HasForeignKey(a => a.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AccessKey>()
                .HasKey(item => new { item.DataKey, item.UserId });
            modelBuilder.Entity<Employee>()
                .HasQueryFilter(item => this.AccessKey.Where(key => item.DataKey == key.DataKey && _userId == key.UserId).First() != null);
            modelBuilder.Entity<CV>()
                .HasQueryFilter(item => this.AccessKey.Where(key => item.DataKey == key.DataKey && _userId == key.UserId).First() != null);
            modelBuilder.Entity<Section>()
                .HasQueryFilter(item => this.AccessKey.Where(key => item.CV.DataKey == key.DataKey && _userId == key.UserId).First() != null);
            modelBuilder.Entity<SectionField>()
                .HasQueryFilter(item => this.AccessKey.Where(key => item.Section.CV.DataKey == key.DataKey && _userId == key.UserId).First() != null);
            modelBuilder.Entity<Video>()
                .HasQueryFilter(item => this.AccessKey.Where(key => item.Section.CV.DataKey == key.DataKey && _userId == key.UserId).First() != null);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // set create, update date time
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));
            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).LastUpdated = DateTime.UtcNow;
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Created = DateTime.UtcNow;
                }
            }
            // check permission
            int userId = _userId;
            entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IProtectedAccess && (e.State == EntityState.Detached || e.State == EntityState.Modified || e.State == EntityState.Deleted));
            foreach (var entry in entries)
            {
                if (entry.Entity is IProtectedAccess e)
                {
                    if (AccessKey.Where(key => key.DataKey == e.DataKey && key.UserId == userId).FirstOrDefault() == null)
                    {
                        return Task.FromResult(0);
                    }
                }
            }
            entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IProtectedAccess && e.State == EntityState.Added);
            if (entries.ToList().Count > 0)
            {
                Guid dataKey = Guid.NewGuid();
                foreach (var entityEntry in entries)
                {
                    if (entityEntry.Entity is IProtectedAccess entityToMark)
                    {
                        entityToMark.SetDataKey(dataKey);
                    }
                }
                if (userId == 0)
                {
                    AccessKey.Add(new AccessKey()
                    {
                        DataKey = dataKey,
                        Account = (Models.Account)(ChangeTracker.Entries().Where(e => e.Entity is Account).First().Entity)
                    });
                }
                else
                {
                    AccessKey.Add(new AccessKey()
                    {
                        DataKey = dataKey,
                        UserId = userId
                    });
                }
            }
            return base.SaveChangesAsync();
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).LastUpdated = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Created = DateTime.UtcNow;
                }
            }
            // set owner key
            int userId = _userId;
            EntityEntry newAccount = null;
            if (userId == 0)
            {
                newAccount = ChangeTracker
                                        .Entries()
                                        .Where(e => e.Entity is Models.Account && e.State == EntityState.Added)
                                        .FirstOrDefault();
                if (newAccount.Entity is Account acc)
                {
                    userId = acc.AccountId;
                }
            }
            entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IProtectedAccess && e.State == EntityState.Added);
            if (entries.ToList().Count > 0)
            {
                Guid dataKey = Guid.NewGuid();
                foreach (var entityEntry in entries)
                {
                    if (entityEntry.Entity is IProtectedAccess entityToMark)
                    {
                        entityToMark.SetDataKey(dataKey);
                    }
                }
                if (userId == 0)
                {
                    AccessKey.Add(new AccessKey()
                    {
                        DataKey = dataKey,
                        Account = (Models.Account)newAccount.Entity
                    });
                }
                else
                {
                    AccessKey.Add(new AccessKey()
                    {
                        DataKey = dataKey,
                        UserId = userId
                    });
                }
            }
            return base.SaveChanges();
        }
    }
}
