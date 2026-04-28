namespace BjjTracker.Domain.Exceptions.Class;

public class ClassDateConflictException : Exception
{
	public ClassDateConflictException(string message) : base(message)
	{
	}
	public ClassDateConflictException(string message, Exception innerException) : base(message, innerException)
	{
	}

	public ClassDateConflictException() : base("Date conflict exception")
	{
	}
}