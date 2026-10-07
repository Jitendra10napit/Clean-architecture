using System;

namespace EmployeeApi.Application.DTOs.Employees;

public record CreateEmployeeRequest(
    string FirstName,
    string LastName,
    string Email,
    string Department,
    string Location,
    decimal Salary,
    DateTime JoiningDate);
