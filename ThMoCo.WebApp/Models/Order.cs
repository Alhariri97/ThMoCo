namespace ThMoCo.WebApp.Models;

public class Order
{
    public int Id { get; set; }
    public string AccountName { get; set; }
    public string CardNumber { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime When { get; set; }
    public string ProductName { get; set; }
    public string ProductEan { get; set; }
    public decimal TotalPrice { get; set; }
}
