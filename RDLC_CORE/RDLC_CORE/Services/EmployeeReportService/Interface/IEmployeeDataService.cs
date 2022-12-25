using RDLC_CORE.Areas.Employee.Models;

namespace RDLC_CORE.Services.EmployeeReportService.Interface
{
    public interface IEmployeeDataService
    {
        IEnumerable<EmployeeReportVm> GetAllAsync();
    }
}
