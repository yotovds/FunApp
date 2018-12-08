using FunApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunApp.Services.DataServices
{
    public interface ICategoriesServices
    {
        IEnumerable<IdAndNameViewModel> GetAll();
    }
}
