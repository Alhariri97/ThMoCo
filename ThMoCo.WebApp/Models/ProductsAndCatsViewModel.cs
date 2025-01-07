using ThMoCo.WebApp.DTO;

namespace ThMoCo.WebApp.Models
{
    public class ProductsAndCatsViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public List<string> Categories { get; set; }
    }
}
