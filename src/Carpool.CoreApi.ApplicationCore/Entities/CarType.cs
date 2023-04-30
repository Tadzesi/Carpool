using Carpool.CoreApi.ApplicationCore.Dto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.ApplicationCore.Entities;


[Index(nameof(Name), IsUnique = true)]
public class CarType
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid? CarTypeId { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Description { get; set; }

    public CarTypeDto ToDto()
    {
        return new CarTypeDto()
        {
            Name = Name,
            Description = Description,
            CarTypeId = CarTypeId
        };
    }
}
