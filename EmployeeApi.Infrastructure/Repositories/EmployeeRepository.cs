
using System;
using EmployeeApi.Application.Abstractions;
using EmployeeApi.Domain.Entities;
using EmployeeApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Infrastructure.Repositories;

public class EmployeeRepository: IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Employee?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(
                x => x.Email == email,
                cancellationToken);
    }

    public async Task<(
        IReadOnlyList<Employee> Items,
        int TotalCount)> SearchAsync(
        string? search,
        string? department,
        string? location,
        string sortBy,
        string sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        IQueryable<Employee> query =
            _context.Employees.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.FirstName.Contains(search) ||
                x.LastName.Contains(search) ||
                x.Email.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(department))
        {
            query = query.Where(
                x => x.Department == department);
        }

        if (!string.IsNullOrWhiteSpace(location))
        {
            query = query.Where(
                x => x.Location == location);
        }

        query = ApplySorting(
            query,
            sortBy,
            sortOrder);

        var totalCount =
            await query.CountAsync(
                cancellationToken);

        var items =
            await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public void Add(Employee employee)
    {
        _context.Employees.Add(employee);
    }

    public void Update(Employee employee)
    {
        _context.Employees.Update(employee);
    }

    public void Delete(Employee employee)
    {
        _context.Employees.Remove(employee);
    }

    private static IQueryable<Employee> ApplySorting(
        IQueryable<Employee> query,
        string sortBy,
        string sortOrder)
    {
        var descending =
            sortOrder.Equals(
                "desc",
                StringComparison.OrdinalIgnoreCase);

        return sortBy.ToLowerInvariant() switch
        {
            "firstname" => descending
                ? query.OrderByDescending(x => x.FirstName)
                : query.OrderBy(x => x.FirstName),

            "salary" => descending
                ? query.OrderByDescending(x => x.Salary)
                : query.OrderBy(x => x.Salary),

            "joiningdate" => descending
                ? query.OrderByDescending(x => x.JoiningDate)
                : query.OrderBy(x => x.JoiningDate),

            _ => descending
                ? query.OrderByDescending(x => x.Id)
                : query.OrderBy(x => x.Id)
        };
    }
}
