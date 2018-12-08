using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Web.Models;
using FunApp.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace FunApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Joke> jokesRepository;

        public HomeController(IRepository<Joke> jokesRepository)
        {
            this.jokesRepository = jokesRepository;
        }

        public IActionResult Index()
        {
            var jokes = this.jokesRepository                
                .All()
                .OrderBy(x => Guid.NewGuid()) // randon ordering
                .Select(j => new IndexJokeViewModel
                {
                    Content = j.Content,
                    CategoryName = j.Category.Name
                })
                .Take(20);

            var viewModel = new IndexViewModel
            {
                Jokes = jokes
            };

            return this.View(viewModel);
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = $"My application has {this.jokesRepository.All().Count()} jokes.";

            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
