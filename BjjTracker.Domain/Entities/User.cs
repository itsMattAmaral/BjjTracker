using BjjTracker.Domain.Enums;

namespace BjjTracker.Domain.Entities;

public abstract class User()
{
	public int Id { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
	
	public int? SchoolId { get; set; }
	public School? School { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	
	public DateTime? DateOfBirth { get; set; }
	
	public int? Weight { get; set; }
	
	public int? Height { get; set; }
	
	public Roles Role { get; set; }
	
	public BeltColors BeltColor { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

	public User(string email, string password, Roles role) : this()
	{
		Email = email.ToLower();
		Password = password;
		Role = role;
		CreatedAt = DateTime.UtcNow;
	}
	
	public void UpdateFirstName(string firstName)
	{
		FirstName = firstName;
		UpdatedAt = DateTime.UtcNow;
	}

	public void UpdateLastName(string lastName)
	{
		LastName = lastName;
		UpdatedAt = DateTime.UtcNow;
	}

	public void UpdateEmail(string newEmail)
	{
		Email = newEmail.ToLower();
		UpdatedAt = DateTime.UtcNow;
	}

	public void UpdatePassword(string newPassword)
	{
		Password = newPassword;
		UpdatedAt = DateTime.UtcNow;
	}

	public void UpdateRole(Roles role)
	{
		Role = role;
		UpdatedAt = DateTime.UtcNow;
	}
	
	public void UpdateWeight(int weight)
	{
		Weight = weight;
		UpdatedAt = DateTime.UtcNow;
	}
	
	public  void UpdateHeight(int height)
	{
		Height = height;
		UpdatedAt = DateTime.UtcNow;
	}
}