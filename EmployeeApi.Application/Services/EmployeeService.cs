using System;
using EmployeeApi.Application.Abstractions;
using EmployeeApi.Application.DTOs.Employees;
using EmployeeApi.Domain.Entities;

namespace EmployeeApi.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(
        IEmployeeRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeResponse> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var employee = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (employee is null)
            throw new KeyNotFoundException(
                $"Employee {id} was not found.");

        return Map(employee);
    }

    public async Task<PagedResponse<EmployeeResponse>> SearchAsync(
        EmployeeSearchRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _repository.SearchAsync(
            request.Search,
            request.Department,
            request.Location,
            request.SortBy,
            request.SortOrder,
            request.Page,
            request.PageSize,
            cancellationToken);

        var items = result.Items
            .Select(Map)
            .ToList();

        var totalPages =
            (int)Math.Ceiling(
                result.TotalCount /
                (double)request.PageSize);

        return new PagedResponse<EmployeeResponse>(
            items,
            request.Page,
            request.PageSize,
            result.TotalCount,
            totalPages,
            request.Page < totalPages,
            request.Page > 1);
    }

    public async Task<EmployeeResponse> CreateAsync(
        CreateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var existing =
            await _repository.GetByEmailAsync(
                request.Email,
                cancellationToken);

        if (existing is not null)
            throw new InvalidOperationException(
                "Employee with this email already exists.");

        var employee = new Employee(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Department,
            request.Location,
            request.Salary,
            request.JoiningDate);

        _repository.Add(employee);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Map(employee);
    }

    public async Task<EmployeeResponse> UpdateAsync(
        int id,
        UpdateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var employee =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        if (employee is null)
            throw new KeyNotFoundException(
                $"Employee {id} was not found.");

        var currentVersion =
            Convert.ToBase64String(
                employee.RowVersion);

        if (currentVersion != request.RowVersion)
        {
            throw new InvalidOperationException(
                "Employee was modified by another request.");
        }

        employee.Update(
            request.FirstName,
            request.LastName,
            request.Department,
            request.Location,
            request.Salary);

        _repository.Update(employee);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Map(employee);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var employee =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        if (employee is null)
            throw new KeyNotFoundException(
                $"Employee {id} was not found.");

        employee.Delete();

        _repository.Update(employee);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);
    }

    private static EmployeeResponse Map(
        Employee employee)
    {
        return new EmployeeResponse(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Email,
            employee.Department,
            employee.Location,
            employee.Salary,
            employee.JoiningDate,
            employee.CreatedAt,
            employee.UpdatedAt,
            Convert.ToBase64String(
                employee.RowVersion));
    }
}
