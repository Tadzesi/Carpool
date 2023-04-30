using Carpool.CoreApi.ApplicationCore.Dto;
using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.ApplicationCore.Entities;

public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid? EmployeeId { get; set; } = Guid.NewGuid();

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    public bool? IsDriver { get; set; }

    public EmployeeDto ToDto()
    {
        return new EmployeeDto()
        {
            EmployeeId = EmployeeId,
            FirstName = FirstName,
            LastName = LastName,
            IsDriver = IsDriver
        };
    }
}
