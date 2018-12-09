using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Mapping;
using FunApp.Services.Models;
using FunApp.Services.Models.Categories;

namespace FunApp.Services.DataServices
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoriesService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }        

        public IEnumerable<CategoryIdAndNameViewModel> GetAll()
        {
            var categories = this.categoryRepository
                .All()
                .OrderBy(c => c.Name)
                .To<CategoryIdAndNameViewModel>()
                .ToList();

            return categories;
        }

        public bool IsCategoryIdValid(int categoryId)
        {
            return this.categoryRepository.All().Any(c => c.Id == categoryId);
        }
    }
}
