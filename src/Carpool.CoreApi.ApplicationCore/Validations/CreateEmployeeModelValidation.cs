using Carpool.CoreApi.ApplicationCore.Models;
using FluentValidation;

namespace Carpool.CoreApi.ApplicationCore.Validations
{
    public class CreateEmployeeModelValidation : AbstractValidator<CreateEmployeeModel>
    {
        public CreateEmployeeModelValidation()
        {
            #region required fields
            RuleFor(x => x.FirstName)
                .NotNull();
            RuleFor(x => x.LastName)
                .NotNull();
            RuleFor(x => x.IsDriver)
                .NotNull();
            #endregion

        }
    }
}

