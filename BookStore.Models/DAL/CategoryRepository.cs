using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DAL
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<Category> GetCategory(Guid categoryId)
        {
            return await GetQuery(ct => ct.Id == categoryId).SingleAsync();
        }

        public async Task<List<Category>> GetCategoryBySubCategory(Guid categoryId)
        {
            return await GetQuery(ct => ct.SubId == categoryId).ToListAsync();
        }

        public async Task<List<Category>> GetSubCategory()
        {
            return await GetQuery(ct => ct.SubId == null).ToListAsync();
        }
    }
}
