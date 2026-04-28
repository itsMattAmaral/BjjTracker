namespace BjjTracker.Domain.Entities;

public class School(string document, string name, string contactEmail, string? contactPhone)
{
	public int Id { get; set; }
	public readonly string Document = document;
	public string Name { get; set; } = name;
	public string ContactEmail { get; set; } = contactEmail;
	public string? ContactPhone { get; set; } = contactPhone;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public List<Teacher> Teachers { get; set; } = [];
	public List<Student> Students { get; set; } = [];
	public List<Class> Classes { get; set; } = [];
	
	public void SetTeachers(List<Teacher> teachers)
	{
		foreach (var teacher in teachers)
		{
			teacher.UpdateSchool(Id);
		}
		
		Teachers = teachers;
		UpdatedAt = DateTime.UtcNow;
	}

	public void SetStudents(List<Student> students)
	{
		foreach (var student in students)
		{
			student.UpdateSchool(Id);
		}
		Students = students;
		UpdatedAt = DateTime.UtcNow;
	}

	public void UpdateContactEmail(string contactEmail)
	{
		ContactEmail = contactEmail;
		UpdatedAt = DateTime.UtcNow;
	}

	public void UpdateContactPhone(string contactPhone)
	{
		ContactPhone = contactPhone;
		UpdatedAt = DateTime.UtcNow;
	}

	public void UpdateName(string name)
	{
		Name = name;
		UpdatedAt = DateTime.UtcNow;
	}

	public void AddTeacher(Teacher teacher)
	{
		Teachers.Add(teacher);
		UpdatedAt = DateTime.UtcNow;
	}
	
	public void AddStudent(Student student)
	{
		Students.Add(student);
		UpdatedAt = DateTime.UtcNow;
	}
	
	public void AddClass(Class @class)
	{
		Classes.Add(@class);
		UpdatedAt = DateTime.UtcNow;
	}
	
	public void RemoveTeacher(Teacher teacher)
	{
		Teachers.Remove(teacher);
		UpdatedAt = DateTime.UtcNow;
	}
	
	public void RemoveStudent(Student student)
	{
		Students.Remove(student);
		UpdatedAt = DateTime.UtcNow;
	}
	
	public void RemoveClass(Class @class)
	{
		Classes.Remove(@class);
		UpdatedAt = DateTime.UtcNow;
	}
}