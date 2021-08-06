using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Zadanie5.Models;

namespace Zadanie5.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IConfiguration _configuration { get; }

        public SignUpModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [BindProperty]
        public Account createAccount { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost(Account createAccount)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            SqlCommand command = new SqlCommand("sp_accountAdd", connection);
            command.CommandType = CommandType.StoredProcedure;

            
            string password = CryptoHelper.Crypto.HashPassword(createAccount.password);

            command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NChar, 50));
            command.Parameters["@Username"].Value = createAccount.userName;
            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NChar, 450));
            command.Parameters["@Password"].Value = password;
            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters["@Id"].Value = ParameterDirection.Output;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return RedirectToPage("/Login/UserLogin");
        }
    }
}
