using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.Core.Operations;

namespace Carpool.CoreApi.ApplicationCore.Services.Interfaces;

public interface IRideService
{
    Task<ListOperationResponse<Ride>> ListAsync(CancellationToken ct = default);
    Task<ItemOperationResponse<Ride>> DeleteAsync(Guid rideId, CancellationToken ct = default);
    Task<ItemOperationResponse<Ride>> UpdateAsync(UpdateRideModel model, CancellationToken ct = default);
    Task<ItemOperationResponse<Ride>> CreateAsync(CreateRideModel model, CancellationToken ct = default);
    Task<ItemOperationResponse<Ride>> AddEmploeeAsync(Guid employeeId, Guid planId, CancellationToken ct = default);
    Task<ItemOperationResponse<Ride>> DeleteEmploeeAsync(Guid employeeId, Guid planId, CancellationToken ct = default);
    Task<ItemOperationResponse<Ride>> UpdatePlanAsync(UpdatePlanModel model, CancellationToken ct = default);
}
