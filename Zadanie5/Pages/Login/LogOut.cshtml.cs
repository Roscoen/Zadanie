using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Zadanie5.Pages.Login
{
    public class LogOutModel : PageModel
    {
        public IActionResult OnGet()
        {
            string key = "CookieAuthentication";
            string value = "";
            CookieOptions cookieoptions = new CookieOptions();
            cookieoptions.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append(key, value, cookieoptions);
            return this.RedirectToPage("/index");
        }
    }
}
