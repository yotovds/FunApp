﻿using FunApp.Services.DataServices;
using FunApp.Services.MachineLearning;
using FunApp.Services.Models;
using FunApp.Services.Models.Jokes;
using FunApp.Web.Models.Jokes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApp.Web.Controllers
{
    public class JokesController : BaseController
    {
        private readonly ICategoriesService categoriesServices;
        private readonly IJokeService jokeService;
        private readonly IJokesCategorizer jokesCategorizer;

        public JokesController(ICategoriesService categoriesServices, IJokeService jokeService, IJokesCategorizer jokesCategorizer)
        {
            this.categoriesServices = categoriesServices;
            this.jokeService = jokeService;
            this.jokesCategorizer = jokesCategorizer;
        }

        [Authorize]
        public IActionResult Create()
        {
            this.ViewData["Categories"] = this.categoriesServices
                .GetAll()
                .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.NameAndCount
                    });

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJokeInputModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var categoryId = await this.jokeService.Create(input.CategoryId, input.Content);

            return this.RedirectToAction("Details", new { id = categoryId });
        }

        public IActionResult Details(int id)
        {
            var joke = this.jokeService.GetJokeById<JokeDetailsViewModel>(id);

            return this.View(joke);
        }
        [HttpPost]
        public SuggestCategoryResult SuggestCategory(string joke)
        {
            var category = this.jokesCategorizer
                .Categorize("MlModels/JokesCategoryModel.zip", joke);

            var categoryId = this.categoriesServices.GetCategoryId(category);

            return new SuggestCategoryResult { CategoryId = categoryId ?? 0, CategoryName = category };
        }
    }

    public class SuggestCategoryResult
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
