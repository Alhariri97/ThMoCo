namespace ThMoCo.Api.DTO;



public class RegisterUserDTO
{
    public Dictionary<string, object>? AppMetadata { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public string? FamilyName { get; set; }
    public string? GivenName { get; set; }
    public DateTime? LastPasswordReset { get; set; }
    public string? Name { get; set; }
    public string? Nickname { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneVerified { get; set; }
    public string? Picture { get; set; }
    public string Tenant { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UserId { get; set; } // Auth0 User ID
    public Dictionary<string, object>? UserMetadata { get; set; }
    public string? Username { get; set; }
}
