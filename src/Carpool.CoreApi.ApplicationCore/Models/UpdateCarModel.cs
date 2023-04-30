using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Core.Attributes;

namespace Carpool.CoreApi.ApplicationCore.Models;


[CustomValidator(typeof(UpdateCarModelValidation))]
public class UpdateCarModel
{
    public Guid? CarId { get; set; }
    public string? Name { get; set; }

    public string? Plate { get; set; }

    public Guid? CarTypeId { get; set; }

    public Guid? ColorId { get; set; }
}
