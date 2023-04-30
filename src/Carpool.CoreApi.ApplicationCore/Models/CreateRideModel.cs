using Carpool.CoreApi.ApplicationCore.Dto;
using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Core.Attributes;

namespace Carpool.CoreApi.ApplicationCore.Models;

[CustomValidator(typeof(CreateRiderModelValidation))]
public class CreateRideModel
{

    public string? Name { get; set; }

    public Guid? CarId { get; set; }

    public RidePlanDto? RidePlan { get; set; }
}
