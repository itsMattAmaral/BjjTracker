using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BjjTracker.Application.Authentication;

public class JwtAuthenticationHelper(IConfiguration configuration) : IJwtAuthenticationHelper
{
	private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

	public string GenerateJwtToken(Domain.Entities.User? user)
	{
		ArgumentNullException.ThrowIfNull(user);
		var claims = new List<Claim>
		{
			new (ClaimTypes.NameIdentifier, user.Id.ToString()),
			new (ClaimTypes.Email, user.Email),
			new (ClaimTypes.Role, user.Role.ToString())
		};
		
		var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"] ?? string.Empty));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var jwtToken = new JwtSecurityToken
			(
			claims: claims,
			notBefore: DateTime.UtcNow,
			expires: DateTime.UtcNow.AddDays(1),
			signingCredentials: credentials
			);
		return new JwtSecurityTokenHandler().WriteToken(jwtToken);
	}
}