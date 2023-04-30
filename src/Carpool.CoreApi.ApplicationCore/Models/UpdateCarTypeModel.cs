using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Core.Attributes;

namespace Carpool.CoreApi.ApplicationCore.Models;


[CustomValidator(typeof(UpdateCarTypeModelValidation))]
public class UpdateCarTypeModel
{
    public Guid? CarTypeId { get; set; }
    public string? Name { get; set; }
    public string? Descritpion { get; set; }
}
