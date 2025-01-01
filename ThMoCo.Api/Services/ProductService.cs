using System.Linq;
using System.Threading.Tasks;
using ThMoCo.Api.Data;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ThMoCo.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsContext _context;

        public ProductService(ProductsContext context)
        {
            _context = context;
        }

        public List<ProductDTO> GetProducts(string? search, string? category, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                var query = _context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
                }

                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(p => p.Category == category);
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching products: {ex.Message}");
                throw new Exception($"Error getting data from the database. {ex.Message}");
            }
        }

        public ProductDTO? GetProductById(int id)
        {
            try
            {
                return _context.Products.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching product with ID {id}: {ex.Message}");
                throw new Exception("Error getting data from the database.");
            }
        }

        public async Task<List<ProductDTO>> GetStockStatus()
        {
            try
            {
                var stockStatus = await _context.Products
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        IsAvailable = p.StockQuantity > 0 
                    })
                    .ToListAsync();

                return stockStatus;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching stock status: {ex.Message}");
                throw new Exception("Error getting data from the database.");
            }
        }

        public async Task UpdateProductCatalog(List<ProductDTO> updatedProducts)
        {
            try
            {
                foreach (var product in updatedProducts)
                {
                    var existingProduct = _context.Products
                        .FirstOrDefault(p => p.Name == product.Name);

                    if (existingProduct != null)
                    {
                        // Update the existing product's price and increase stock quantity
                        existingProduct.Price = product.Price;
                        existingProduct.StockQuantity += product.StockQuantity; // Increase stock quantity
                    }
                    else
                    {
                        // Add new product if it doesn't exist
                        _context.Products.Add(new ProductDTO
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price * 1.1m,
                            Category = product.Category,
                            StockQuantity = product.StockQuantity
                        });
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating product catalog: {ex.Message}");
                throw new Exception("Error updating the product catalog.");
            }
        }

        public List<string> GetProductCategories()
        {
            try
            {
                return _context.Products
                    .Select(p => p.Category)
                    .Distinct()
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching product categories: {ex.Message}");
                throw new Exception("Error getting data from the database.");
            }
        }
    }
}
