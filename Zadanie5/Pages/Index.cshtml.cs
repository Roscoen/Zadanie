using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;
using Zadanie5.Models;
using System.Data;
using Zadanie5.DAL;

namespace Zadanie5.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> productList;

        IProductDB productDB;

        public IndexModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }

        public void OnGet()
        {
            productList = productDB.List();
        }
    }
}
