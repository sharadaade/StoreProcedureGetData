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
        public static int Choice { get; set; }
        public static string Name { get; set; }
        public static string Mobono { get; set; }
        public static string Address { get; set; }
        public static string Capname { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("1.Get your data with ID");
            Console.WriteLine("2.Enter The new Data ");
            Console.WriteLine("Enter the Choice : ");
            Choice = Convert.ToInt32(Console.ReadLine());

            switch (Choice)
            {
                case 1:
                    Console.Write("Enter Your ID : ");
                    int ID = Convert.ToInt32(Console.ReadLine());
                    GetMyData(ID);
                    Console.ReadLine();
                    break;

                case 2:
                    Console.WriteLine("Enter your name : ");
                    Name = Console.ReadLine();

                    Console.WriteLine("Enter your Mobono : ");
                    Mobono = Console.ReadLine();

                    Console.WriteLine("Enter your Address : ");
                    Address = Console.ReadLine();

                    Console.WriteLine("Enter your Capname : ");
                    Capname = Console.ReadLine();

                    InsertData(Name, Mobono, Address, Capname);

                    break;

                default:
                    Console.WriteLine("Invalid Data");
                    break;
            }

            
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


                // Add the ID parameter to the SqlCommand
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("ID =>" + dr["id"] + " Name =>" + dr["name"] + " Address =>" + dr["address"] + " Capname =>" + dr["capname"]);
                }

            }
            con.Close();
        }
        static void InsertData(string Name, string Mobono, string Address, string Capname)
        {
            string cs = "Data Source=.\\sqlexpress;Initial Catalog=crud;Integrated Security=True;TrustServerCertificate=True";

            string query = "insertData";

            SqlConnection con = null;
            using(con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@mobono", Mobono);
                cmd.Parameters.AddWithValue("@address", Address);
                cmd.Parameters.AddWithValue("@capname", Capname);

                con.Open();
                int affect = cmd.ExecuteNonQuery();

                if(affect > 0)
                {
                    Console.WriteLine("Data inserted");
                }
                else
                {
                    Console.WriteLine("Data not inserted");
                }

            }
            con.Close();
        }
    }
}
