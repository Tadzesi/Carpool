using Carpool.CoreApi.Api.Core.Models;
using Carpool.CoreApi.ApplicationCore.Dto;
using Carpool.CoreApi.ApplicationCore.Entities;
using Carpool.CoreApi.ApplicationCore.Models;
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
/// Car types controller 
/// </summary>
[ApiVersion("1.0")]
[DisplayName("Car types")]
[Produces("application/json")]
[SwaggerTag("Car types service")]
public class CarTypesController : ApiController<CarTypesController>
{
    private readonly ICarTypeService _carTypeService;

    #region Route Names
    public const string CarTypesRoute = "get-car-types";
    public const string CarTypeDeleteRoute = "delete-car-type";
    public const string CarTypeUpdateRoute = "update-car-type";
    public const string CarTypeCreateRoute = "create-car-type";
    #endregion

    /// <summary>
    /// Car types controller
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="correlationContextAccessor"></param>
    /// <param name="carTypeService"></param>
    public CarTypesController(
        ILogger<CarTypesController> logger,
        IWebHostEnvironment environment,
        ICorrelationContextAccessor correlationContextAccessor,
        ICarTypeService carTypeService)
        : base(logger, correlationContextAccessor, environment)
    {
        _carTypeService = carTypeService;
    }

    /// <summary>
    /// List all car types stored in the database
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet(Name = CarTypesRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(List<CarTypeDto>), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    public async Task<IActionResult> GetListCarTypesAsync(CancellationToken ct = default)
    {
        ListOperationResponse<CarType> response = await _carTypeService.ListAsync(ct);

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
    /// Removed car type defined by carID
    /// </summary>        
    /// <param name="carTypeId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("{carTypeId}", Name = CarTypeDeleteRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(CarTypeDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> DeleteCarAsync([FromRoute(Name = "carTypeId"), Required] Guid carTypeId,
            CancellationToken ct = default)
    {
        ItemOperationResponse<CarType> response = await _carTypeService.DeleteAsync(carTypeId, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// Update car type
    /// </summary>        
    /// <param name="model"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost(Name = CarTypeUpdateRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(CarTypeDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> PostCarAsync([FromBody, Bind] UpdateCarTypeModel model,
            CancellationToken ct = default)
    {
        ItemOperationResponse<CarType> response = await _carTypeService.UpdateAsync(model, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }
}
