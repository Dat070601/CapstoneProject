using BookStore.Models.DataViewModel;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
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
        Task<CategoryResponse> AddCategory(CategoryRequest cateReq);
        Task<CategoryResponse> AddSubCateogry(SubCategoryRequest cateReq);
        Task<CategoryResponse> UpdateNameCategory(SubCategoryRequest cateReq, Guid cateId); 
    }
} 