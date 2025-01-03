namespace ThMoCo.Api.DTO;

public class PaymentCard
{
    public int Id { get; set; }
    public string UserId { get; set; }            // Reference to the owner user
    public string CardNumber { get; set; }     // In real-world scenarios, store securely or partially
    public string CardHolderName { get; set; }
    public string ExpiryDate { get; set; }     // e.g., MM/YY
    public int Cvv { get; set; }

}
