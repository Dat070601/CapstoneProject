using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IBookRepository bookRepository;
        public CategoryService(
            IUnitOfWork unitOfWork, 
            IMapperCustom mapperCustom,
            ICategoryRepository categoryRepository,
            IBookRepository bookRepository) : base(unitOfWork, mapperCustom)
        {
            this.categoryRepository = categoryRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<CategoryViewModel> GetBookByCategoryId(Guid cateId, int page, int pageSize)
        {
            var findCategory = await categoryRepository.GetCategory(cateId);
            if (findCategory.SubId != null)
            {
                var findBook = await bookRepository.GetAllPaging().Where(b => b.CategoryId == cateId).OrderBy(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                var mapBook = mapperCustom.MapBookPagging(findBook);
                var result = new CategoryViewModel
                {
                    CategoryId = cateId,
                    SubId = findCategory.SubId!.Value,
                    Books = mapBook
                };
                return result;
            }
            var findCateChild = await categoryRepository.GetCategoryBySubCategory(findCategory.Id);
            var listBook = new List<BookViewModel>();
            foreach (var item in findCateChild)
            {
                var findBook = await bookRepository.GetAllPaging().Where(b => b.CategoryId == item.Id).OrderBy(b => b.Id).Skip((page - 1) * pageSize/(findCateChild.Count)).Take(pageSize/(findCateChild.Count)).ToListAsync();
                var mapBook = mapperCustom.MapBookPagging(findBook);
                listBook.AddRange(mapBook);
            }
            var resultBook = new CategoryViewModel
            {
                CategoryId = cateId,
                NameCategory =findCategory.CategoryName,
                Books = listBook,
            };
            return resultBook;
        }

        public async Task<List<CategoryViewModel>> GetSubCategory()
        {
            var categoryViewModel = await categoryRepository.GetSubCategory();
            var listCate = new List<CategoryViewModel>();
            foreach (var item in categoryViewModel)
            {
                var categoryChild = await getCategoryChild(item.Id);
                var category = new CategoryViewModel
                {
                    CategoryId = item.Id,
                    NameCategory = item.CategoryName,
                    Categories = categoryChild
                };
                listCate.Add(category);
            }
            return listCate;
        }

        private async Task<List<CategoryViewModel>> getCategoryChild(Guid id)
        {
            var categoryChild = await categoryRepository.GetCategoryBySubCategory(id);
            var listChild = new List<CategoryViewModel>();
            foreach (var item in categoryChild)
            {
                var category = new CategoryViewModel
                {
                    SubId = item.SubId!.Value,
                    CategoryId = item.Id,
                    NameCategory = item.CategoryName,
                };
                listChild.Add(category);
            }
            return listChild;
        }

        
    }
}
