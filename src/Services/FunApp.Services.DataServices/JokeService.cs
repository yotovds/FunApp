using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Models;
using FunApp.Services.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunApp.Services.DataServices
{
    public class JokeService : IJokeService
    {
        private readonly IRepository<Joke> jokesRepository;

        public JokeService(IRepository<Joke> jokesRepository)
        {
            this.jokesRepository = jokesRepository;
        }

        public IEnumerable<IndexJokeViewModel> GetRandomJokes(int count)
        {
            var jokes = this.jokesRepository
                .All()
                .OrderBy(x => Guid.NewGuid()) // randon ordering
                .Select(j => new IndexJokeViewModel
                {
                    Content = j.Content,
                    CategoryName = j.Category.Name
                })
                .Take(count)
                .ToList();

            return jokes;
        }

        public int GetCount()
        {
            return this.jokesRepository.All().Count();
        }
    }
}
