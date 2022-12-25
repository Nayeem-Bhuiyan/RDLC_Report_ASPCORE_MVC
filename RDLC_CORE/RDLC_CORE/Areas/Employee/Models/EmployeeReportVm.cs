namespace RDLC_CORE.Areas.Employee.Models
{
    public class EmployeeReportVm
    {
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? DepartmentName { get; set; }
    }
}
