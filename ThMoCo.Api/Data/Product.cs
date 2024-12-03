namespace ThMoCo.Api.Data;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Supplier { get; set; }
    public int StockQuantity { get; set; }
    public bool IsAvailable { get; set; }
    public string? Category { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
