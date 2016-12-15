using MySql.Data.MySqlClient;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NeoMix.DAL
{
    public class ChampionshipDAL : BaseDAL
    {
        public bool ChampionshipCreate(Championship c, List<Championship> champs)
        {
            bool result = false;

            if (!champs.Exists(x => x.Link == c.Link))
            {
                MySqlCommand cmd = new MySqlCommand("proc_championship_create", conn);
                MySqlDataReader reader;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("p_game", c.Game));
                cmd.Parameters.Add(new MySqlParameter("p_name", c.Name));
                cmd.Parameters.Add(new MySqlParameter("p_date", c.Date));
                cmd.Parameters.Add(new MySqlParameter("p_img", c.Img));
                cmd.Parameters.Add(new MySqlParameter("p_link", c.Link));
                cmd.Parameters.Add(new MySqlParameter("p_owner", c.Owner));
                cmd.Parameters.Add(new MySqlParameter("p_prize", c.Prize));
                cmd.Parameters.Add(new MySqlParameter("p_details", c.Details));
                cmd.Parameters.Add(new MySqlParameter("p_islocal", c.IsLocal));
                cmd.Parameters.Add(new MySqlParameter("p_local", c.Place));
                cmd.Parameters.Add(new MySqlParameter("p_source", c.Source));
                cmd.Parameters.Add(new MySqlParameter("p_stream", c.Stream));
            
                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    result = true;
                }
                catch (Exception e)
                {
                    conn.Close();
                }
                finally
                {
                    conn.Close();
                }
            }

            return result;
        }

        public List<Championship> ChampionshipListByGame(string game)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_select_game", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_game", game));

            List<Championship> championships = new List<Championship>();
            Championship championship = new Championship();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.Game = game;
                    championship.Name = reader.GetValue(2).ToString();                    
                    championship.Date = DateTime.Parse(reader.GetValue(3).ToString());
                    championship.Img = reader.GetValue(4).ToString();
                    championship.Link = reader.GetValue(5).ToString();
                    championship.Owner = reader.GetValue(5).ToString();
                    championship.Prize = reader.GetValue(6).ToString();
                    championship.Details = reader.GetValue(7).ToString();
                    championship.IsPremium = reader.GetBoolean(11);

                    championships.Add(championship);
                    championship = new Championship();
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

            return championships;
        }

        public List<Championship> ChampionshipList()
        {
            CreateView("/home/champs", DateTime.Now, "Championship");

            MySqlCommand cmd = new MySqlCommand("proc_championship_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Championship> championships = new List<Championship>();
            Championship championship = new Championship();

            try
            {
                conn.Open();                
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.Game = reader.GetValue(1).ToString();
                    championship.Name = reader.GetValue(2).ToString();
                    championship.Date = DateTime.Parse(reader.GetValue(3).ToString());
                    championship.Img = reader.GetValue(4).ToString();
                    championship.Link = reader.GetValue(5).ToString();
                    championship.Owner = reader.GetValue(6).ToString();
                    championship.Prize = reader.GetValue(7).ToString();
                    championship.Details = reader.GetValue(8).ToString();
                    championship.IsPremium = reader.GetBoolean(11);

                    championships.Add(championship);
                    championship = new Championship();
                }
            }
            catch (Exception e)
            {
                conn.Close();
                SaveLog(e);
            }
            finally
            {
                conn.Close();
            }

            return championships;
        }

        public Championship ChampionshipSelect(int id_champ)
        {
            CreateView("/home/champ?id_champ=" + id_champ, DateTime.Now, "Championship");

            MySqlCommand cmd = new MySqlCommand("proc_championship_select_id", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_champ", id_champ));

            Championship championship = new Championship();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.Game = reader.GetValue(1).ToString();
                    championship.Name = reader.GetValue(2).ToString();
                    championship.Date = DateTime.Parse(reader.GetValue(3).ToString());
                    championship.Img = reader.GetValue(4).ToString();
                    championship.Link = reader.GetValue(5).ToString();
                    championship.Owner = reader.GetValue(6).ToString();
                    championship.Prize = reader.GetValue(7).ToString();
                    championship.Details = reader.GetValue(8).ToString();
                    championship.IsPremium = reader.GetBoolean(11);
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

            return championship;
        }

        public List<Championship> ChampionshipSelectHome(int p)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_select_home", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_views", p));

            List<Championship> championships = new List<Championship>();
            Championship championship = new Championship();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.Game = reader.GetValue(1).ToString();
                    championship.Name = reader.GetValue(2).ToString();
                    championship.Date = DateTime.Parse(reader.GetValue(3).ToString());
                    championship.Img = reader.GetValue(4).ToString();
                    championship.Link = reader.GetValue(5).ToString();
                    championship.Owner = reader.GetValue(6).ToString();
                    championship.Prize = reader.GetValue(7).ToString();
                    championship.Details = reader.GetValue(8).ToString();
                    championship.IsPremium = reader.GetBoolean(11);

                    championships.Add(championship);
                    championship = new Championship();
                }
            }
            catch (Exception e)
            {
                conn.Close();
                SaveLog(e);
            }
            finally
            {
                conn.Close();
            }

            return championships;
        }

        public List<Championship> ChampionshipListByGamePreview(string game)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_select_game", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_game", game));

            List<Championship> championships = new List<Championship>();
            Championship championship = new Championship();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                int i = 0;
                while (reader.Read() && i < 6)
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.Game = game;
                    championship.Name = reader.GetValue(2).ToString();
                    championship.Date = DateTime.Parse(reader.GetValue(3).ToString());
                    championship.Img = reader.GetValue(4).ToString();
                    championship.Link = reader.GetValue(5).ToString();
                    championship.Owner = reader.GetValue(5).ToString();
                    championship.Prize = reader.GetValue(6).ToString();
                    championship.Details = reader.GetValue(7).ToString();
                    championship.IsPremium = reader.GetBoolean(11);

                    championships.Add(championship);
                    championship = new Championship();
                    i++;
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

            return championships;
        }

        public List<View> ViewList()
        {
            MySqlCommand cmd = new MySqlCommand("proc_view_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<View> views = new List<View>();
            View view = new View();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    view.Id = (int)reader.GetValue(0);                    
                    view.Page = reader.GetValue(1).ToString();
                    view.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    view.Type = reader.GetValue(3).ToString();

                    views.Add(view);
                    view = new View();
                }
            }
            catch (Exception e)
            {
                conn.Close();
                SaveLog(e);
            }
            finally
            {
                conn.Close();
            }

            return views;
        }

        private void SaveLog(Exception e)
        {
            string fileName = String.Format("{0}" + @"\Log.txt", AppDomain.CurrentDomain.BaseDirectory);

            // Check that the file doesn't already exist. If it doesn't exist, create
            if (!System.IO.File.Exists(fileName))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                {
                    file.WriteLine(e.Message);
                    file.Flush();
                    file.Dispose();
                    file.Close();
                }
            }
        }
    }
}