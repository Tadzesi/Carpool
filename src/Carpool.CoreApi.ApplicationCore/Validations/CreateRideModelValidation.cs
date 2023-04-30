using Carpool.CoreApi.ApplicationCore.Models;
using FluentValidation;

namespace Carpool.CoreApi.ApplicationCore.Validations
{
    public class CreateRiderModelValidation : AbstractValidator<CreateRideModel>
    {
        public CreateRiderModelValidation()
        {
            #region required fields
            RuleFor(x => x.Name)
                .NotNull();
            RuleFor(x => x.CarId)
                .NotNull();
            #endregion

        }
    }
}

