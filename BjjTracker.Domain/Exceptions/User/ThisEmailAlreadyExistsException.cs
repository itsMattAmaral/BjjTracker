namespace BjjTracker.Domain.Exceptions.User;

public class ThisEmailAlreadyExistsException : Exception
{
	public ThisEmailAlreadyExistsException() : base("This email already exists.")
	{
	}
	
	public ThisEmailAlreadyExistsException(string message) : base(message)
	{
	}
	
	public ThisEmailAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
	{
	}
}