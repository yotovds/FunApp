using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.DataServices;
using FunApp.Services.Models;
using FunApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace FunApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJokeService jokeService;

        public HomeController(IJokeService jokeService)
        {
            this.jokeService = jokeService;
        }

        public IActionResult Index()
        {
            var jokes = this.jokeService.GetRandomJokes(20);

            var viewModel = new IndexViewModel
            {
                Jokes = jokes
            };

            return this.View(viewModel);
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = $"My application has {this.jokeService.GetCount()} jokes.";

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
