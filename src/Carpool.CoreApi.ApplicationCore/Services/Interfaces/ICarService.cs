using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.ApplicationCore.Queries;
using Carpool.CoreApi.Core.Operations;

namespace Carpool.CoreApi.ApplicationCore.Services.Interfaces;

public interface ICarService
{
    Task<ListOperationResponse<Car>> ListAsync(CancellationToken ct = default);
    Task<ListOperationResponse<Car>> ListAvailableAsync(DateTimePickerPeriodQuery query, CancellationToken ct = default);
    Task<ItemOperationResponse<Car>> DeleteAsync(Guid carId, CancellationToken ct = default);
    Task<ItemOperationResponse<Car>> UpdateAsync(UpdateCarModel model, CancellationToken ct = default);
    Task<ItemOperationResponse<Car>> CreateAsync(CreateCarModel model, CancellationToken ct = default);
}
