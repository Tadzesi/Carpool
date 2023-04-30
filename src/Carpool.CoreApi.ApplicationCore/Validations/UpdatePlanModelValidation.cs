using Carpool.CoreApi.ApplicationCore.Models;
using FluentValidation;

namespace Carpool.CoreApi.ApplicationCore.Validations
{
    public class UpdatePlanModelValidation : AbstractValidator<UpdatePlanModel>
    {
        public UpdatePlanModelValidation()
        {
            #region required fields
            RuleFor(x => x.EndDate)
                .NotNull();
            RuleFor(x => x.StartDate)
                .NotNull();
            RuleFor(x => x.EndLocation)
                .NotNull();
            RuleFor(x => x.StartLocation)
                .NotNull();
            #endregion

        }
    }
}

