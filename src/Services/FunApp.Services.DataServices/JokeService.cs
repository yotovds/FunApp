using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Mapping;
using FunApp.Services.Models.Home;
using FunApp.Services.Models.Jokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApp.Services.DataServices
{
    public class JokeService : IJokeService
    {
        private readonly IRepository<Joke> jokesRepository;
        private readonly IRepository<Category> catecoryRepository;

        public JokeService(IRepository<Joke> jokesRepository, IRepository<Category> catecoriesRepository)
        {
            this.jokesRepository = jokesRepository;
            this.catecoryRepository = catecoriesRepository;
        }

        public IEnumerable<IndexJokeViewModel> GetRandomJokes(int count)
        {
            //var jokes = this.jokesRepository
            //    .All()
            //    .OrderBy(x => Guid.NewGuid()) // randon ordering
            //    .Select(j => new IndexJokeViewModel
            //    {
            //        Id = j.Id,
            //        Content = j.Content,
            //        CategoryName = j.Category.Name
            //    })
            //    .Take(count)
            //    .ToList();

            var jokes = this.jokesRepository
                .All()
                .OrderBy(x => Guid.NewGuid()) // randon ordering
                .To<IndexJokeViewModel>()
                .Take(count)
                .ToList();

            return jokes;
        }

        public int GetCount()
        {
            return this.jokesRepository.All().Count();
        }

        public async Task<int> Create(int categoryId, string jokeContent)
        {
            var joke = new Joke
            {
                CategoryId = categoryId,
                Content = jokeContent
            };

            await this.jokesRepository.AddAsync(joke);
            await this.jokesRepository.SaveChangesAsync();

            return joke.Id;
        }

        public JokeDetailsViewModel GetJokeById(int jokeId)
        {
            var joke = this.jokesRepository
                .All()
                .Where(j => j.Id == jokeId)
                .To<JokeDetailsViewModel>()
                .FirstOrDefault();

            return joke;
        }
    }
}
