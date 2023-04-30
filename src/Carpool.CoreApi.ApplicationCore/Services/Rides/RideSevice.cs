using Carpool.CoreApi.ApplicationCore.Database;
using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.ApplicationCore.Services.Interfaces;
using Carpool.CoreApi.Core.Operations;
using Carpool.CoreApi.Services;
using Microsoft.Extensions.Logging;

namespace Carpool.CoreApi.ApplicationCore.Services.RidesService;

/// <summary>
/// Car service
/// </summary>    
public class RideService : ServiceBase<RideService>, IRideService
{
    private readonly CoreContext _context;

    /// <summary>
    /// CarService constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    public RideService(
        ILogger<RideService> logger,
        CoreContext context
        ) : base(logger)
    {
        _context = context;
    }

    public Task<ItemOperationResponse<Ride>> AddEmploeeAsync(Guid employeeId, Guid planId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ItemOperationResponse<Ride>> CreateAsync(CreateRideModel model, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ItemOperationResponse<Ride>> DeleteAsync(Guid rideId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ListOperationResponse<Ride>> ListAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ItemOperationResponse<Ride>> DeleteEmploeeAsync(Guid employeeId, Guid planId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ItemOperationResponse<Ride>> UpdateAsync(UpdateRideModel model, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ItemOperationResponse<Ride>> UpdatePlanAsync(UpdatePlanModel model, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}