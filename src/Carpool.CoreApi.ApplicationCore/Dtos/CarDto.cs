namespace Carpool.CoreApi.ApplicationCore.Dto;

public class CarDto
{
    public Guid? CarId { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Plate { get; set; }

    public CarTypeDto? CarType { get; set; }

    public ColorDto? Color { get; set; }
}
