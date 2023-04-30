using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.Core.Operations;

namespace Carpool.CoreApi.ApplicationCore.Services.Interfaces;

public interface IEmployeeService
{
    Task<ListOperationResponse<Employee>> ListAsync(CancellationToken ct = default);
    Task<ItemOperationResponse<Employee>> DeleteAsync(Guid employeeId, CancellationToken ct = default);
    Task<ItemOperationResponse<Employee>> UpdateAsync(UpdateEmployeeModel model, CancellationToken ct = default);
    Task<ItemOperationResponse<Employee>> CreateAsync(CreateEmployeeModel model, CancellationToken ct = default);
}
