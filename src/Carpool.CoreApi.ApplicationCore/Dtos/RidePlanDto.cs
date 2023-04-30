namespace Carpool.CoreApi.ApplicationCore.Dto;

public class RidePlanDto
{
    public Guid? RideId { get; set; } = Guid.NewGuid();

    public string? StartLocaion { get; set; }
    public string? EndLocaion { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

}
