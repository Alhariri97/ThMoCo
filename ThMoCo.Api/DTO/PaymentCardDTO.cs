﻿namespace ThMoCo.Api.DTO;

public class PaymentCardDTO
{
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpiryDate { get; set; }
    public string Cvv { get; set; }
}
