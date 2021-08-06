using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Text;
using Zadanie5.Models;
using System.Data;
using Zadanie5.DAL;

namespace Zadanie5.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }

        IProductDB productDB;
        public CreateModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            productDB.Add(product);
            return RedirectToPage("Index");
        }
    }
}
