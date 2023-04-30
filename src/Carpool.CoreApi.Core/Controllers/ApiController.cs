using Carpool.CoreApi.Api.Core.Models;
using Carpool.CoreApi.Core.Helper;
using Carpool.CoreApi.Core.Operations;
using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace WebTD.Api.Core.Controllers
{
    /// <summary>
    /// Base API controller class
    /// </summary>
    /// <typeparam name="TController"></typeparam>
    [Route("/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class ApiController<TController> : ControllerBase where TController : ApiController<TController>
    {
        protected ILogger<TController> Logger;
        protected ICorrelationContextAccessor? CorrelationContextAccessor;
        protected IWebHostEnvironment? HostEnvironment;

        protected ApiController(ILogger<TController> logger)
        {
            Logger = logger;
        }

        protected ApiController(
            ILogger<TController> logger,
            ICorrelationContextAccessor? correlationContextAccessor,
            IWebHostEnvironment? hostEnvironment)
        {
            Logger = logger;
            CorrelationContextAccessor = correlationContextAccessor;
            HostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Create error response determined from OperationResponse object
        /// Make sure you initialized controller with IWebHostEnvironment and ICorrelationContextAccessor
        /// </summary>
        /// <exception cref="ArgumentException">Throws exception when environment and correlation are not setup</exception>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="errorCode"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        protected IActionResult ErrorResponse<TResponse>(
            TResponse response,
            string? errorCode = null,
            string? link = null) where TResponse : OperationResponse
        {


            if (HostEnvironment == null)
            {
                throw new ArgumentException(nameof(HostEnvironment));
            }

            if (CorrelationContextAccessor == null)
            {
                throw new ArgumentException(nameof(CorrelationContextAccessor));
            }

            IErrorResponse error = ResponseHelper.GetErrorResponse(
                response,
                HostEnvironment!,
                CorrelationContextAccessor!.CorrelationContext,
                errorCode,
                link);

            return StatusCode(error.Status, error);
        }
    }
}
