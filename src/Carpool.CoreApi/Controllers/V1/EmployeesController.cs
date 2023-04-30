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
[DisplayName("Cars")]
[Produces("application/json")]
[SwaggerTag("Cars service")]
public class EmployeesController : ApiController<EmployeesController>
{
    private readonly IEmployeeService _employeeService;

    #region Route Names
    public const string EmployeesRoute = "get-employees";
    public const string EmployeeDeleteRoute = "delete-employee";
    public const string EmployeeUpdateRoute = "update-employee";
    #endregion

    /// <summary>
    /// Cars controller
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="correlationContextAccessor"></param>
    /// <param name="employeeService"></param>
    public EmployeesController(
        ILogger<EmployeesController> logger,
        IWebHostEnvironment environment,
        ICorrelationContextAccessor correlationContextAccessor,
        IEmployeeService employeeService)
        : base(logger, correlationContextAccessor, environment)
    {
        _employeeService = employeeService;
    }

    /// <summary>
    /// List all employees stored in the database
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet(Name = EmployeesRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(List<EmployeeDto>), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    public async Task<IActionResult> GetListEmployeesAsync(CancellationToken ct = default)
    {
        ListOperationResponse<Employee> response = await _employeeService.ListAsync(ct);

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
    /// Removed emloyee defined by employeeId
    /// </summary>        
    /// <param name="employeeId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("{employeeId}", Name = EmployeeDeleteRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(CarDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> DeleteCarAsync([FromRoute(Name = "employeeId"), Required] Guid employeeId,
            CancellationToken ct = default)
    {
        ItemOperationResponse<Employee> response = await _employeeService.DeleteAsync(employeeId, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }

    /// <summary>
    /// Update employee
    /// </summary>        
    /// <param name="model"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost(Name = EmployeeUpdateRoute)]
    [ValidateModelState]
    [SwaggerResponse(statusCode: 200, type: typeof(EmployeeDto), description: "OK")]
    [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse400), description: "The request cannot be processed because this is a bad request or there are validation errors")]
    [SwaggerResponse(statusCode: 422, type: typeof(ErrorResponse422), description: "Unprocessable entity")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse404), description: "Not found")]
    public async Task<IActionResult> PostCarAsync([FromBody, Bind] UpdateEmployeeModel model,
            CancellationToken ct = default)
    {
        ItemOperationResponse<Employee> response = await _employeeService.UpdateAsync(model, ct);

        if (response.Succeeded == true)
        {
            return Ok(response.Item!.ToDto());
        }

        return ErrorResponse(response);
    }
}
