namespace ThMoCo.Api.DTO;


public class AppUser
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? UserAuthId { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public double? Fund { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public string? Provider { get; set; } // e.g., "auth0", "google", "facebook"
    public string? Role { get; set; } // Or use an enum
    public bool? IsEmailVerified { get; set; }

    // Foreign keys
    public int? PaymentCardId { get; set; }
    public int? AddressId { get; set; }

    public PaymentCard? PaymentCard { get; set; } // One-to-one relationship
    public Address? Address { get; set; } // One-to-one relationship
}
