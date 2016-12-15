using MySql.Data.MySqlClient;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NeoMix.DAL
{
    public class VoteDAL : BaseDAL
    {
        #region Survey
        public bool SurveyCreate(Survey s)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_survey_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_title", s.Title));
            cmd.Parameters.Add(new MySqlParameter("p_date", s.Date));

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

        public List<Survey> SurveyList()
        {
            //CreateView("/survey", DateTime.Now, "survey");

            MySqlCommand cmd = new MySqlCommand("proc_survey_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Survey> surveys = new List<Survey>();
            Survey survey = new Survey();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    survey.Id = int.Parse(reader.GetString(0));
                    survey.Title = reader.GetValue(1).ToString();
                    survey.Date = DateTime.Parse(reader.GetValue(2).ToString());


                    surveys.Add(survey);
                    survey = new Survey();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


            return surveys;
        }

        
        #endregion

        #region SurveyOption
        #endregion

        #region Vote
        #endregion
    }
}