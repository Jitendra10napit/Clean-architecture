namespace EmployeeApi.Application.DTOs.Employees;

public record UpdateEmployeeRequest(
    string FirstName,
    string LastName,
    string Department,
    string Location,
    decimal Salary,
    string RowVersion);