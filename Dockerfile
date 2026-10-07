# ==========================================
# Stage 1: Build
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src 

# Copy project files
COPY EmployeeApi.API/EmployeeApi.API.csproj EmployeeApi.API/
COPY EmployeeApi.Application/EmployeeApi.Application.csproj EmployeeApi.Application/
COPY EmployeeApi.Domain/EmployeeApi.Domain.csproj EmployeeApi.Domain/
COPY EmployeeApi.Infrastructure/EmployeeApi.Infrastructure.csproj EmployeeApi.Infrastructure/

# Restore dependencies
RUN dotnet restore EmployeeApi.API/EmployeeApi.API.csproj

# Copy source code
COPY . .

# Build
RUN dotnet build EmployeeApi.API/EmployeeApi.API.csproj \
    --configuration Release \
    --no-restore \
    --output /app/build


# ==========================================
# Stage 2: Publish
# ==========================================
FROM build AS publish

RUN dotnet publish EmployeeApi.API/EmployeeApi.API.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish \
    /p:UseAppHost=false


# ==========================================
# Stage 3: Runtime
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

# Container port
EXPOSE 8080

ENV ASPNETCORE_HTTP_PORTS=8080

ENTRYPOINT ["dotnet", "EmployeeApi.API.dll"]