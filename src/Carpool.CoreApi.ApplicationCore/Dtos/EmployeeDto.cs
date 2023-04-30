namespace Carpool.CoreApi.ApplicationCore.Dto;

public class EmployeeDto
{

    public Guid? EmployeeId { get; set; } = Guid.NewGuid();

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public bool? IsDriver { get; set; }
}
