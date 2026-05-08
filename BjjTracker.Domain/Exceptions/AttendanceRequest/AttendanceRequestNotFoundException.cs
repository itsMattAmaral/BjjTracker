namespace BjjTracker.Domain.Exceptions.AttendanceRequest;

public class AttendanceRequestNotFoundException : Exception
{
	public AttendanceRequestNotFoundException() : base("Attendance request not found.")
	{
	}
	
	public AttendanceRequestNotFoundException(string message) : base(message)
	{
	}
	
	public AttendanceRequestNotFoundException(string message, Exception innerException) : base(message, innerException)
	{
	}
}