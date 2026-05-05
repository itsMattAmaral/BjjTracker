namespace BjjTracker.Domain.Exceptions.Teacher;

public class TeacherAlreadyOwnsThisSchoolException(int teacherId, int schoolId)
	: Exception($"Teacher with id {teacherId} already owns school with id {schoolId}.");