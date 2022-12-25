using Microsoft.AspNetCore.Mvc;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using System.Composition;
using System.Data;

namespace RDLC_CORE.Areas.Employee.Controllers
{
    public class EmployeeReportController : Controller
    {
    
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeReportController(IWebHostEnvironment webHostEnvironment)
        {

            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Report()
        {
            var dt = new DataTable();
            dt = report.GetReportList();  //Get Store procedure result set

            string mimeType = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\ReportExpense.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm1", "RDCL Report");
            parameters.Add("prm2", DateTime.Now.ToString("dd-MM-yyyy"));
            parameters.Add("prm3", "Expense Report");

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSetName", dt);  //This DataSetName must be used  in .rdlc report datasetName

            var res = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);
            return File(res.MainStream, "application/pdf");
            //return File(result.MainStream, "application/msexcel", "Export.xls");
        }

    }
}
