namespace Carpool.CoreApi.ApplicationCore.Dto;

public class RideDto
{

    public Guid? RideId { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public CarDto? Car { get; set; }

    public virtual ICollection<EmployeeDto>? Employees { get; set; } = new List<EmployeeDto>();

    public RidePlanDto? RidePlan { get; set; }

}
