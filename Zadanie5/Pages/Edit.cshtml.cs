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
    public class EditModel : PageModel
    {
        [BindProperty]
        public Product editProduct { get; set; }

        IProductDB productDB;

        public EditModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }

        public int id { get; set; }

        public void OnGet(int id)
        {
            this.id = id;
            editProduct = productDB.Get(id);
        }

        public IActionResult OnPost(Product editProduct)
        {
            string key = "CookieAuthentication";
            var cookievalue = Request.Cookies[key];

            if (cookievalue == null)
            {
                return RedirectToPage("/Login/UserLogin");
            }
            else
            {
                productDB.Update(editProduct);
                return RedirectToPage("Index");
            }

        }
    }
}
