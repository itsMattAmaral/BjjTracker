namespace BjjTracker.Domain.Entities;

public class Class
{
	public int Id { get; set; }
	public int SchoolId { get; set; }
	public School? School { get; set; }
	public int TeacherId { get; set; }
	public required Teacher Teacher { get; set; }
	public DateTime BeginDate { get; set; } = DateTime.UtcNow;
	public DateTime EndDate { get; set; } = DateTime.UtcNow.AddHours(1);
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public List<AttendanceRequest> AttendanceRequests { get; set; } = [];

	public void AddAttendanceRequest(AttendanceRequest attendanceRequest)
	{
		AttendanceRequests.Add(attendanceRequest);
		UpdatedAt = DateTime.UtcNow;
	}

	public void RemoveAttendanceRequest(AttendanceRequest attendanceRequest)
	{
		AttendanceRequests.Remove(attendanceRequest);
		UpdatedAt = DateTime.UtcNow;
	}

	public void ApproveAttendedRequest(int studentId)
	{
		var request = AttendanceRequests.FirstOrDefault(r => r.StudentId == studentId);
		if (request is null) return;

		request.Attended = true;
		UpdatedAt = DateTime.UtcNow;
	}
	
	public void SetClassDates(DateTime beginDate, DateTime endDate)
	{
		BeginDate = beginDate;
		EndDate = endDate;
		UpdatedAt = DateTime.UtcNow;
	}

	public void SetTeacher(Teacher teacher)
	{
		Teacher = teacher;
		UpdatedAt = DateTime.UtcNow;
	}
}