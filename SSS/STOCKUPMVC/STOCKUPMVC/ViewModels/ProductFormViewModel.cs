using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using STOCKUPMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace STOCKUPMVC.ViewModels
{
    public class ProductFormViewModel
    {
        public ProductFormViewModel()
        {
            Product = new Product();
        }

        [ValidateNever]
        public Product Product { get; set; }
        [ValidateNever]
        public SelectList Categories { get; set; }
    }
}
