using BjjTracker.Domain.Enums;

namespace BjjTracker.Domain.Entities;

public class Teacher : User
{
	public bool IsSchoolOwner { get; set; }
	
	public new BeltColors BeltColor { get; set; } = BeltColors.Black;
	
	public void UpdateSchool(int schoolId)
	{
		SchoolId = schoolId;
		UpdatedAt = DateTime.UtcNow;
	}

	public void MarkAsSchoolOwner()
	{
		IsSchoolOwner = true;
		UpdatedAt = DateTime.UtcNow;
	}
	
	public void GraduateStudent(Student? student)
	{
		ArgumentNullException.ThrowIfNull(student);
        var studentCurrentBelt = student.BeltColor;
		if (studentCurrentBelt == BeltColors.Black) return;

		var nextBelt = (int)studentCurrentBelt + 1;
		student.BeltColor = (BeltColors)nextBelt;
		student.UpdatedAt = DateTime.UtcNow;
		UpdatedAt = DateTime.UtcNow;
	}
}