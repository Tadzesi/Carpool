using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carpool.CoreApi.ApplicationCore.Entities;

public class RideEmployees
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid? RideEmployeId { get; set; } = Guid.NewGuid();

    [ForeignKey("Employee")]
    public int? EmployeeId { get; set; }

    [ForeignKey("Ride")]
    public int? RideId { get; set; }

}
