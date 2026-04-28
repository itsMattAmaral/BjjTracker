namespace BjjTracker.Domain.Exceptions.School;

public class SchoolNotFoundException : Exception
{
	public SchoolNotFoundException() : base("School not found.")
	{
	}

	public SchoolNotFoundException(int id) : base($"School with id {id} not found.")
	{
	}
	
	public SchoolNotFoundException(string message) : base(message)
	{
	}
	
	public SchoolNotFoundException(string message, Exception innerException) : base(message, innerException)
	{
	}
}