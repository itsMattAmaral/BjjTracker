namespace BjjTracker.Application.AttendanceRequest.Queries.Dtos;

public class AttendanceRequestDto
{
	public int ClassId { get; set; }
	public int StudentId { get; set; }
	public bool Attended { get; set; }
}