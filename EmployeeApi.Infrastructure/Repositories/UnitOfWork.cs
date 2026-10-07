using System;
using EmployeeApi.Application.Abstractions;
using EmployeeApi.Infrastructure.Persistence;

namespace EmployeeApi.Infrastructure.Repositories; 

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(
            cancellationToken);
    }
}
