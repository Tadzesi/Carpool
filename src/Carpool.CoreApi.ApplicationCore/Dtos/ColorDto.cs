using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.ApplicationCore.Dto;

public class ColorDto
{

    [Required]
    public Guid? ColorId { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Hex { get; set; }
}
