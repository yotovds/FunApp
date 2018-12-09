using FunApp.Data.Models;
using FunApp.Services.Models;
using FunApp.Services.Models.Home;
using FunApp.Services.Models.Jokes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunApp.Services.DataServices
{
    public interface IJokeService
    {
        IEnumerable<IndexJokeViewModel> GetRandomJokes(int count);

        int GetCount();

        Task<int> Create(int categoryId, string jokeContent);

        TViewModel GetJokeById<TViewModel>(int jokeId);
    }
}
