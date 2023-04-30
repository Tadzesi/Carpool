using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Core.Attributes;

namespace Carpool.CoreApi.ApplicationCore.Models;


[CustomValidator(typeof(UpdateRideModelValidation))]
public class UpdateRideModel
{
    public Guid? RideId { get; set; }

    public string? Name { get; set; }

    public Guid? CarId { get; set; }
}
