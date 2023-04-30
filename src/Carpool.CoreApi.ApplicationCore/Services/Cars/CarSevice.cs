using Carpool.CoreApi.ApplicationCore.Database;
using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.ApplicationCore.Queries;
using Carpool.CoreApi.ApplicationCore.Resources;
using Carpool.CoreApi.ApplicationCore.Services.Interfaces;
using Carpool.CoreApi.Core.Operations;
using Carpool.CoreApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Carpool.CoreApi.ApplicationCore.Services.CarsService;

/// <summary>
/// Car service
/// </summary>    
public class CarService : ServiceBase<CarService>, ICarService
{
    private readonly CoreContext _context;

    /// <summary>
    /// CarService constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    public CarService(
        ILogger<CarService> logger,
        CoreContext context
        ) : base(logger)
    {
        _context = context;
    }

    public async Task<ItemOperationResponse<Car>> CreateAsync(CreateCarModel model, CancellationToken ct = default)
    {
        return await ExecuteAsync<ItemOperationResponse<Car>>(async response =>
        {
            Color? color = await _context.Colors
            .SingleOrDefaultAsync(x => x.ColorId == model.ColorId);

            if (color == null)
            {
                response.AddError(string.Format(ErrorMessages.eColorNotFound, model.ColorId));
                response.StatusCode = HttpStatusCode.NotFound;
                return;
            }

            CarType? carType = await _context.CarTypes
                .SingleOrDefaultAsync(x => x.CarTypeId == model.CarTypeId);

            if (carType == null)
            {
                response.AddError(string.Format(ErrorMessages.eCarTypeNotFound, model.CarTypeId));
                response.StatusCode = HttpStatusCode.NotFound;
                return;
            }

            Car car = new Car();
            car.Name = model.Name;

            // duplicty will be handled by database
            car.Plate = model.Plate!.ToUpperInvariant();
            car.Color = color;
            car.CarType = carType;

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            response.Item = car;
        });
    }

    public async Task<ItemOperationResponse<Car>> DeleteAsync(Guid carId, CancellationToken ct = default)
    {
        return await ExecuteAsync<ItemOperationResponse<Car>>(async response =>
        {
            Car? car = await _context.Cars
                .SingleOrDefaultAsync(x => x.CarId == carId);

            if (car == null)
            {
                response.AddError(string.Format(ErrorMessages.eCarNotFound, carId));
                response.StatusCode = HttpStatusCode.NotFound;
                return;
            }

            // ToDo: by logic remove all references | not
            // In real world applicatio delete will be not applied

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            response.Item = car;
        });
    }


    public async Task<ListOperationResponse<Car>> ListAvailableAsync(DateTimePickerPeriodQuery query, CancellationToken ct = default)
    {
        return await ExecuteAsync<ListOperationResponse<Car>>(async response =>
        {
            List<Guid?> carsIdInPeriod = await _context.Rides
                .Where(x => x.RidePlan!.StartDate <= query.StartDate && x.RidePlan!.EndDate >= query.EndDate)
                .Select(x => x.Car!.CarId)
                .Distinct()
                .ToListAsync();

            List<Car> availableCars = await _context.Cars
                .Where(x => !carsIdInPeriod.Contains(x.CarId))
                .ToListAsync();

            response.Items = availableCars;
        });
    }

    public async Task<ListOperationResponse<Car>> ListAsync(CancellationToken ct = default)
    {
        return await ExecuteAsync<ListOperationResponse<Car>>(async response =>
        {
            List<Car> cars = await _context.Cars.ToListAsync();

            response.Items = cars;
        });
    }

    public async Task<ItemOperationResponse<Car>> UpdateAsync(UpdateCarModel model, CancellationToken ct = default)
    {
        return await ExecuteAsync<ItemOperationResponse<Car>>(async response =>
        {
            Car? car = await _context.Cars
                .SingleOrDefaultAsync(x => x.CarId == model.CarId);

            if (car == null)
            {
                response.AddError(string.Format(ErrorMessages.eCarNotFound, model.CarId));
                response.StatusCode = HttpStatusCode.NotFound;
                return;
            }

            Color? color = await _context.Colors
                .SingleOrDefaultAsync(x => x.ColorId == model.ColorId);

            if (color == null)
            {
                response.AddError(string.Format(ErrorMessages.eColorNotFound, model.ColorId));
                response.StatusCode = HttpStatusCode.NotFound;
                return;
            }

            CarType? carType = await _context.CarTypes
                .SingleOrDefaultAsync(x => x.CarTypeId == model.CarTypeId);

            if (carType == null)
            {
                response.AddError(string.Format(ErrorMessages.eCarTypeNotFound, model.CarTypeId));
                response.StatusCode = HttpStatusCode.NotFound;
                return;
            }


            car.Name = model.Name;

            // duplicty will be handled by database
            car.Plate = model.Plate!.ToUpperInvariant();
            car.Color = color;
            car.CarType = carType;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            response.Item = car;
        });
    }
}