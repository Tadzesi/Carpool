using Carpool.CoreApi.Api.Core.Models;
using Carpool.CoreApi.ApplicationCore.Dto;
using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.ApplicationCore.Queries;
using Carpool.CoreApi.ApplicationCore.Services.Interfaces;
using Carpool.CoreApi.Core.Attributes;
using Carpool.CoreApi.Core.Operations;
using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebTD.Api.Core.Controllers;

namespace Carpool.CoreApi.Controllers.V1;

/// <summary>
/// Cars controller 
/// </summary>
[ApiVersion("1.0")]
[DisplayName("Cars")]
[Produces("application/json")]
[SwaggerTag("Cars service")]
public class CarsController : ApiController<CarsController>
{
    private readonly ICarService _carService;

    #region Route Names
    public const string CarsRoute = "get-cars";
    public const string CarsAvailableRoute = "get-cars-availabe";
    public const string CarDeleteRoute = "delete-car";
    public const string CarUpdateRoute = "update-car";
    #endregion

    /// <summary>
    /// Cars controller
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="correlationContextAccessor"></param>
    /// <param name="carService"></param>
    public CarsController(
        ILogger<CarsController> logger,
        IWebHostEnvironment environment,
        ICorrelationContextAccessor correlationContextAccessor,
        ICarService carService)
        : base(logger, correlationContextAccessor, environment)
    {
        _carService = carService;
    }

    /// <summary>
    /// List available cars defined by start and end date
    /// </summary>
    /// <param name="query"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("available", Name = CarsAvailableRoute)]
    [SwaggerResponse(statusCode: 200, type: typeof(List<CarDto>), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    public async Task<IActionResult> GetListAvailableCarsAsync([FromQuery, Bind] DateTimePickerPeriodQuery query,
            CancellationToken ct = default)
    {
        ListOperationResponse<Car> response = await _carService.ListAvailableAsync(query, ct);

        if (response.Succeeded == true)
        {
            var objectsList = response.Items!
                    .Select(x => x.ToDto())
                    .ToList();

            return Ok(objectsList);
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// List all cars stored in the database
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet(Name = CarsRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(List<CarDto>), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    public async Task<IActionResult> GetListCarsAsync(CancellationToken ct = default)
    {
        ListOperationResponse<Car> response = await _carService.ListAsync(ct);

        if (response.Succeeded == true)
        {
            var objectsList = response.Items!
                    .Select(x => x.ToDto())
                    .ToList();

            return Ok(objectsList);
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// Removed car defined by carId
    /// </summary>        
    /// <param name="carId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("{carId}", Name = CarDeleteRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(CarDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> DeleteCarAsync([FromRoute(Name = "carId"), Required] Guid carId,
            CancellationToken ct = default)
    {
        ItemOperationResponse<Car> response = await _carService.DeleteAsync(carId, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// Update Car
    /// </summary>        
    /// <param name="model"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost(Name = CarUpdateRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(CarDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> PostCarAsync([FromBody, Bind] UpdateCarModel model,
            CancellationToken ct = default)
    {
        ItemOperationResponse<Car> response = await _carService.UpdateAsync(model, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }
}
