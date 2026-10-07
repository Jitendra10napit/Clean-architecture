using System;

namespace EmployeeApi.Application.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken);
}
