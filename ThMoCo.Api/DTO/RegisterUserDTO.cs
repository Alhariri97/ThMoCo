namespace ThMoCo.Api.DTO;



public class RegisterUserDTO
{
    public DateTime created_at { get; set; } // User creation time
    public string email { get; set; } // User email
    public bool email_verified { get; set; } // Whether email is verified
    public string? family_name { get; set; } // User's family/last name
    public string? given_name { get; set; } // User's given/first name
    public string? name { get; set; } // Full name of the user
    public string? nickname { get; set; } // User nickname
    public string? phone_number { get; set; } // User phone number
    public bool phone_verified { get; set; } // Whether phone is verified
    public string? picture { get; set; } // URL to user's profile picture
    public string tenant { get; set; } // Auth0 tenant identifier
    public DateTime updated_at { get; set; } // Time of last profile update
    public string user_id { get; set; } // Unique identifier for the user
    public string? username { get; set; } // Username of the user
}
