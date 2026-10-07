namespace EmployeeApi.Application.DTOs.Employees;

public record EmployeeSearchRequest(
    string? Search = null,
    string? Department = null,
    string? Location = null,
    string SortBy = "Id",
    string SortOrder = "asc",
    int Page = 1,
    int PageSize = 20);
