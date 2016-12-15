using MySql.Data.MySqlClient;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NeoMix.DAL
{
    public class AdminDAL : BaseDAL
    {
        public Admin AdminLogin(string nick, string password)
        {
            MySqlCommand cmd = new MySqlCommand("proc_admin_login", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_nick", nick));
            cmd.Parameters.Add(new MySqlParameter("p_password", password));

            Admin admin = new Admin();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    admin.Id = (int)reader.GetValue(0);
                    admin.Name = reader.GetValue(1).ToString();
                    admin.Nick = reader.GetValue(2).ToString();
                    admin.Desc = reader.GetValue(3).ToString();
                    admin.Link = reader.GetValue(4).ToString();
                    admin.Img = reader.GetValue(5).ToString();
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return admin;
        }

        public Admin AdminLogin(int id_admin)
        {
            MySqlCommand cmd = new MySqlCommand("proc_admin_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_admin", id_admin));

            Admin admin = new Admin();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    admin.Id = (int)reader.GetValue(0);
                    admin.Name = reader.GetValue(1).ToString();
                    admin.Nick = reader.GetValue(2).ToString();
                    admin.Desc = reader.GetValue(3).ToString();
                    admin.Link = reader.GetValue(4).ToString();
                    admin.Img = reader.GetValue(5).ToString();
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return admin;
        }

        public Admin AdminSelectByNick(string nick)
        {
            MySqlCommand cmd = new MySqlCommand("proc_admin_select_by_nick", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_nick", nick));

            Admin admin = new Admin();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    admin.Id = (int)reader.GetValue(0);
                    admin.Name = reader.GetValue(1).ToString();
                    admin.Nick = reader.GetValue(2).ToString();
                    admin.Desc = reader.GetValue(3).ToString();
                    admin.Link = reader.GetValue(4).ToString();
                    admin.Img = reader.GetValue(5).ToString();
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return admin;
        }

        public Stats ViewListByPage()
        {
            Stats stats = new Stats();

            stats.Name = "Por Páginas";

            MySqlCommand cmd = new MySqlCommand("proc_view_list_page", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;            

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    stats.Views.Add(int.Parse(reader.GetString(1)));
                    stats.Collumn.Add(reader.GetValue(0).ToString());
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return stats;
        }

        public Stats ViewListByDate()
        {
            Stats stats = new Stats();

            stats.Name = "Por Data";

            MySqlCommand cmd = new MySqlCommand("proc_view_list_page", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    stats.Views.Add(int.Parse(reader.GetString(1)));
                    stats.Date.Add((DateTime)reader.GetValue(0));
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return stats;
        }

        public Stats ViewListByType()
        {
            Stats stats = new Stats();

            stats.Name = "Por Tipo";

            MySqlCommand cmd = new MySqlCommand("proc_view_list_type", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    stats.Views.Add(int.Parse(reader.GetString(1)));
                    stats.Date.Add((DateTime)reader.GetValue(0));
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return stats;
        }
    }
}