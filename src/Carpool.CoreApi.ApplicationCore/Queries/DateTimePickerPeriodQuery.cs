using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Core.Attributes;

namespace Carpool.CoreApi.ApplicationCore.Queries;

[CustomValidator(typeof(DateTimePickerPeriodQueryValidator))]
public class DateTimePickerPeriodQuery
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
