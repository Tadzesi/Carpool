using Carpool.CoreApi.ApplicationCore.Dto;
using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.ApplicationCore.Entities;

public class Ride
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid? RideId { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public Car? Car { get; set; }

    [Required]
    public RidePlan? RidePlan { get; set; }

    public RideDto ToDto()
    {
        return new RideDto()
        {
            Car = Car!.ToDto(),
            Name = Name,
            RideId = RideId,
            RidePlan = RidePlan!.ToDto()
        };
    }

}
