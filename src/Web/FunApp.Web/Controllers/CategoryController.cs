using AutoMapper;
using FunApp.Services.DataServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApp.Web.Controllers
{ 
    public class CategoryController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IJokeService jokesService;

        public CategoryController(ICategoriesService categoriesService, IJokeService jokesService)
        {
            this.categoriesService = categoriesService;
            this.jokesService = jokesService;
        }

        public IActionResult Index()
        {
            var categories = this.categoriesService.GetAll().ToList();

            return this.View(categories);
        }

        public IActionResult Details(int id, int? page)
        {
            //var jokesInCategory = this.jokesService.GetAllByCategory(id).ToList();

            //var nextPage = page ?? 1;

            //var pagedJokes = jokesInCategory.ToPagedList(nextPage, 4);

            //return this.View(pagedJokes);

            return this.View();
        }
    }
}
