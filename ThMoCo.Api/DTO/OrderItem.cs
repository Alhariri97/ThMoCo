using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThMoCo.Api.DTO;

public class OrderItem
{
    [Key]  
    public int Id { get; set; } // Do NOT manually set Identity

    public int OrderId { get; set; } //  Foreign Key

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")] //  Ensures decimal precision
    public decimal PricePerUnit { get; set; }
}
