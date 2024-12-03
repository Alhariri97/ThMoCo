namespace ThMoCo.Api.Data
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product? GetProductById(int id);
    }
}
