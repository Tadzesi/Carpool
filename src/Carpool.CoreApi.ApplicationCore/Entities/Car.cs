using Carpool.CoreApi.ApplicationCore.Dto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.ApplicationCore.Entities;

[Index(nameof(Plate), IsUnique = true)]
public class Car
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid? CarId { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Plate { get; set; }

    public CarType? CarType { get; set; }

    public Color? Color { get; set; }


    public CarDto ToDto()
    {
        return new CarDto()
        {
            CarId = CarId,
            Name = Name,
            Plate = Plate,
            CarType = CarType?.ToDto(),
            Color = Color?.ToDto()
        };
    }

}
