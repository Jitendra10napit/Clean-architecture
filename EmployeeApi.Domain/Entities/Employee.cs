using System;

namespace EmployeeApi.Domain.Entities;

public class Employee
{
    public int Id { get; private set; }

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string Department { get; private set; } = null!;

    public string Location { get; private set; } = null!;

    public decimal Salary { get; private set; }

    public DateTime JoiningDate { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public bool IsDeleted { get; private set; }

    public byte[] RowVersion { get; private set; } = [];

    private Employee()
    {
    }

    public Employee(
        string firstName,
        string lastName,
        string email,
        string department,
        string location,
        decimal salary,
        DateTime joiningDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Department = department;
        Location = location;
        Salary = salary;
        JoiningDate = joiningDate;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        string firstName,
        string lastName,
        string department,
        string location,
        decimal salary)
    {
        FirstName = firstName;
        LastName = lastName;
        Department = department;
        Location = location;
        Salary = salary;

        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
}