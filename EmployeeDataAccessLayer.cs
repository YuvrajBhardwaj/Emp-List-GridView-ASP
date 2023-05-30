using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace demo
{
    public class Employee
    {
        public int Empid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
    }
    public class EmployeeDataAccessLayer
    {
        public static List<Employee> GetAllEmployees()
        {
            List<Employee> listEmployees = new List<Employee>();

            string CS = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select * from tblEmployees", con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Employee employee = new Employee();
                        employee.Empid = Convert.ToInt32(rdr["Empid"]);
                        employee.Firstname = rdr["Firstname"].ToString();
                        employee.Lastname = rdr["Lastname"].ToString();
                        employee.Gender = rdr["Gender"].ToString();
                        employee.City = rdr["City"].ToString();

                        listEmployees.Add(employee);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Some Error Occured", ex);
                }
                finally
                {
                    con.Close();
                    con.Dispose();

                }
            }

            return listEmployees;
        }
        public static void DeleteEmployee(int Empid)
        {
            string CS = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Delete from tblEmployees where Empid = @Empid", con);
                SqlParameter param = new SqlParameter("@Empid", Empid);
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal static void UpdateEmployee(int employeeId, string name, string gender, string city)
        {
            throw new NotImplementedException();
        }

        public static int UpdateEmployee(int Empid, string Firstname, string Lastname, string Gender, string City)
        {
            string CS = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string updateQuery = "Update tblEmployees SET Firstname = @Firstname,  " +
                    "Lastname = @Lastname, Gender = @Gender, City = @City WHERE Empid = @Empid";
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                SqlParameter paramOriginalEmployeeId = new SqlParameter("@Empid", Empid);
                cmd.Parameters.Add(paramOriginalEmployeeId);
                SqlParameter paramName = new SqlParameter("@Firstname", Firstname);
                cmd.Parameters.Add(paramName);
                SqlParameter paramLname = new SqlParameter("@Lastname", Lastname);
                cmd.Parameters.Add(paramLname);
                SqlParameter paramGender = new SqlParameter("@Gender", Gender);
                cmd.Parameters.Add(paramGender);
                SqlParameter paramCity = new SqlParameter("@City", City);
                cmd.Parameters.Add(paramCity);
                con.Open();
                return cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public static int InsertEmployee(string Firstname, string Lastname, string Gender, string City)
        {
            string CS = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                SqlCommand cmd = new SqlCommand("insert into tblEmployees(Firstname,Lastname,Gender,City)" +
                    "values(@Firstname, @Lastname, @Gender, @City)", con);

                SqlParameter paramFirstname = new SqlParameter("Firstname", Firstname);
                cmd.Parameters.Add(paramFirstname);
                SqlParameter paramLastname = new SqlParameter("Lastname", Lastname);
                cmd.Parameters.Add(paramLastname);
                SqlParameter paramGender = new SqlParameter("Gender", Gender);
                cmd.Parameters.Add(paramGender);
                SqlParameter paramCity = new SqlParameter("City", City);
                cmd.Parameters.Add(paramCity);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

    }
}