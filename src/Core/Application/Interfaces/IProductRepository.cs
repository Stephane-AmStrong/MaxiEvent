using Application.Features.Products.Queries.GetProducts;
using Application.Parameters;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetProductsAsync(GetProductsQuery productsQuery);

        Task<Product> GetProductByIdAsync(Guid id);
        Task<bool> ProductExistAsync(Product product);

        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task UpdateProductAsync(IEnumerable<Product> products);
        Task DeleteProductAsync(Product product);
    }
}
