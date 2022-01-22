using Application.Features.Products.Queries.GetProducts;
using Application.Interfaces;
using Application.Parameters;
using Application.Wrappers;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private ISortHelper<Product> _sortHelper;

        public ProductRepository
        (
            RepositoryContext repositoryContext,
            ISortHelper<Product> sortHelper
        ) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<Product>> GetProductsAsync(GetProductsQuery productsQuery)
        {
            var products = Enumerable.Empty<Product>().AsQueryable();

            ApplyFilters(ref products, productsQuery);

            PerformSearch(ref products, productsQuery.SearchTerm);

            var sortedProducts = _sortHelper.ApplySort(products, productsQuery.OrderBy);

            return await Task.Run(() =>
                PagedList<Product>.ToPagedList
                (
                    sortedProducts,
                    productsQuery.PageNumber,
                    productsQuery.PageSize)
                );
        }


        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await FindByCondition(product => product.Id.Equals(id))
                .FirstOrDefaultAsync();
        }


        public async Task<bool> ProductExistAsync(Product product)
        {
            return await FindByCondition(x => x.Name == product.Name && x.Barcode == product.Barcode && x.Description == product.Description)
                .AnyAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await CreateAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await UpdateAsync(product);
        }

        public async Task UpdateProductAsync(IEnumerable<Product> products)
        {
            await UpdateAsync(products);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await DeleteAsync(product);
        }

        #region ApplyFilters and PerformSearch Region
        private void ApplyFilters(ref IQueryable<Product> products, GetProductsQuery productsQuery)
        {
            products = FindAll();

            /*
            if (productsQuery.MinCreateAt != null)
            {
                products = products.Where(x => x.CreateAt >= productsQuery.MinCreateAt);
            }

            if (productsQuery.MaxCreateAt != null)
            {
                products = products.Where(x => x.CreateAt < productsQuery.MaxCreateAt);
            }
            */
        }

        private void PerformSearch(ref IQueryable<Product> products, string searchTerm)
        {
            if (!products.Any() || string.IsNullOrWhiteSpace(searchTerm)) return;

            products = products.Where(x => x.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
        }

        #endregion

    }
}
