using Carpool.CoreApi.ApplicationCore.Dto;
using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.ApplicationCore.Entities;

public class RidePlan
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid? RideId { get; set; } = Guid.NewGuid();

    public string? StartLocation { get; set; }
    public string? EndLocation { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public RidePlanDto ToDto()
    {
        return new RidePlanDto()
        {
            RideId = RideId,
            StartLocaion = StartLocation,
            EndLocaion = EndLocation,
            StartDate = StartDate,
            EndDate = EndDate
        };
    }

}
