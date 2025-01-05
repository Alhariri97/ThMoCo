namespace ThMoCo.WebApp.Models;

public class AppUserDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal? Fund { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public string? Provider { get; set; } // e.g., "auth0", "google", "facebook"
    public string? Role { get; set; } // User's role
    public bool? IsEmailVerified { get; set; }

}

