using System;
using EmployeeApi.Application.DTOs.Employees;

namespace EmployeeApi.Application.Abstractions;

public interface IEmployeeService
{
    Task<EmployeeResponse> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<PagedResponse<EmployeeResponse>> SearchAsync(
        EmployeeSearchRequest request,
        CancellationToken cancellationToken);

    Task<EmployeeResponse> CreateAsync(
        CreateEmployeeRequest request,
        CancellationToken cancellationToken);

    Task<EmployeeResponse> UpdateAsync(
        int id,
        UpdateEmployeeRequest request,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}
