using Carpool.CoreApi.ApplicationCore.Models;
using FluentValidation;

namespace Carpool.CoreApi.ApplicationCore.Validations
{
    public class UpdateEmployeeModelValidation : AbstractValidator<UpdateEmployeeModel>
    {
        public UpdateEmployeeModelValidation()
        {
            #region required fields
            RuleFor(x => x.EmployeeId)
                .NotNull();
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

