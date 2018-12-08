using FunApp.Services.DataServices;
using FunApp.Services.Models;
using FunApp.Services.Models.Jokes;
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
        private readonly ICategoriesServices categoriesServices;
        private readonly IJokeService jokeService;

        public JokesController(ICategoriesServices categoriesServices, IJokeService jokeService)
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
        public IActionResult Create(CreateJokeInputModel input)
        {
            return this.RedirectToAction("View", new {id = 0});
        }

        public IActionResult Details(int id)
        {
            return this.View();
        }
    }
}
