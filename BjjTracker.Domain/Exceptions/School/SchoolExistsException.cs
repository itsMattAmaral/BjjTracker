namespace BjjTracker.Domain.Exceptions.School;

public class SchoolExistsException : Exception 
{
	public SchoolExistsException() : base("School already exists.")
	{
	}
}