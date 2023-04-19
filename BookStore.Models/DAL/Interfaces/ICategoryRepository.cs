using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetSubCategory();
        Task<List<Category>> GetCategoryBySubCategory(Guid categoryId);
        Task<Category> GetCategory(Guid categoryId);
    }
}
