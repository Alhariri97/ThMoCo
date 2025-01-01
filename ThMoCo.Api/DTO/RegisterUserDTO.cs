namespace ThMoCo.Api.DTO;



public class RegisterUserDTO
{
    public Dictionary<string, object>? app_metadata { get; set; }
    public DateTime created_at { get; set; }
    public string email { get; set; }
    public bool email_verified { get; set; }
    public string? family_name { get; set; }
    public string? given_name { get; set; }
    public DateTime? last_password_reset { get; set; }
    public string? name { get; set; }
    public string? nickname { get; set; }
    public string? phone_number { get; set; }
    public bool phone_verified { get; set; }
    public string? picture { get; set; }
    public string tenant { get; set; }
    public DateTime updated_at { get; set; }
    public string user_id { get; set; } // Auth0 User ID
    public Dictionary<string, object>? user_metadata { get; set; }
    public string? username { get; set; }
}
