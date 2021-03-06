using Application.Features.Categories.Queries.GetCategories;
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
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private ISortHelper<Category> _sortHelper;

        public CategoryRepository
        (
            RepositoryContext repositoryContext,
            ISortHelper<Category> sortHelper
        ) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<Category>> GetPagedListAsync(GetCategoriesQuery categoriesQuery)
        {
            var categories = Enumerable.Empty<Category>().AsQueryable();

            ApplyFilters(ref categories, categoriesQuery);

            PerformSearch(ref categories, categoriesQuery.SearchTerm);

            var sortedCategories = _sortHelper.ApplySort(categories, categoriesQuery.OrderBy);

            return await Task.Run(() =>
                PagedList<Category>.ToPagedList
                (
                    sortedCategories,
                    categoriesQuery.PageNumber,
                    categoriesQuery.PageSize)
                );
        }


        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await BaseFindByCondition(product => product.Id.Equals(id))
                .FirstOrDefaultAsync();
        }


        public async Task<bool> ExistAsync(Category product)
        {
            return await BaseFindByCondition(x => x.Name == product.Name && x.Description == product.Description)
                .AnyAsync();
        }

        public async Task CreateAsync(Category product)
        {
            await BaseCreateAsync(product);
        }

        public async Task UpdateAsync(Category product)
        {
            await BaseUpdateAsync(product);
        }

        public async Task UpdateAsync(IEnumerable<Category> categories)
        {
            await BaseUpdateAsync(categories);
        }

        public async Task DeleteAsync(Category product)
        {
            await BaseDeleteAsync(product);
        }

        private void ApplyFilters(ref IQueryable<Category> categories, GetCategoriesQuery categoriesQuery)
        {
            categories = BaseFindAll();

            /*
            if (categoriesQuery.MinCreateAt != null)
            {
                categories = categories.Where(x => x.CreateAt >= categoriesQuery.MinCreateAt);
            }

            if (categoriesQuery.MaxCreateAt != null)
            {
                categories = categories.Where(x => x.CreateAt < categoriesQuery.MaxCreateAt);
            }
            */
        }

        private void PerformSearch(ref IQueryable<Category> categories, string searchTerm)
        {
            if (!categories.Any() || string.IsNullOrWhiteSpace(searchTerm)) return;

            categories = categories.Where(x => x.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
        }

    }
}
