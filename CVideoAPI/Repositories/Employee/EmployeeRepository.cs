using CVideoAPI.Context;

namespace CVideoAPI.Repositories.Employee
{
    public class EmployeeRepository : GenericRepository<Models.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CVideoContext context) : base(context) { }
    }
}
