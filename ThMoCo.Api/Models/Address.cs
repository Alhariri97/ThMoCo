namespace ThMoCo.Api.Models;

public class Address
{
    public int Id { get; set; }
    public string UserId { get; set; }       // Reference to the owner user
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }

}
