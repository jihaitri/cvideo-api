using AutoMapper;
using CVideoAPI.Datasets.Account;
using CVideoAPI.Datasets.Application;
using CVideoAPI.Datasets.Company;
using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.CV.Section;
using CVideoAPI.Datasets.CV.Section.Field;
using CVideoAPI.Datasets.CV.SectionType;
using CVideoAPI.Datasets.Employee;
using CVideoAPI.Datasets.Major;
using CVideoAPI.Datasets.NewsFeedSection;
using CVideoAPI.Datasets.Question;
using CVideoAPI.Datasets.Recruitment;
using CVideoAPI.Datasets.Video;
using CVideoAPI.Models;

namespace CVideoAPI.Datasets
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Account, AccountDataset>();
            CreateMap<Models.Employee, EmployeeDataset>()
                .ForMember(dest => dest.Email, src => src.MapFrom(emp => emp.Account.Email));
            CreateMap<RecruitmentPost, CommonRecruitmentPostDataset>();
            CreateMap<Models.Company, CompanyDataset>()
                .ForMember(dest => dest.Id, src => src.MapFrom(company => company.AccountId))
                .ForMember(dest => dest.Name, src => src.MapFrom(company => company.CompanyName))
                .ForMember(dest => dest.Email, src => src.MapFrom(company => company.Account.Email));
            CreateMap<Models.Major, MajorDataset>();
            CreateMap<Models.NewsFeedSection, NewsFeedSectionDataset>();
            CreateMap<Models.CV, CVDataset>();
            CreateMap<Models.Video, VideoDataset>()
                .ForMember(dest => dest.CVId, src => src.MapFrom(video => video.Section.CVId));
            CreateMap<Models.Style, VideoStyleDataset>();
            CreateMap<Models.Section, CVSectionDataset>()
                .ForMember(dest => dest.Title, src => src.MapFrom(section => section.DisplayTitle))
                .ForMember(dest => dest.Icon, src => src.MapFrom(section => section.SectionType.Image))
                .ForMember(dest => dest.Fields, src => src.MapFrom(section => section.SectionFields));
            CreateMap<Models.SectionType, CVSectionTypeDataset>()
                .ForMember(dest => dest.Icon, src => src.MapFrom(type => type.Image));
            CreateMap<Models.SectionField, CVFieldDataset>();
            CreateMap<Models.Question, QuestionDataset>()
                .ForMember(des => des.Modified, src => src.MapFrom(q => q.LastUpdated));
            CreateMap<Models.QuestionSet, QuestionSetDataset>()
                .ForMember(des => des.Modified, src => src.MapFrom(q => q.LastUpdated));
            CreateMap<Models.Style, VideoStyleDataset>();
            CreateMap<Models.RecruitmentPost, PostStaticsDataset>();
            CreateMap<Models.Application, ApplicationDataset>();
        }
    }
}
