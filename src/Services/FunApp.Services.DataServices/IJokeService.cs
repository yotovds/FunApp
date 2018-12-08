using FunApp.Data.Models;
using FunApp.Services.Models;
using System.Collections.Generic;

namespace FunApp.Services.DataServices
{
    public interface IJokeService
    {
        IEnumerable<IndexJokeViewModel> GetRandomJokes(int count);

        int GetCount();
    }
}
