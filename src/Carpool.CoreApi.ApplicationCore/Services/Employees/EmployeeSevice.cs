using Carpool.CoreApi.ApplicationCore.Database;
using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.ApplicationCore.Resources;
using Carpool.CoreApi.ApplicationCore.Services.Interfaces;
using Carpool.CoreApi.Core.Operations;
using Carpool.CoreApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Carpool.CoreApi.ApplicationCore.Services.EmployeesService;

/// <summary>
/// Car service
/// </summary>    
public class EmployeeService : ServiceBase<EmployeeService>, IEmployeeService
{
    private readonly CoreContext _context;

    /// <summary>
    /// CarService constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    public EmployeeService(
        ILogger<EmployeeService> logger,
        CoreContext context
        ) : base(logger)
    {
        _context = context;
    }

    public async Task<ItemOperationResponse<Employee>> CreateAsync(CreateEmployeeModel model, CancellationToken ct = default)
    {
        return await ExecuteAsync<ItemOperationResponse<Employee>>(async response =>
        {
            Employee employee = new();
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.IsDriver = model.IsDriver;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            response.Item = employee;
        });
    }

    public async Task<ItemOperationResponse<Employee>> DeleteAsync(Guid employeeId, CancellationToken ct = default)
    {
        return await ExecuteAsync<ItemOperationResponse<Employee>>(async response =>
        {
            Employee? employee = await _context.Employees
                .SingleOrDefaultAsync(x => x.EmployeeId == employeeId);

            if (employee == null)
            {
                response.AddError(string.Format(ErrorMessages.eEmployeeNotFound, employeeId));
                response.StatusCode = HttpStatusCode.NotFound;
                return;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            response.Item = employee;
        });
    }

    public async Task<ListOperationResponse<Employee>> ListAsync(CancellationToken ct = default)
    {
        return await ExecuteAsync<ListOperationResponse<Employee>>(async response =>
        {
            List<Employee> employees = await _context.Employees.ToListAsync();

            response.Items = employees;
        });
    }

    public async Task<ItemOperationResponse<Employee>> UpdateAsync(UpdateEmployeeModel model, CancellationToken ct = default)
    {
        return await ExecuteAsync<ItemOperationResponse<Employee>>(async response =>
        {
            Employee? employee = await _context.Employees
                .SingleOrDefaultAsync(x => x.EmployeeId == model.EmployeeId);

            if (employee == null)
            {
                response.AddError(string.Format(ErrorMessages.eEmployeeNotFound, model.EmployeeId));
                response.StatusCode = HttpStatusCode.NotFound;
                return;
            }


            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.IsDriver = model.IsDriver;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            response.Item = employee;
        });
    }
}