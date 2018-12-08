using FunApp.Services.DataServices;
using FunApp.Services.Models;
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

        public JokesController(ICategoriesService categoriesServices, IJokeService jokeService)
        {
            this.categoriesServices = categoriesServices;
            this.jokeService = jokeService;
        }

        [Authorize]
        public IActionResult Create()
        {
            this.ViewData["Categories"] = this.categoriesServices
                .GetAll()
                .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
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
            var joke = this.jokeService.GetJokeById(id);

            return this.View(joke);
        }
    }
}
