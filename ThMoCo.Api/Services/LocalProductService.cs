using Microsoft.EntityFrameworkCore;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;


namespace ThMoCo.Api.Services
{
    public class LocalProductService : IProductService
    {
        // Sample list of products for demo purposes
        private readonly List<ProductDTO> _products = new List<ProductDTO>
        {
            new ProductDTO { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics", StockQuantity = 10, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-6), UpdatedDate = DateTime.Now, Description = "A high-performance laptop for work and gaming." },
            new ProductDTO { Id = 2, Name = "Smartphone", Price = 799.99m, Category = "Electronics", StockQuantity = 20, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-3), UpdatedDate = DateTime.Now, Description = "A modern smartphone with excellent camera quality." },
            new ProductDTO { Id = 3, Name = "Headphones", Price = 199.99m, Category = "Accessories", StockQuantity = 50, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-1), UpdatedDate = DateTime.Now, Description = "Noise-canceling headphones for immersive sound." },
            new ProductDTO { Id = 4, Name = "Monitor", Price = 299.99m, Category = "Electronics", StockQuantity = 5, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-2), UpdatedDate = DateTime.Now, Description = "A 24-inch monitor with stunning picture quality." },
            new ProductDTO { Id = 5, Name = "Monitor 2", Price = 299.99m, Category = "Electronics", StockQuantity = 5, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-2), UpdatedDate = DateTime.Now, Description = "Another 24-inch monitor, great for dual setups." },
            new ProductDTO { Id = 6, Name = "Monitor 3", Price = 299.99m, Category = "Electronics", StockQuantity = 5, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-2), UpdatedDate = DateTime.Now, Description = "A 27-inch monitor with ultra-clear resolution." },
            new ProductDTO { Id = 7, Name = "Tablet", Price = 499.99m, Category = "Electronics", StockQuantity = 15, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-5), UpdatedDate = DateTime.Now, Description = "A lightweight tablet, perfect for reading and browsing." },
            new ProductDTO { Id = 8, Name = "Gaming Chair", Price = 199.99m, Category = "Furniture", StockQuantity = 25, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-4), UpdatedDate = DateTime.Now, Description = "Ergonomic gaming chair for extended comfort." },
            new ProductDTO { Id = 9, Name = "Keyboard", Price = 89.99m, Category = "Accessories", StockQuantity = 30, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-6), UpdatedDate = DateTime.Now, Description = "Mechanical keyboard with customizable RGB lighting." },
            new ProductDTO { Id = 10, Name = "Wireless Mouse", Price = 49.99m, Category = "Accessories", StockQuantity = 40, IsAvailable = true, ImageUrl = null, CreatedDate = DateTime.Now.AddMonths(-2), UpdatedDate = DateTime.Now, Description = "A wireless mouse with high precision and long battery life." }
        };

        // Retrieves a list of products based on optional filtering parameters
        public List<ProductDTO> GetProducts(string? search, string? category, decimal? minPrice, decimal? maxPrice)
        {
            if (_products == null || !_products.Any())
            {
                throw new InvalidOperationException("No products available for querying.");
            }

            var query = _products.AsQueryable();

            // Search by name or description
            if (!string.IsNullOrEmpty(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(p =>
                    (p.Name != null && p.Name.ToLower().Contains(lowerSearch)) ||
                    (p.Description != null && p.Description.ToLower().Contains(lowerSearch)));
            }


            // Filter by category
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by price range
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return query.ToList();
        }


        // Retrieves a single product by its ID
        public ProductDTO? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        // Fetches stock status for all products
        public async Task<List<ProductDTO>> GetStockStatus()
        {
            return await Task.FromResult(_products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price * 1.1m,
                Category = p.Category,
                StockQuantity = p.StockQuantity,
                IsAvailable = p.StockQuantity > 0, // If stock quantity is more than 0, the product is available
                ImageUrl = p.ImageUrl,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate
            }).ToList());
        }

        // Updates the product catalog and prices, typically from a supplier source (Admin-only access)
        public async Task UpdateProductCatalog(List<ProductDTO> updatedProducts)
        {
            foreach (var updatedProduct in updatedProducts)
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
                if (existingProduct != null)
                {
                    // Update the existing product's price and increase stock quantity
                    existingProduct.Price = updatedProduct.Price;
                    existingProduct.StockQuantity += updatedProduct.StockQuantity; // Increase stock quantity
                }
                else
                {
                    // Add new product if it doesn't exist
                    _products.Add(new ProductDTO
                    {
                        Name = updatedProduct.Name,
                        Description = updatedProduct.Description,
                        Price = updatedProduct.Price ,
                        Category = updatedProduct.Category,
                        StockQuantity = updatedProduct.StockQuantity
                    });
                }
            }

            await Task.CompletedTask;  // Simulate async operation
        }

        // Retrieves a list of distinct product categories
        public List<string> GetProductCategories()
        {
            return _products.Select(p => p.Category).Distinct().ToList();
        }
        public async Task<bool> UpdateProduct(ProductDTO updatedProduct)
        {
            if (updatedProduct == null)
            {
                throw new ArgumentNullException(nameof(updatedProduct), "Updated product cannot be null.");
            }

            if (updatedProduct.Id <= 0)
            {
                throw new ArgumentException("Invalid Product ID.");
            }

            if (updatedProduct.StockQuantity < 0)
            {
                updatedProduct.StockQuantity = 0; // Prevent negative stock
            }

            try
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
                if (existingProduct == null)
                {
                    return false; // Product not found
                }

                // Update product fields
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Category = updatedProduct.Category;
                existingProduct.StockQuantity = updatedProduct.StockQuantity;
                existingProduct.IsAvailable = updatedProduct.IsAvailable;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.UpdatedDate = DateTime.UtcNow;

                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
                return false;
            }
        }


    }
}
