namespace EmployeeApi.Application.DTOs.Employees;

public record EmployeeResponse(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Department,
    string Location,
    decimal Salary,
    DateTime JoiningDate,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string RowVersion);
