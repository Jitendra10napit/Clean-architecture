using System;
using EmployeeApi.Application.DTOs.Employees;
using FluentValidation;

namespace EmployeeApi.Application.Validators;

public class CreateEmployeeValidator
    : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(x => x.Department)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.JoiningDate)
            .LessThanOrEqualTo(DateTime.UtcNow);
    }
}
