using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.Core.Operations;

namespace Carpool.CoreApi.ApplicationCore.Services.Interfaces;

public interface ICarTypeService
{
    Task<ListOperationResponse<CarType>> ListAsync(CancellationToken ct = default);
    Task<ItemOperationResponse<CarType>> DeleteAsync(Guid carTypeId, CancellationToken ct = default);
    Task<ItemOperationResponse<CarType>> UpdateAsync(UpdateCarTypeModel model, CancellationToken ct = default);
    Task<ItemOperationResponse<CarType>> CreateAsync(CreateCarTypeModel model, CancellationToken ct = default);
}
