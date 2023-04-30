using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.ApplicationCore.Dto;


public class CarTypeDto
{

    [Required]
    public Guid? CarTypeId { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Description { get; set; }
}
