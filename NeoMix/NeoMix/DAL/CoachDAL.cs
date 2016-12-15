using MySql.Data.MySqlClient;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NeoMix.DAL
{
  public class CoachDAL : BaseDAL
    {
        public List<Coach> CoachList()
        {
            MySqlCommand cmd = new MySqlCommand("proc_coach_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Coach> Coachs = new List<Coach>();
            Coach Coach = new Coach();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Coach.Id = int.Parse(reader.GetString(0));
                    Coach.Name = reader.GetValue(1).ToString();
                    Coach.Game = reader.GetValue(2).ToString();
                    Coach.Link = reader.GetValue(3).ToString();
                    Coach.Img = reader.GetValue(4).ToString();
                    Coach.Desc = reader.GetValue(5).ToString();
                    Coach.Date = DateTime.Parse(reader.GetValue(6).ToString());

                    Coachs.Add(Coach);
                    Coach = new Coach();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return Coachs;
        }

        public bool CoachCreate(Coach coach)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_coach_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_name", coach.Name));
            cmd.Parameters.Add(new MySqlParameter("p_game", coach.Game));
            cmd.Parameters.Add(new MySqlParameter("p_link", coach.Link));
            cmd.Parameters.Add(new MySqlParameter("p_img", coach.Img));
            cmd.Parameters.Add(new MySqlParameter("p_desc", coach.Desc));
            cmd.Parameters.Add(new MySqlParameter("p_date", coach.Date));

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

        public Coach CoachSelect(int id_Coach)
        {
            MySqlCommand cmd = new MySqlCommand("proc_coach_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_Coach", id_Coach));

            Coach Coach = new Coach();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Coach.Id = int.Parse(reader.GetString(0));
                    Coach.Name = reader.GetValue(1).ToString();
                    Coach.Game = reader.GetValue(2).ToString();
                    Coach.Link = reader.GetValue(3).ToString();
                    Coach.Img = reader.GetValue(4).ToString();
                    Coach.Desc = reader.GetValue(5).ToString();
                    Coach.Date = DateTime.Parse(reader.GetValue(6).ToString());

                    Coach.Date.AddHours(4);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return Coach;
        }

        public List<Coach> CoachSelectHome(int views)
        {
            MySqlCommand cmd = new MySqlCommand("proc_coach_select_home", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_views", views));

            List<Coach> Coachs = new List<Coach>();
            Coach Coach = new Coach();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Coach.Id = int.Parse(reader.GetString(0));
                    Coach.Name = reader.GetValue(1).ToString();
                    Coach.Game = reader.GetValue(2).ToString();
                    Coach.Link = reader.GetValue(3).ToString();
                    Coach.Img = reader.GetValue(4).ToString();
                    Coach.Desc = reader.GetValue(5).ToString();
                    Coach.Date = DateTime.Parse(reader.GetValue(6).ToString());

                    Coachs.Add(Coach);
                    Coach = new Coach();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return Coachs;
        }

        public bool CoachUpdate(Coach coach)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_coach_update", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_name", coach.Name));
            cmd.Parameters.Add(new MySqlParameter("p_game", coach.Game));
            cmd.Parameters.Add(new MySqlParameter("p_link", coach.Link));
            cmd.Parameters.Add(new MySqlParameter("p_img", coach.Img));
            cmd.Parameters.Add(new MySqlParameter("p_desc", coach.Desc));
            cmd.Parameters.Add(new MySqlParameter("p_date", coach.Date));

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
