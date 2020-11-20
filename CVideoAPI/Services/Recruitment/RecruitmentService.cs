using AutoMapper;
using CVideoAPI.Datasets;
using CVideoAPI.Datasets.Application;
using CVideoAPI.Datasets.Company;
using CVideoAPI.Datasets.CV.Section;
using CVideoAPI.Datasets.CV.Section.Field;
using CVideoAPI.Datasets.Recruitment;
using CVideoAPI.Datasets.Video;
using CVideoAPI.Models;
using CVideoAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Recruitment
{
    public class RecruitmentService : IRecruitmentService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public RecruitmentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<bool> ApplyCV(int postId, int userId, int cvId)
        {
            if (!await IsUserApplied(postId, userId))
            {
                _uow.AppliedCVRepository.Insert(new Application()
                {
                    CVId = cvId,
                    PostId = postId,
                    Viewed = false
                });
                Models.CV cv = await _uow.CVRepository.GetById(cvId);
                RecruitmentPost post = await _uow.RecruitmentRepository.GetById(postId);
                if (await _uow.AccessKeyRepository.GetFirst(filter: key => key.DataKey == cv.DataKey && key.UserId == post.CompanyId) == null ) {
                    _uow.AccessKeyRepository.Insert(new AccessKey()
                    {
                        DataKey = cv.DataKey,
                        UserId = post.CompanyId
                    });
                }
                Models.Employee employee = await _uow.EmployeeRepository.GetById(userId);
                if (await _uow.AccessKeyRepository.GetFirst(filter: key => key.DataKey == employee.DataKey && key.UserId == post.CompanyId) == null) {
                    _uow.AccessKeyRepository.Insert(new AccessKey()
                    {
                        DataKey = employee.DataKey,
                        UserId = post.CompanyId
                    });
                }
            }
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> IsUserApplied(int postId, int userId)
        {
            return await _uow.AppliedCVRepository.GetFirst(filter: item => item.PostId == postId && item.CV.EmployeeId == userId,
                                                    includeProperties: "CV") != null;
        }

        public async Task<List<CommonRecruitmentPostDataset>> GetListRecruitmentPost(RecruitmentPostRequestParam param)
        {
            Expression<Func<Models.RecruitmentPost, bool>> filter = post => (post.Company.CompanyName.Contains(param.CompanyName)
                                                                                && post.Location.Contains(param.Location)
                                                                                && (param.CompanyId == 0 || post.CompanyId == param.CompanyId)
                                                                                && (param.MajorId == 0 || post.MajorId == param.MajorId)
                                                                                && (param.IsExpired || post.DueDate >= DateTime.UtcNow));
            Func<IQueryable<RecruitmentPost>, IOrderedQueryable<RecruitmentPost>> order;
            switch (param.OrderBy)
            {
                case "+created":
                    order = post => post.OrderBy(post => post.Created);
                    break;
                case "+salary":
                    order = post => post.OrderBy(post => post.MaxSalary);
                    break;
                case "-salary":
                    order = post => post.OrderByDescending(post => post.MaxSalary);
                    break;
                default:
                    order = post => post.OrderByDescending(post => post.Created);
                    break;
            }
            IEnumerable<Models.RecruitmentPost> list = await _uow.RecruitmentRepository.Get(filter: filter,
                                                                                            offset: param.Offset,
                                                                                            first: param.Limit,
                                                                                            includeProperties: "Company,Company.Account",
                                                                                            orderBy: order);
            IEnumerable<Translation> trans = await _uow.TranslationRepository.Get(filter: t => t.MajorId != null && t.Language == param.Lang);
            List<CommonRecruitmentPostDataset> result = (from r in list
                                                   join t in trans on r.MajorId equals t.MajorId
                                                   select new CommonRecruitmentPostDataset()
                                                   {
                                                       PostId = r.PostId,
                                                       Company = _mapper.Map<CompanyDataset>(r.Company),
                                                       Created = r.Created,
                                                       DueDate = r.DueDate,
                                                       ExpectedNumber = r.ExpectedNumber,
                                                       JobBenefit = r.JobBenefit,
                                                       JobDescription = r.JobDescription,
                                                       JobRequirement = r.JobRequirement,
                                                       Location = r.Location,
                                                       Major = new Datasets.Major.MajorDataset()
                                                       {
                                                           MajorId = r.MajorId,
                                                           MajorName = t.Text
                                                       },
                                                       MaxSalary = r.MaxSalary,
                                                       MinSalary = r.MinSalary,
                                                       Title = r.Title
                                                   }).ToList();
            return result;
        }
        public async Task<CommonRecruitmentPostDataset> GetRecruitmentPostById(int id, string lang = "vi")
        {
            CommonRecruitmentPostDataset result = null;
            RecruitmentPost post = await _uow.RecruitmentRepository.GetFirst(filter: post => post.PostId == id, includeProperties: "Company,Company.Account");
            if (post != null)
            {
                result = _mapper.Map<CommonRecruitmentPostDataset>(post);
                result.Major = new Datasets.Major.MajorDataset()
                {
                    MajorId = post.MajorId,
                    MajorName = (await _uow.TranslationRepository.GetFirst(filter: t => t.MajorId == post.MajorId && t.Language == lang)).Text
                };
            }
            return result;
        }

        public async Task<CommonRecruitmentPostDataset> InsertRecruitmentPost(NewRecruitmentPostParam param)
        {
            RecruitmentPost post = new RecruitmentPost()
            {
                Title = param.Title,
                CompanyId = param.CompanyId,
                DueDate = param.DueDate,
                ExpectedNumber = param.ExpectedNumber,
                JobBenefit = param.JobBenefit,
                JobDescription = param.JobDescription,
                JobRequirement = param.JobRequirement,
                Location = param.Location,
                MajorId = param.MajorId,
                MinSalary = param.MinSalary,
                MaxSalary = param.MaxSalary
            };
            _uow.RecruitmentRepository.Insert(post);
            if (await _uow.CommitAsync() > 0)
            {
                return await this.GetRecruitmentPostById(post.PostId);
            }
            return null;
        }

        public async Task<bool> UpdateRecruitmentPost(RecruitmentPostToUpdateParam param)
        {
            RecruitmentPost post = await _uow.RecruitmentRepository.GetFirst(filter: post => post.PostId == param.PostId);
            post.Title = param.Title;
            post.MinSalary = param.MinSalary;
            post.MajorId = param.MajorId;
            post.MaxSalary = param.MaxSalary;
            post.Location = param.Location;
            post.JobRequirement = param.JobRequirement;
            post.JobDescription = param.JobDescription;
            post.JobBenefit = param.JobBenefit;
            post.ExpectedNumber = param.ExpectedNumber;
            post.DueDate = param.DueDate;
            _uow.RecruitmentRepository.Update(post);
            if (await _uow.CommitAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<CommonRecruitmentPostDataset> GetRecruitmentPostByName(int compannyId, string title)
        {
            RecruitmentPost post = await _uow.RecruitmentRepository.GetFirst(filter: post => post.CompanyId == compannyId && post.Title == title);
            if (post != null)
            {
                return _mapper.Map<CommonRecruitmentPostDataset>(post);
            }
            return null;
        }

        public async Task<List<PostStaticsDataset>> GetPostStatisticsList(int companyId, CommonParam param)
        {
            Func<IQueryable<RecruitmentPost>, IOrderedQueryable<RecruitmentPost>> order;
            switch (param.OrderBy)
            {
                case "+created":
                    order = post => post.OrderBy(post => post.Created);
                    break;
                case "+salary":
                    order = post => post.OrderBy(post => post.MaxSalary);
                    break;
                case "-salary":
                    order = post => post.OrderByDescending(post => post.MaxSalary);
                    break;
                default:
                    order = post => post.OrderByDescending(post => post.Created);
                    break;
            }
            IEnumerable<RecruitmentPost> posts = await _uow.RecruitmentRepository.Get(filter: post => post.CompanyId == companyId,
                                                                                        first: param.Limit,
                                                                                        offset: param.Offset,
                                                                                        orderBy: order);
            IEnumerable<Translation> trans = await _uow.TranslationRepository.Get(filter: trans => trans.MajorId != null && trans.Language == param.Lang);
            IEnumerable<Application> applications = await _uow.AppliedCVRepository.Get(filter: a => a.RecruitmentPost.CompanyId == companyId, includeProperties: "RecruitmentPost");

            IEnumerable<PostStaticsDataset> result = from post in posts
                                               join tran in trans on post.MajorId equals tran.MajorId
                                               select new PostStaticsDataset()
                                               {
                                                   Created = post.Created,
                                                   MajorId = post.MajorId,
                                                   PostId = post.PostId,
                                                   DueDate = post.DueDate,
                                                   ExpectedNumber = post.ExpectedNumber,
                                                   Location = post.Location,
                                                   Major = new Datasets.Major.MajorDataset()
                                                   {
                                                       MajorId = post.MajorId,
                                                       MajorName = tran.Text
                                                   },
                                                   MaxSalary = post.MaxSalary,
                                                   MinSalary = post.MinSalary,
                                                   Title = post.Title,
                                                   TotalCVs = (from application in applications
                                                              where application.PostId == post.PostId
                                                              select application).ToList().Count(),
                                                   NewCVs = (from application in applications
                                                             where application.PostId == post.PostId && !application.Viewed
                                                             select application).ToList().Count()
                                               };
            return result.ToList();
        }

        public async Task<PostStaticsDataset> GetPostStatisticsById(int id, string lang = "vi")
        {
            RecruitmentPost post = await _uow.RecruitmentRepository.GetFirst(filter: post => post.PostId == id);
            PostStaticsDataset result = _mapper.Map<PostStaticsDataset>(post);
            result.TotalCVs = (await _uow.AppliedCVRepository.Get(filter: p => p.PostId == post.PostId)).ToList().Count;
            result.NewCVs = (await _uow.AppliedCVRepository.Get(filter: p => p.PostId == post.PostId && !p.Viewed)).ToList().Count;
            return result;
        }
        public async Task<List<ApplicationDataset>> GetAllAppliedCVs(int companyId, ApplicationParam param)
        {
            Func<IQueryable<Models.Application>, IOrderedQueryable<Models.Application>> order;
            switch (param.OrderBy)
            {
                case "+created":
                    order = a => a.OrderBy(a => a.Created);
                    break;
                default:
                    order = a => a.OrderByDescending(a => a.Created);
                    break;
            }
            IEnumerable<Application> list = await _uow.AppliedCVRepository.Get(filter: a => (a.RecruitmentPost.CompanyId == companyId) && (param.Viewed == null || a.Viewed == param.Viewed),
                                                                                                first: param.Limit,
                                                                                                offset: param.Offset,
                                                                                                orderBy: order,
                                                                                                includeProperties: "CV,CV.Employee,CV.Employee.Account,RecruitmentPost");
            return _mapper.Map<List<ApplicationDataset>>(list);
        }
        public async Task<List<ApplicationDataset>> GetAppliedCVs(int postId, ApplicationParam param)
        {
            Func<IQueryable<Models.Application>, IOrderedQueryable<Models.Application>> order;
            switch (param.OrderBy)
            {
                case "+created":
                    order = a => a.OrderBy(a => a.Created);
                    break;
                default:
                    order = a => a.OrderByDescending(a => a.Created);
                    break;
            }
            IEnumerable<Application> list = await _uow.AppliedCVRepository.Get(filter: a => (a.PostId == postId) && (param.Viewed == null || a.Viewed == param.Viewed),
                                                                                                first: param.Limit,
                                                                                                offset: param.Offset,
                                                                                                orderBy: order,
                                                                                                includeProperties: "CV,CV.Employee");
            return _mapper.Map<List<ApplicationDataset>>(list);
        }

        public async Task<ApplicationDataset> GetAppliedCV(int applicationId)
        {
            Application application = await _uow.AppliedCVRepository.GetFirst(filter: a => a.ApplicationId == applicationId, includeProperties: "CV,CV.Employee,CV.Employee.Account");
            ApplicationDataset result = _mapper.Map<ApplicationDataset>(application);
            IEnumerable<Section> sections = await _uow.SectionRepository.Get(filter: section => section.CVId == application.CVId, includeProperties: "SectionType");
            IEnumerable<SectionField> fields = await _uow.SectionFieldRepository.Get(filter: field => field.Section.CVId == application.CVId, includeProperties: "Section");
            IEnumerable<Models.Video> videos = await _uow.VideoRepository.Get(filter: video => video.Section.CVId == application.CVId, includeProperties: "Section,VideoStyle");
            result.CV.Sections = (from section in sections
                               select new CVSectionDataset()
                               {
                                   SectionId = section.SectionId,
                                   CVId = application.CVId,
                                   SectionTypeId = section.SectionTypeId,
                                   Text = section.Text,
                                   Title = section.DisplayTitle,
                                   Icon = section.SectionType.Image,
                                   Fields = (from field in fields
                                             where field.SectionId == section.SectionId
                                             select new CVFieldDataset()
                                             {
                                                 FieldId = field.FieldId,
                                                 Name = field.FieldTitle,
                                                 Text = field.Text
                                             }).ToList(),
                                   Videos = (from video in videos
                                             where video.SectionId == section.SectionId
                                             select new VideoDataset()
                                             {
                                                 AspectRatio = video.AspectRatio,
                                                 ThumbUrl = video.ThumbUrl,
                                                 VideoUrl = video.VideoUrl,
                                                 CoverUrl = video.CoverUrl,
                                                 VideoId = video.VideoId,
                                                 VideoStyle = new VideoStyleDataset()
                                                 {
                                                     StyleId = video.VideoStyle.StyleId,
                                                     StyleName = video.VideoStyle.StyleName
                                                 }
                                             }).ToList()
                               }).ToList();
        
            return result;
        }
    }
}
