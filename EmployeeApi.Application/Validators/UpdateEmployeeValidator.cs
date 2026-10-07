using System;
using EmployeeApi.Application.DTOs.Employees;
using FluentValidation;

namespace EmployeeApi.Application.Validators;

public class UpdateEmployeeValidator
    : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Department)
            .NotEmpty();

        RuleFor(x => x.Location)
            .NotEmpty();

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.RowVersion)
            .NotEmpty();
    }
}
