using Microsoft.Data.SqlClient;
using RDLC_CORE.Areas.Employee.Models;
using RDLC_CORE.Services.EmployeeReportService.Interface;
using System;
using System.Data;
namespace RDLC_CORE.Services.EmployeeReportService
{
    public class EmployeeDataService: IEmployeeDataService
    {
        private readonly string _connectionString;
        public EmployeeDataService(IConfiguration configuration)
        {

            _connectionString = configuration.GetConnectionString("AppDbConnection");
        }

        #region GetAllRecord
        public IEnumerable<EmployeeReportVm> GetAllAsync()
        {

            List<EmployeeReportVm> EmployeeList = new List<EmployeeReportVm>();
            try
            {

                using (SqlConnection thisConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Sp_GetEmployeeDetails", thisConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                         thisConnection.OpenAsync();
                        SqlDataReader rdr =  cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            EmployeeReportVm objEmployee = new EmployeeReportVm();
   
                            objEmployee.EmployeeId = Convert.ToInt32(rdr["DivisionId"]);
                            objEmployee.Age = Convert.ToInt32(rdr["DivisionId"]);
                            objEmployee.EmployeeName = rdr["EmployeeName"].ToString();
                            objEmployee.Address = rdr["Address"].ToString();
                            objEmployee.DateOfBirth =Convert.ToDateTime(rdr["DateOfBirth"]);
                            objEmployee.Gender = rdr["Gender"].ToString();
                            objEmployee.DepartmentName = rdr["DepartmentName"].ToString();
                            EmployeeList.Add(objEmployee);
                        }
                        thisConnection.Close();
                    }
                }

            }
            catch (Exception)
            {
                return EmployeeList = new List<EmployeeReportVm>();
            }

            return EmployeeList;
        }
        #endregion
    }
}
