using System;
using EmployeeApi.Domain.Entities;

namespace EmployeeApi.Application.Abstractions;

public interface IEmployeeRepository
{
Task<Employee?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<Employee?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task<(IReadOnlyList<Employee> Items, int TotalCount)> SearchAsync(
        string? search,
        string? department,
        string? location,
        string sortBy,
        string sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    void Add(Employee employee);

    void Update(Employee employee);

    void Delete(Employee employee);
}
