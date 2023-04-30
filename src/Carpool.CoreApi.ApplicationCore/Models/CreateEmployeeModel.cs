using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Core.Attributes;

namespace Carpool.CoreApi.ApplicationCore.Models;


[CustomValidator(typeof(CreateEmployeeModelValidation))]
public class CreateEmployeeModel
{

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool? IsDriver { get; set; }
}
