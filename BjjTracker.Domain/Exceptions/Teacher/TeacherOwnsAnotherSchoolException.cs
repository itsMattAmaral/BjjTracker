namespace BjjTracker.Domain.Exceptions.Teacher;

public class TeacherOwnsAnotherSchoolException(int teacherId)
	: Exception($"Teacher with id {teacherId} already owns another school.");