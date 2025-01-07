using ThMoCo.WebApp.DTO;

namespace ThMoCo.WebApp.Models
{
    public class HomeViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public List<string> Categories { get; set; }
    }
}
