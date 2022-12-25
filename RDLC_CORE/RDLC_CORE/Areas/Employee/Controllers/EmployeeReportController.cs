using Microsoft.AspNetCore.Mvc;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using System.Composition;
using System.Data;
using RDLC_CORE.Services.EmployeeReportService.Interface;
using RDLC_CORE.Areas.Employee.Models;

namespace RDLC_CORE.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class EmployeeReportController : Controller
    {
    
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmployeeDataService _employeeDataService;
        public EmployeeReportController(IWebHostEnvironment webHostEnvironment, IEmployeeDataService employeeDataService)
        {
           
            _webHostEnvironment = webHostEnvironment;
            _employeeDataService=employeeDataService;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public  IActionResult Report()
        {

            IEnumerable<EmployeeReportVm> EmpDetailsList = _employeeDataService.GetAllAsync();  //Get Store procedure result set

            string mimeType = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.ContentRootPath}Reports\\EmployeeReport.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("CompanyName", "ABC Limted");
            parameters.Add("prmReportHeadLine","Employee Details Info" );
            parameters.Add("prmDate", DateTime.Now.ToString("dd-MMM-yyyy"));

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsEmployeeRecord", EmpDetailsList);  //This DataSetName must be used  in .rdlc report datasetName

            var res = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);
            return File(res.MainStream, "application/pdf");
            //return File(result.MainStream, "application/msexcel", "Export.xls");
        }

    }
}
