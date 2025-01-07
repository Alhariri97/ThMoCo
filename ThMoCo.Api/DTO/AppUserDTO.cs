using System.Text.Json.Serialization;
using ThMoCo.Api.Models;

namespace ThMoCo.Api.DTO;


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
    public string? Provider { get; set; } // 
    public string? Role { get; set; } // User's role
    public bool? IsEmailVerified { get; set; }

    // Ensure a parameterless constructor exists
    public AppUserDTO() { }

    [JsonConstructor]
    public AppUserDTO(int id, string? name, string? email, string? photoUrl, string? phoneNumber,
                  decimal? fund, DateTime? updatedAt, DateTime? lastLogin, string? provider, string? role, bool? isEmailVerified)
    {
        Id = id;
        Name = name;
        Email = email;
        PhotoUrl = photoUrl;
        PhoneNumber = phoneNumber;
        Fund = fund;
        UpdatedAt = updatedAt;
        LastLogin = lastLogin;
        Provider = provider;
        Role = role;
        IsEmailVerified = isEmailVerified;
    }

    // Constructor to map AppUser to AppUserDTO
    public AppUserDTO(AppUser user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        PhotoUrl = user.PhotoUrl;
        PhoneNumber = user.PhoneNumber;
        Fund = user.Fund;
        UpdatedAt = user.UpdatedAt;
        LastLogin = user.LastLogin;
        Provider = user.Provider;
        Role = user.Role;
        IsEmailVerified = user.IsEmailVerified;
    }
}

