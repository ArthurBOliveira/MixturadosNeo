using MySql.Data.MySqlClient;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NeoMix.DAL
{
    public class FanArtDAL : BaseDAL
    {    
        public List<FanArt> FanArtList()
        {
            CreateView("/fanarts", DateTime.Now, "Fanart");

            MySqlCommand cmd = new MySqlCommand("proc_fanart_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<FanArt> FanArts = new List<FanArt>();
            FanArt FanArt = new FanArt();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FanArt.Id = int.Parse(reader.GetString(0));
                    FanArt.Title = reader.GetValue(1).ToString();
                    FanArt.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    FanArt.Url = reader.GetValue(3).ToString();
                    FanArt.Author = reader.GetValue(4).ToString();

                    FanArt.Date.AddHours(4);

                    FanArts.Add(FanArt);
                    FanArt = new FanArt();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return FanArts;
        }

        public bool FanArtCreate(FanArt f)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_fanart_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_title", f.Title));
            cmd.Parameters.Add(new MySqlParameter("p_date", f.Date));
            cmd.Parameters.Add(new MySqlParameter("p_url", f.Url));
            cmd.Parameters.Add(new MySqlParameter("p_author", f.Author));

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

        public FanArt FanArtSelect(int id_FanArt)
        {
            CreateView("/fanart?id_fanart=" + id_FanArt, DateTime.Now, "Fanart");

            MySqlCommand cmd = new MySqlCommand("proc_fanart_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_fanart", id_FanArt));

            FanArt FanArt = new FanArt();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FanArt.Id = int.Parse(reader.GetString(0));
                    FanArt.Title = reader.GetValue(1).ToString();
                    FanArt.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    FanArt.Url = reader.GetValue(3).ToString();
                    FanArt.Author = reader.GetValue(4).ToString();

                    FanArt.Date.AddHours(4);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return FanArt;
        }

        public List<FanArt> FanArtSelectHome(int views)
        {
            MySqlCommand cmd = new MySqlCommand("proc_FanArt_select_home", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_views", views));

            List<FanArt> FanArts = new List<FanArt>();
            FanArt FanArt = new FanArt();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FanArt.Id = int.Parse(reader.GetString(0));
                    FanArt.Title = reader.GetValue(1).ToString();
                    FanArt.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    FanArt.Url = reader.GetValue(3).ToString();
                    FanArt.Author = reader.GetValue(4).ToString();

                    FanArt.Date.AddHours(4);

                    FanArts.Add(FanArt);
                    FanArt = new FanArt();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return FanArts;
        }

        public bool FanArtUpdate(FanArt f)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_fanart_update", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_fanart", f.Id));
            cmd.Parameters.Add(new MySqlParameter("p_title", f.Title));
            cmd.Parameters.Add(new MySqlParameter("p_date", f.Date));
            cmd.Parameters.Add(new MySqlParameter("p_url", f.Url));
            cmd.Parameters.Add(new MySqlParameter("p_author", f.Author));

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