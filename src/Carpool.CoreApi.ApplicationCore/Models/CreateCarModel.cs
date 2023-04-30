using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Core.Attributes;

namespace Carpool.CoreApi.ApplicationCore.Models;


[CustomValidator(typeof(CreateCarModelValidation))]
public class CreateCarModel
{
    public string? Name { get; set; }

    public string? Plate { get; set; }

    public Guid? CarTypeId { get; set; }

    public Guid? ColorId { get; set; }
}
