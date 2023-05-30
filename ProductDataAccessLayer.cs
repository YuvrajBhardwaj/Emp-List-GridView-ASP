using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace demo
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ProductDataAccessLayer
    {
        public static List<Product> GetAllProducts()
        {
            List<Product> listproducts = new List<Product>();

            string CS = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetProducts", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(dr["Id"]);
                    product.Name = dr["Name"].ToString();
                    product.Description = dr["Description"].ToString();

                    listproducts.Add(product);
                }

                con.Close();
            }
            return listproducts;
        }
    }
}