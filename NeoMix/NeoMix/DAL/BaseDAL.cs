using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NeoMix.DAL
{
    public class BaseDAL
    {
        public string connectionString;
        public MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnUolProducao"].ConnectionString);

        public bool CreateView(string page, DateTime date, string type)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_view_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_page", page));
            cmd.Parameters.Add(new MySqlParameter("p_date", date));
            cmd.Parameters.Add(new MySqlParameter("p_type", type));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                result = true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}