using AutoMapper;
using CVideoAPI.Cache;
using CVideoAPI.Context;
using CVideoAPI.Helpers;
using CVideoAPI.Repositories;
using CVideoAPI.Services.Admin;
using CVideoAPI.Services.Authen;
using CVideoAPI.Services.BlobStorage;
using CVideoAPI.Services.Cache;
using CVideoAPI.Services.Company;
using CVideoAPI.Services.CV;
using CVideoAPI.Services.Employee;
using CVideoAPI.Services.Employer;
using CVideoAPI.Services.FCM;
using CVideoAPI.Services.Major;
using CVideoAPI.Services.NewsFeed;
using CVideoAPI.Services.Question;
using CVideoAPI.Services.Recruitment;
using CVideoAPI.Services.Video;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace CVideoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.GetApplicationDefault(),
            });
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Cors configure
            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    //.AllowCredentials();
                });
            });
            // Add jwt authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;

                    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtSecret"])),
                        ValidateIssuer = true,
                        ValidIssuer = AppSettings.Settings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AppSettings.Settings.Audience,
                        RequireExpirationTime = false
                    };
                });
            // DB configure
            services.AddDbContext<CVideoContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:CVideoDB"]));
            services.AddScoped<CVideoContext>();
            // set up redis cache
            var redisCacheSettings = new RedisCacheSettings();
            Configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);
            if (redisCacheSettings.Enabled)
            {
                services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
                services.AddSingleton<ICacheService, CacheService>();
            }
            // get config object
            AppConfig.SetConfig(Configuration);
            // create singleton context accessor
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IGetClaimsProvider, ClaimsProvider>();
            // Configure controller
            services.AddControllers();
            // Add unit of work scope
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Add services
            AddServicesScoped(services);
            // Auto mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Generate swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CVideo API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()}
                });

            });
            // API versioning
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
        }
        private void AddServicesScoped(IServiceCollection services)
        {
            services.AddScoped<IAuthenService, AuthenService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IRecruitmentService, RecruitmentService>();
            services.AddScoped<INewsFeedService, NewsFeedService>();
            services.AddScoped<ICVService, CVService>();
            services.AddScoped<IFCMService, FCMService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IMajorService, MajorService>();
            services.AddScoped<IEmployerService, EmployerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            app.UseCors("AllowAll");

            app.UseSwagger(option => { option.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CVideo API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
