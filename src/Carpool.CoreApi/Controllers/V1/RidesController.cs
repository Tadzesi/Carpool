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
/// Cars controller 
/// </summary>
[ApiVersion("1.0")]
[DisplayName("Rides")]
[Produces("application/json")]
[SwaggerTag("Rides service")]
public class RidesController : ApiController<RidesController>
{
    private readonly IRideService _rideService;

    #region Route Names
    public const string RidesRoute = "get-rides";
    public const string RideDeleteRoute = "delete-ride";
    public const string RideDeleteEmployeeRoute = "delete-employee-ride";
    public const string RideUpdateRoute = "update-ride";
    public const string RideCreateRoute = "create-ride";
    public const string RideAddEmployeeRoute = "add-employee-ride";
    public const string RideUpdatePlanRoute = "update-plan-ride";
    #endregion

    /// <summary>
    /// Cars controller
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="correlationContextAccessor"></param>
    /// <param name="rideService"></param>
    public RidesController(
        ILogger<RidesController> logger,
        IWebHostEnvironment environment,
        ICorrelationContextAccessor correlationContextAccessor,
        IRideService rideService)
        : base(logger, correlationContextAccessor, environment)
    {
        _rideService = rideService;
    }

    /// <summary>
    /// List all rides stored in the database
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet(Name = RidesRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(List<RideDto>), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    public async Task<IActionResult> GetListRidesAsync(CancellationToken ct = default)
    {
        ListOperationResponse<Ride> response = await _rideService.ListAsync(ct);

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
    /// Remove ride defined by rideId
    /// </summary>        
    /// <param name="rideId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("{rideId}", Name = RideDeleteRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(RideDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> DeleteRideAsync([FromRoute(Name = "rideId"), Required] Guid rideId,
            CancellationToken ct = default)
    {
        ItemOperationResponse<Ride> response = await _rideService.DeleteAsync(rideId, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// Update ride
    /// </summary>        
    /// <param name="model"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPut(Name = RideUpdateRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(RideDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> PutRideAsync([FromBody, Bind] UpdateRideModel model,
            CancellationToken ct = default)
    {
        ItemOperationResponse<Ride> response = await _rideService.UpdateAsync(model, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// Create ride
    /// </summary>        
    /// <param name="model"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost(Name = RideCreateRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(RideDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> PostRideAsync([FromBody, Bind] CreateRideModel model,
            CancellationToken ct = default)
    {
        ItemOperationResponse<Ride> response = await _rideService.CreateAsync(model, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// Update plan
    /// </summary>        
    /// <param name="model"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPut("plan", Name = RideUpdatePlanRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(RideDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> PutPlanAsync([FromBody, Bind] UpdatePlanModel model,
            CancellationToken ct = default)
    {
        ItemOperationResponse<Ride> response = await _rideService.UpdatePlanAsync(model, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// Remove employee from plan defined by rideId and planId
    /// </summary>        
    /// <param name="employeeId"></param>
    /// <param name="rideId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("Employee/{employeeId}/{rideId}", Name = RideDeleteEmployeeRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(RideDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> DeleteEmployeeAsync(
        [FromRoute(Name = "employeeId"), Required] Guid employeeId,
        [FromRoute(Name = "rideId"), Required] Guid rideId,
        CancellationToken ct = default)
    {
        ItemOperationResponse<Ride> response = await _rideService.DeleteEmploeeAsync(employeeId, rideId, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// Add employee to ride
    /// </summary>        
    /// <param name="employeeId"></param>
    /// <param name="rideId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("Employee/{employeeId}/{rideId}", Name = RideAddEmployeeRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(RideDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> PostEmployeeAsync([FromRoute(Name = "employeeId"), Required] Guid employeeId,
        [FromRoute(Name = "rideId"), Required] Guid rideId,
        CancellationToken ct = default)
    {
        ItemOperationResponse<Ride> response = await _rideService.DeleteEmploeeAsync(employeeId, rideId, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }
}
