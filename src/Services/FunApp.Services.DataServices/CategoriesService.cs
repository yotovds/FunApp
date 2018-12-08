using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Models;

namespace FunApp.Services.DataServices
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoriesService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }        

        public IEnumerable<IdAndNameViewModel> GetAll()
        {
            var categories = this.categoryRepository
                .All()
                .OrderBy(c => c.Name)
                .Select(c => new IdAndNameViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            return categories;
        }

        public bool IsCategoryIdValid(int categoryId)
        {
            return this.categoryRepository.All().Any(c => c.Id == categoryId);
        }
    }
}
