using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace StoreProcedureGetData
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.Write("Enter Your ID : ");
            int ID = Convert.ToInt32(Console.ReadLine());
            GetMyData(ID);
            Console.ReadLine();
        }
        static void GetMyData(int ID)
        {
            string cs = "Data Source=.\\sqlexpress;Initial Catalog=crud;Integrated Security=True;TrustServerCertificate=True";

            string query = "tbldata";

            SqlConnection con = null;
                
            
            using(con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;

                // ------------------------------------------------
                // Add the ID parameter to the SqlCommand
                cmd.Parameters.Add(new SqlParameter("@ID", ID));
                // ------------------------------------------------

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("ID =>" + dr["id"] + " Name =>" + dr["name"] + " Address =>" + dr["address"] + " Capname =>" + dr["capname"]);
                }

            }
            con.Close();
        }
    }
}
