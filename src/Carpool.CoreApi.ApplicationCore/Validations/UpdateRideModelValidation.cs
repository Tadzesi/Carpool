using Carpool.CoreApi.ApplicationCore.Models;
using FluentValidation;

namespace Carpool.CoreApi.ApplicationCore.Validations
{
    public class UpdateRideModelValidation : AbstractValidator<UpdateRideModel>
    {
        public UpdateRideModelValidation()
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

