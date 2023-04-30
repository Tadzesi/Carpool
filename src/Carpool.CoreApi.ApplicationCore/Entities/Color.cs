using Carpool.CoreApi.ApplicationCore.Dto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.ApplicationCore.Entities;

[Index(nameof(Hex), IsUnique = true)]
public class Color
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid? ColorId { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Hex { get; set; }

    public ColorDto ToDto()
    {
        return new ColorDto()
        {
            ColorId = ColorId,
            Name = Name,
            Description = Description,
            Hex = Hex
        };
    }
}
