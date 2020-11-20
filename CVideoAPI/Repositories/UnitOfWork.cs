using CVideoAPI.Context;
using CVideoAPI.Models;
using CVideoAPI.Repositories.Account;
using CVideoAPI.Repositories.Company;
using CVideoAPI.Repositories.CV;
using CVideoAPI.Repositories.Employee;
using CVideoAPI.Repositories.Recruitment;
using CVideoAPI.Repositories.Video;
using System;
using System.Threading.Tasks;

namespace CVideoAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CVideoContext _context;
        private bool disposed = false;
        private IAccountRepository _accountRepository;
        private IEmployeeRepository _employeeRepository;
        private ICompanyRepository _companyRepository;
        private IRecruitmentRepository _recruitmentRepository;
        private ICVRepository _cvRepository;
        private IVideoRepository _videoRepository;
        private IGenericRepository<NewsFeedSection> _newsFeedRepository;
        private IGenericRepository<Application> _appliedCVRepository;
        private IGenericRepository<Translation> _translationRepository;
        private IGenericRepository<Section> _sectionRepository;
        private IGenericRepository<SectionField> _sectionFieldRepository;
        private IGenericRepository<SectionType> _sectionTypeRepository;
        private IGenericRepository<UserDevice> _userDeviceRepository;
        private IGenericRepository<AccessKey> _accessKeyRepository;
        private IGenericRepository<Style> _styleRepository;
        private IGenericRepository<QuestionSet> _questionSetRepository;
        private IGenericRepository<Question> _questionRepository;
        private IGenericRepository<Major> _majorRepository;
        public UnitOfWork(CVideoContext context)
        {
            _context = context;
        }
        public IAccountRepository AccountRepository
        {
            get { return _accountRepository ??= new AccountRepository(_context); }
        }
        public IEmployeeRepository EmployeeRepository
        {
            get { return _employeeRepository ??= new EmployeeRepository(_context); }
        }
        public ICompanyRepository CompanyRepository
        {
            get { return _companyRepository ??= new CompanyRepository(_context); }
        }
        public IRecruitmentRepository RecruitmentRepository
        {
            get { return _recruitmentRepository ??= new RecruitmentRepository(_context); }
        }
        public ICVRepository CVRepository
        {
            get { return _cvRepository ??= new CVRepository(_context); }
        }
        public IVideoRepository VideoRepository
        {
            get { return _videoRepository ??= new VideoRepository(_context); }
        }
        public IGenericRepository<NewsFeedSection> NewsFeedRepository
        {
            get { return _newsFeedRepository ??= new GenericRepository<NewsFeedSection>(_context); }
        }
        public IGenericRepository<Application> AppliedCVRepository
        {
            get { return _appliedCVRepository ??= new GenericRepository<Application>(_context); }
        }
        public IGenericRepository<Translation> TranslationRepository
        {
            get { return _translationRepository ??= new GenericRepository<Translation>(_context); }
        }
        public IGenericRepository<Section> SectionRepository
        {
            get { return _sectionRepository ??= new GenericRepository<Section>(_context); }
        }
        public IGenericRepository<SectionField> SectionFieldRepository
        {
            get { return _sectionFieldRepository ??= new GenericRepository<SectionField>(_context); }
        }
        public IGenericRepository<SectionType> SectionTypeRepository
        {
            get { return _sectionTypeRepository ??= new GenericRepository<SectionType>(_context); }
        }
        public IGenericRepository<UserDevice> UserDeviceRepository
        {
            get { return _userDeviceRepository ??= new GenericRepository<UserDevice>(_context); }
        }
        public IGenericRepository<Style> StyleRepository
        {
            get { return _styleRepository ??= new GenericRepository<Style>(_context); }
        }
        public IGenericRepository<AccessKey> AccessKeyRepository
        {
            get { return _accessKeyRepository ??= new GenericRepository<AccessKey>(_context); }
        }
        public IGenericRepository<QuestionSet> QuestionSetRepository
        {
            get { return _questionSetRepository ??= new GenericRepository<QuestionSet>(_context); }
        }
        public IGenericRepository<Question> QuestionRepository
        {
            get { return _questionRepository ??= new GenericRepository<Question>(_context); }
        }
        public IGenericRepository<Major> MajorRepository
        {
            get { return _majorRepository ??= new GenericRepository<Major>(_context); }
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
