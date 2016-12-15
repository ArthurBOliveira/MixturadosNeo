using MySql.Data.MySqlClient;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NeoMix.DAL
{
    public class SpotlightDAL : BaseDAL
    {
        public List<Spotlight> SpotlightList()
        {
            CreateView("/home", DateTime.Now, "Home");

            MySqlCommand cmd = new MySqlCommand("proc_spotlight_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Spotlight> Spotlights = new List<Spotlight>();
            Spotlight Spotlight = new Spotlight();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Spotlight.Id = int.Parse(reader.GetString(0));
                    Spotlight.Width = reader.GetValue(1).ToString();
                    Spotlight.Height = reader.GetValue(2).ToString();
                    Spotlight.Img = reader.GetValue(3).ToString();
                    Spotlight.Link = reader.GetValue(4).ToString();
                    Spotlight.Title = reader.GetValue(5).ToString();
                    Spotlight.Type = reader.GetValue(6).ToString();

                    Spotlights.Add(Spotlight);
                    Spotlight = new Spotlight();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return Spotlights;
        }

        public bool SpotlightCreate(Spotlight s)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_spotlight_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_width", s.Width));
            cmd.Parameters.Add(new MySqlParameter("p_height", s.Height));
            cmd.Parameters.Add(new MySqlParameter("p_img", s.Img));
            cmd.Parameters.Add(new MySqlParameter("p_title", s.Title));
            cmd.Parameters.Add(new MySqlParameter("p_link", s.Link));
            cmd.Parameters.Add(new MySqlParameter("p_type", s.Type));

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

        public Spotlight SpotlightSelect(int id_Spotlight)
        {
            MySqlCommand cmd = new MySqlCommand("proc_spotlight_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_spotlight", id_Spotlight));

            Spotlight Spotlight = new Spotlight();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Spotlight.Id = int.Parse(reader.GetString(0));
                    Spotlight.Width = reader.GetValue(1).ToString();
                    Spotlight.Height = reader.GetValue(2).ToString();
                    Spotlight.Img = reader.GetValue(3).ToString();
                    Spotlight.Title = reader.GetValue(4).ToString();
                    Spotlight.Link = reader.GetValue(5).ToString();
                    Spotlight.Type = reader.GetValue(6).ToString();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return Spotlight;
        }       

        public bool SpotlightUpdate(Spotlight s)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_spotlight_update", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_spotlight", s.Id));
            cmd.Parameters.Add(new MySqlParameter("p_width", s.Width));
            cmd.Parameters.Add(new MySqlParameter("p_height", s.Height));
            cmd.Parameters.Add(new MySqlParameter("p_img", s.Img));
            cmd.Parameters.Add(new MySqlParameter("p_title", s.Title));
            cmd.Parameters.Add(new MySqlParameter("p_link", s.Link));
            cmd.Parameters.Add(new MySqlParameter("p_type", s.Type));

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

        public bool SpotlightDelete(int id)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_spotlight_delete", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_spotlight", id));

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

        public void SpotlightClean()
        {
            MySqlCommand cmd = new MySqlCommand("proc_spotlight_clean", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }
}