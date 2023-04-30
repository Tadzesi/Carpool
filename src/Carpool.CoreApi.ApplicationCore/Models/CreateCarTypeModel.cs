using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Core.Attributes;

namespace Carpool.CoreApi.ApplicationCore.Models;


[CustomValidator(typeof(CreateCarTypeModelValidation))]
public class CreateCarTypeModel
{

    public string? Name { get; set; }

    public string? Descritpion { get; set; }
}
