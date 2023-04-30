using Carpool.CoreApi.ApplicationCore.Database;
using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.ApplicationCore.Services.Interfaces;
using Carpool.CoreApi.Core.Operations;
using Carpool.CoreApi.Services;
using Microsoft.Extensions.Logging;

namespace Carpool.CoreApi.ApplicationCore.Services.CarsService;

/// <summary>
/// Car service
/// </summary>    
public class CarTypeService : ServiceBase<CarTypeService>, ICarTypeService
{
    private readonly CoreContext _context;

    /// <summary>
    /// CarService constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    public CarTypeService(
        ILogger<CarTypeService> logger,
        CoreContext context
        ) : base(logger)
    {
        _context = context;
    }

    public Task<ItemOperationResponse<CarType>> CreateAsync(CreateCarTypeModel model, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ItemOperationResponse<CarType>> DeleteAsync(Guid carTypeId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ListOperationResponse<CarType>> ListAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ItemOperationResponse<CarType>> UpdateAsync(UpdateCarTypeModel model, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}