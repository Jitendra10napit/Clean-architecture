EmployeeApi/
│
├── EmployeeApi.sln
│
├── EmployeeApi.Domain/
│   │
│   ├── Entities/
│   │   └── Employee.cs
│   │
│   └── Exceptions/
│       └── DomainException.cs
│
├── EmployeeApi.Application/
│   │
│   ├── Abstractions/
│   │   ├── IEmployeeRepository.cs
│   │   └── IUnitOfWork.cs
│   │
│   ├── DTOs/
│   │   └── Employees/
│   │       ├── CreateEmployeeRequest.cs
│   │       ├── UpdateEmployeeRequest.cs
│   │       ├── EmployeeResponse.cs
│   │       ├── EmployeeSearchRequest.cs
│   │       └── PagedResponse.cs
│   │
│   ├── Services/
│   │   ├── IEmployeeService.cs
│   │   └── EmployeeService.cs
│   │
│   └── Validators/
│       ├── CreateEmployeeValidator.cs
│       └── UpdateEmployeeValidator.cs
│
├── EmployeeApi.Infrastructure/
│   │
│   ├── Persistence/
│   │   ├── AppDbContext.cs
│   │   └── Configurations/
│   │       └── EmployeeConfiguration.cs
│   │
│   └── Repositories/
│       └── EmployeeRepository.cs
│
└── EmployeeApi.API/
    │
    ├── Controllers/
    │   └── EmployeesController.cs
    │
    ├── Middleware/
    │   └── ExceptionMiddleware.cs
    │
    ├── Program.cs
    └── appsettings.json