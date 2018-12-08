using FunApp.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunApp.Services.DataServices
{
    public interface ICategoriesService
    {
        IEnumerable<IdAndNameViewModel> GetAll();

        bool IsCategoryIdValid(int categoryId);
    }
}
