using BjjTracker.Domain.Enums;

namespace BjjTracker.Domain.Entities;

public class Student() : User
{
	public int ClassesAttended { get; set; }

	public void UpdateSchool(int schoolId)
	{
		SchoolId = schoolId;
		UpdatedAt = DateTime.UtcNow;
	}
}