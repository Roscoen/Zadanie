using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Zadanie5.Models;
using Zadanie5.Pages;

namespace Zadanie5.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public Account accountUser { get; set; }

        public string userName { get; set; }

        public UserLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private bool ValidateUser(Account accountUser1)
        {
            this.userName = accountUser1.userName;
            Account account;
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection connection = new SqlConnection(myCompanyDBcs);

            connection.Open();
            SqlCommand command = new SqlCommand("sp_accountOneSelect", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NChar, 50));
            command.Parameters["@Username"].Value = accountUser1.userName;

            SqlDataReader dataReader = command.ExecuteReader();

            account = new Account();
            while (dataReader.Read())
            {
               account.userName = Convert.ToString(dataReader["Username"]);
               account.password = (string)dataReader["Password"];

                

                if (CryptoHelper.Crypto.VerifyHashedPassword(account.password, accountUser1.password))
            {
                dataReader.Close();
                connection.Close();
                return true;
            }
            else
            {
                dataReader.Close();
                connection.Close();
                return false;
            } }
            return true;
        
        }

        public IActionResult OnPost(Account accountUser, string returnUrl = null)
        {
            this.userName = accountUser.userName;
            if (ValidateUser(accountUser))
            {
                string key = "CookieAuthentication";
                string value = accountUser.userName;
                CookieOptions cookieoptions = new CookieOptions();
                cookieoptions.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Append(key, value, cookieoptions);

                return RedirectToPage("/Index");
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
