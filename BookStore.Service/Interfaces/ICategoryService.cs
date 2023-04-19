using BookStore.Models.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interfaces
{
    public interface ICategoryService 
    {
        Task<List<CategoryViewModel>> GetSubCategory();
        Task<CategoryViewModel> GetBookByCategoryId(Guid cateId, int page, int pageSize);
    }
}
