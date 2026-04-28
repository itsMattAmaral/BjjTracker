namespace BjjTracker.Application.Authentication;

public interface IJwtAuthenticationHelper
{
	string GenerateJwtToken(Domain.Entities.User? user);
}