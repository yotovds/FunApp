using FunApp.Services.DataServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FunApp.Web.Models
{
    public class ValidCategoryIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (ICategoriesService)validationContext.GetService(typeof(ICategoriesService));

            if (service.IsCategoryIdValid((int)value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid category Id.");
            }
        }
    }
}
