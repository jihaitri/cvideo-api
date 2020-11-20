using CVideoAPI.Context;

namespace CVideoAPI.Repositories.Recruitment
{
    public class RecruitmentRepository : GenericRepository<Models.RecruitmentPost>, IRecruitmentRepository
    {
        public RecruitmentRepository(CVideoContext context) : base(context) { }
    }
}
