using Carpool.CoreApi.ApplicationCore.Models;
using FluentValidation;

namespace Carpool.CoreApi.ApplicationCore.Validations
{
    public class CreateCarModelValidation : AbstractValidator<CreateCarModel>
    {
        public CreateCarModelValidation()
        {
            #region required fields
            RuleFor(x => x.Name)
                .NotNull();
            RuleFor(x => x.ColorId)
                .NotNull();
            RuleFor(x => x.CarTypeId)
                .NotNull();
            RuleFor(x => x.Plate)
                .NotNull();
            #endregion

        }
    }
}

