using ThMoCo.Api.DTO;

namespace ThMoCo.Api.IServices
{
    public interface IProductService
    {
        List<ProductDTO> GetProducts();
        ProductDTO? GetProductById(int id);
    }
}
