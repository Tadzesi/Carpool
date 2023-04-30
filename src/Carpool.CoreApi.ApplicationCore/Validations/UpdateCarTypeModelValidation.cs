using Carpool.CoreApi.ApplicationCore.Models;
using FluentValidation;

namespace Carpool.CoreApi.ApplicationCore.Validations
{
    public class UpdateCarTypeModelValidation : AbstractValidator<UpdateCarTypeModel>
    {
        public UpdateCarTypeModelValidation()
        {
            #region required fields
            RuleFor(x => x.Name)
                .NotNull();
            RuleFor(x => x.Descritpion)
                .NotNull();
            #endregion

        }
    }
}

