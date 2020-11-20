using CVideoAPI.Models;
using CVideoAPI.Repositories.Account;
using CVideoAPI.Repositories.Company;
using CVideoAPI.Repositories.CV;
using CVideoAPI.Repositories.Employee;
using CVideoAPI.Repositories.Recruitment;
using CVideoAPI.Repositories.Video;
using System.Threading.Tasks;

namespace CVideoAPI.Repositories
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IRecruitmentRepository RecruitmentRepository { get; }
        ICVRepository CVRepository { get; }
        IVideoRepository VideoRepository { get; }
        IGenericRepository<NewsFeedSection> NewsFeedRepository { get; }
        IGenericRepository<Application> AppliedCVRepository { get; }
        IGenericRepository<Translation> TranslationRepository { get; }
        IGenericRepository<Section> SectionRepository { get; }
        IGenericRepository<SectionField> SectionFieldRepository { get; }
        IGenericRepository<SectionType> SectionTypeRepository { get; }
        IGenericRepository<UserDevice> UserDeviceRepository { get; }
        IGenericRepository<AccessKey> AccessKeyRepository { get; }
        IGenericRepository<Style> StyleRepository { get; }
        IGenericRepository<QuestionSet> QuestionSetRepository { get; }
        IGenericRepository<Question> QuestionRepository { get; }
        IGenericRepository<Major> MajorRepository { get; }
        Task<int> CommitAsync();
    }
}
