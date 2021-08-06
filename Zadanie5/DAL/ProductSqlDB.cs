using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Zadanie5.DAL;
using Zadanie5.Models;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Zadanie5.DAL
{
    public class ProductSqlDB : IProductDB
    {
        public IConfiguration _configuration { get; }

        public int id { get; set; }

        public ProductSqlDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Product> List()
        {
            List<Product> productList = new List<Product>();
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_productSelect", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Product product = new Product();
                product.id = Convert.ToInt32(dataReader["Id"]);
                product.name = Convert.ToString(dataReader["Name"]);
                product.price = Convert.ToDecimal(dataReader["Price"]);
                productList.Add(product);
            }
            dataReader.Close();
            connection.Close();
            return productList;
        }
        public Product Get(int _id)
        {
            this.id = _id;

            Product product;
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_productOneSelect", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@productID", SqlDbType.Int));
            command.Parameters["@productID"].Value = _id;

            SqlDataReader dataReader = command.ExecuteReader();
            
           /* product = new Product()
            {
                id = (int)dataReader["id"],
                name = (string)dataReader["name"],
                price = (decimal)dataReader["price"],
            };*/
            product = new Product();
            while (dataReader.Read())
            {
                //product = new Product();
                product.id = _id;
                product.name = Convert.ToString(dataReader["Name"]);
                product.price = Convert.ToDecimal(dataReader["Price"]);
            }


            dataReader.Close();
            connection.Close();
            return product;
        }
        public void Update(Product _product)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            SqlCommand command = new SqlCommand("sp_productEdit", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@name", SqlDbType.NChar, 50));
            command.Parameters["@name"].Value = _product.name;
            command.Parameters.Add(new SqlParameter("@price", SqlDbType.Money));
            command.Parameters["@price"].Value = _product.price;
            command.Parameters.AddWithValue("@productID", _product.id);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Add(Product _product)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            SqlCommand command = new SqlCommand("sp_productAdd", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@name", SqlDbType.NChar, 50));
            command.Parameters["@name"].Value = _product.name;
            command.Parameters.Add(new SqlParameter("@price", SqlDbType.Money));
            command.Parameters["@price"].Value = _product.price;
            command.Parameters.Add(new SqlParameter("@productID", SqlDbType.Int));
            command.Parameters["@productID"].Value = ParameterDirection.Output;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(int _id)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            SqlCommand command = new SqlCommand("sp_productDelete", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@productID", _id);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
