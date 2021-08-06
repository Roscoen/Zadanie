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
using RestSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Zadanie5.Pages;

namespace Zadanie5.Pages
{
    public class DeleteModel : PageModel
    {
        IProductDB productDB;
        public DeleteModel(IProductDB _productDB)
        {
            productDB = _productDB;
            
            
        }

        public int id { get; set; }

        public void OnGet(int id)
        {
            this.id = id;
        }

        public IActionResult OnPost(int id)
        {
            string key = "CookieAuthentication";
            var cookievalue = Request.Cookies[key];

            if (cookievalue == null)
            {
                return RedirectToPage("/Login/UserLogin");
            }
            else
            {
                productDB.Delete(id);
                return RedirectToPage("Index"); 
            }
        }
    }
}
