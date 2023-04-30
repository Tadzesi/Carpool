using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Core.Attributes;

namespace Carpool.CoreApi.ApplicationCore.Models;


[CustomValidator(typeof(UpdateCarModelValidation))]
public class UpdatePlanModel
{
    public Guid? RideId { get; set; }
    public string? StartLocation { get; set; }
    public string? EndLocation { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
