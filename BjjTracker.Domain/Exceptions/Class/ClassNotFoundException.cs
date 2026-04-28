namespace BjjTracker.Domain.Exceptions.Class;

public class ClassNotFoundException : Exception
{
	public ClassNotFoundException()
	{
	}
	public ClassNotFoundException(string message) : base(message)
	{}
}