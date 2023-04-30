using Carpool.CoreApi.ApplicationCore.Queries;
using FluentValidation;

namespace Carpool.CoreApi.ApplicationCore.Validations
{
    public class DateTimePickerPeriodQueryValidator : AbstractValidator<DateTimePickerPeriodQuery>
    {
        public DateTimePickerPeriodQueryValidator()
        {
            #region required fields
            RuleFor(x => x.StartDate)
                .NotNull()
                .LessThanOrEqualTo(x => x.EndDate)
                .GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.EndDate)
                .NotNull()
                .GreaterThanOrEqualTo(x => x.StartDate);

            #endregion

        }
    }
}

