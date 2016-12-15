using MySql.Data.MySqlClient;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NeoMix.DAL
{
    public class NewsDAL : BaseDAL
    {
        public List<News> NewsList()
        {
            CreateView("/news", DateTime.Now, "news");

            MySqlCommand cmd = new MySqlCommand("proc_new_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<News> news = new List<News>();
            News _news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _news.Id = int.Parse(reader.GetString(0));
                    _news.Title = reader.GetValue(1).ToString();
                    _news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    _news.Text = reader.GetValue(3).ToString();
                    _news.Author = reader.GetValue(4).ToString();
                    _news.Source = reader.GetValue(5).ToString();
                    _news.Img = reader.GetValue(6).ToString();
                    _news.NewsType = reader.GetValue(7).ToString();
                    _news.IsPremium = reader.GetBoolean(8);
                    _news.ShortDesc = reader.GetString(9);
                    _news.Link = reader.GetString(11);
                    _news.IsPublished = reader.GetBoolean(12);
                    

                    news.Add(_news);
                    _news = new News();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


            return news;
        }

        public bool NewsCreate(News n)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_new_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_title", n.Title));
            cmd.Parameters.Add(new MySqlParameter("p_date", n.Date));
            cmd.Parameters.Add(new MySqlParameter("p_text", n.Text));
            cmd.Parameters.Add(new MySqlParameter("p_author", n.Author));
            cmd.Parameters.Add(new MySqlParameter("p_url", n.Source));
            cmd.Parameters.Add(new MySqlParameter("p_img", n.Img));
            cmd.Parameters.Add(new MySqlParameter("p_newsType", n.NewsType));
            cmd.Parameters.Add(new MySqlParameter("p_shortDesc", n.ShortDesc));
            cmd.Parameters.Add(new MySqlParameter("p_comments", n.Comments));
            cmd.Parameters.Add(new MySqlParameter("p_link", n.Link));
            cmd.Parameters.Add(new MySqlParameter("p_isPublished", n.IsPublished));

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

        public News NewsSelect(int id_news)
        {
            CreateView("/news?id_news=" + id_news, DateTime.Now, "news");

            MySqlCommand cmd = new MySqlCommand("proc_new_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_new", id_news));

            News news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    news.Id = int.Parse(reader.GetString(0));
                    news.Title = reader.GetValue(1).ToString();
                    news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    news.Text = reader.GetValue(3).ToString();
                    news.Author = reader.GetValue(4).ToString();
                    news.Source = reader.GetValue(5).ToString();
                    news.Img = reader.GetValue(6).ToString();
                    news.NewsType = reader.GetValue(7).ToString();
                    news.IsPremium = reader.GetBoolean(8);
                    news.ShortDesc = reader.GetString(9);
                    news.Link = reader.GetString(11);
                    news.IsPublished = reader.GetBoolean(12);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            news.Views = getNewsViews(news.Id);

            return news;
        }

        public News NewsSelectBot(int id_news)
        {
            CreateView("/news?id_news=" + id_news, DateTime.Now, "news");

            MySqlCommand cmd = new MySqlCommand("proc_new_select_bot", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_new", id_news));

            News news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    news.Id = int.Parse(reader.GetString(0));
                    news.Title = reader.GetValue(1).ToString();
                    news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    news.Text = reader.GetValue(3).ToString();
                    news.Author = reader.GetValue(4).ToString();
                    news.Source = reader.GetValue(5).ToString();
                    news.Img = reader.GetValue(6).ToString();
                    news.NewsType = reader.GetValue(7).ToString();
                    news.IsPremium = reader.GetBoolean(8);
                    news.ShortDesc = reader.GetString(9);
                    news.Link = reader.GetString(11);
                    news.IsPublished = reader.GetBoolean(12);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            news.Views = getNewsViews(news.Id);

            return news;
        }

        public List<News> NewsSelectHome(int views)
        {
            MySqlCommand cmd = new MySqlCommand("proc_news_select_home", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_views", views));

            List<News> news = new List<News>();
            News _news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _news.Id = int.Parse(reader.GetString(0));
                    _news.Title = reader.GetValue(1).ToString();
                    _news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    _news.Text = reader.GetValue(3).ToString();
                    _news.Author = reader.GetValue(4).ToString();
                    _news.Source = reader.GetValue(5).ToString();
                    _news.Img = reader.GetValue(6).ToString();
                    _news.NewsType = reader.GetValue(7).ToString();
                    _news.IsPremium = reader.GetBoolean(8);
                    _news.ShortDesc = reader.GetString(9);
                    _news.IsPublished = reader.GetBoolean(12);

                    news.Add(_news);
                    _news = new News();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return news;
        }

        public List<News> NewsListBot()
        {
            CreateView("/news", DateTime.Now, "news");

            MySqlCommand cmd = new MySqlCommand("proc_new_list_bot", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<News> news = new List<News>();
            News _news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _news.Id = int.Parse(reader.GetString(0));
                    _news.Title = reader.GetValue(1).ToString();
                    _news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    _news.Text = reader.GetValue(3).ToString();
                    _news.Author = reader.GetValue(4).ToString();
                    _news.Source = reader.GetValue(5).ToString();
                    _news.Img = reader.GetValue(6).ToString();
                    _news.NewsType = reader.GetValue(7).ToString();
                    _news.IsPremium = reader.GetBoolean(8);
                    _news.ShortDesc = reader.GetString(9);
                    _news.Link = reader.GetString(11);
                    _news.IsPublished = reader.GetBoolean(12);


                    news.Add(_news);
                    _news = new News();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


            return news;
        }

        public bool NewsUpdate(News n)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_new_update", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_new", n.Id));
            cmd.Parameters.Add(new MySqlParameter("p_title", n.Title));
            cmd.Parameters.Add(new MySqlParameter("p_date", n.Date));
            cmd.Parameters.Add(new MySqlParameter("p_text", n.Text));
            cmd.Parameters.Add(new MySqlParameter("p_author", n.Author));
            cmd.Parameters.Add(new MySqlParameter("p_url", n.Source));
            cmd.Parameters.Add(new MySqlParameter("p_img", n.Img));
            cmd.Parameters.Add(new MySqlParameter("p_newsType", n.NewsType));
            cmd.Parameters.Add(new MySqlParameter("p_shortDesc", n.ShortDesc));
            cmd.Parameters.Add(new MySqlParameter("p_comments", n.Comments));
            cmd.Parameters.Add(new MySqlParameter("p_link", n.Link));
            cmd.Parameters.Add(new MySqlParameter("p_isPublished", n.IsPublished));

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

        public List<News> NewsBaseList()
        {
            //CreateView("/news", DateTime.Now, "news");

            MySqlCommand cmd = new MySqlCommand("proc_new_list_bot", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<News> news = new List<News>();
            News _news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _news.Id = int.Parse(reader.GetString(0));
                    _news.Title = reader.GetValue(1).ToString();
                    _news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    _news.Text = reader.GetValue(3).ToString();
                    _news.Author = reader.GetValue(4).ToString();
                    _news.Source = reader.GetValue(5).ToString();
                    _news.Img = reader.GetValue(6).ToString();
                    _news.NewsType = reader.GetValue(7).ToString();
                    _news.IsPremium = reader.GetBoolean(8);
                    _news.ShortDesc = reader.GetString(9);
                    _news.Link = reader.GetString(11);
                    _news.IsPublished = reader.GetBoolean(12);

                    news.Add(_news);
                    _news = new News();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return news;
        }

        public List<News> NewsListBySubTitle(string subTitle)
        {
            CreateView("/news", DateTime.Now, "news");

            MySqlCommand cmd = new MySqlCommand("proc_new_list_subtitle", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_subTitle", subTitle));

            List<News> news = new List<News>();
            News _news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _news.Id = int.Parse(reader.GetString(0));
                    _news.Title = reader.GetValue(1).ToString();
                    _news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    _news.Text = reader.GetValue(3).ToString();
                    _news.Author = reader.GetValue(4).ToString();
                    _news.Source = reader.GetValue(5).ToString();
                    _news.Img = reader.GetValue(6).ToString();
                    _news.NewsType = reader.GetValue(7).ToString();
                    _news.IsPremium = reader.GetBoolean(8);
                    _news.ShortDesc = reader.GetString(9);
                    _news.Link = reader.GetString(11);
                    _news.IsPublished = reader.GetBoolean(12);

                    news.Add(_news);
                    _news = new News();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


            return news;
        }

        public bool NewsDelete(int id_new)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_new_delete", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_new", id_new));

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

        public int getNewsViews(int id)
        {
            MySqlCommand cmd = new MySqlCommand("proc_view_list_open", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_page", "/news?id_news=" + id));

            int result = 0;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
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

        public List<News> NewsListBySource(string source)
        {
            MySqlCommand cmd = new MySqlCommand("proc_news_select_source", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_source", source));

            List<News> news = new List<News>();
            News _news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _news.Id = int.Parse(reader.GetString(0));
                    _news.Title = reader.GetValue(1).ToString();
                    _news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    _news.Text = reader.GetValue(3).ToString();
                    _news.Author = reader.GetValue(4).ToString();
                    _news.Source = reader.GetValue(5).ToString();
                    _news.Img = reader.GetValue(6).ToString();
                    _news.NewsType = reader.GetValue(7).ToString();
                    _news.IsPremium = reader.GetBoolean(8);
                    _news.ShortDesc = reader.GetString(9);
                    _news.IsPublished = reader.GetBoolean(12);

                    news.Add(_news);
                    _news = new News();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return news;
        }

        public List<News> NewsListByNewsType(string newsType)
        {
            MySqlCommand cmd = new MySqlCommand("proc_news_select_newsType", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_newsType", newsType));

            List<News> news = new List<News>();
            News _news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _news.Id = int.Parse(reader.GetString(0));
                    _news.Title = reader.GetValue(1).ToString();
                    _news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    _news.Text = reader.GetValue(3).ToString();
                    _news.Author = reader.GetValue(4).ToString();
                    _news.Source = reader.GetValue(5).ToString();
                    _news.Img = reader.GetValue(6).ToString();
                    _news.NewsType = reader.GetValue(7).ToString();
                    _news.IsPremium = reader.GetBoolean(8);
                    _news.ShortDesc = reader.GetString(9);
                    _news.IsPublished = reader.GetBoolean(12);

                    news.Add(_news);
                    _news = new News();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return news;
        }
    }
}