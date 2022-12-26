CREATE PROCEDURE Sp_GetEmployeeDetails
AS
BEGIN
SELECT e.Id as EmployeeId,e.Name as EmployeeName,e.Age,e.Address,e.DateOfBirth,e.Gender,d.Name as DepartmentName
FROM EmployeeDetails e
INNER JOIN Department d ON e.DepertmentId=d.Id
END

--EXEC Sp_GetEmployeeDetails