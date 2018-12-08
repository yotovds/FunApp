using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Models;

namespace FunApp.Services.DataServices
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoriesServices(IRepository<Category> categoryRepository)
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
    }
}
