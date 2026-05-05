namespace BjjTracker.Domain.Entities;

public class AttendanceRequest
{
	public int ClassId { get; set; }
	public Class? Class { get; set; }
	public int StudentId { get; set; }
	public Student? Student { get; set; }
	public bool Attended { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? UpdatedAt { get; set; }
	
	public void ApproveAttendedRequest()
	{
		Attended = true;
		UpdatedAt = DateTime.UtcNow;
	}
}