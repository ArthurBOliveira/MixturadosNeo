using MySql.Data.MySqlClient;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NeoMix.DAL
{
    public class PostDAL : BaseDAL
    {
        public List<Post> PostList()
        {
            CreateView("/Post", DateTime.Now, "Post");

            MySqlCommand cmd = new MySqlCommand("proc_post_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Post> Posts = new List<Post>();
            Post post = new Post();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    post.Id = int.Parse(reader.GetString(0));
                    post.Title = reader.GetValue(1).ToString();
                    post.Text = reader.GetValue(2).ToString();
                    post.Tag = (Tag)Enum.Parse(typeof(Tag), reader.GetString(4), true);
                    post.CreateDate = DateTime.Parse(reader.GetValue(5).ToString());
                    post.IsOn = reader.GetBoolean(6);
                    post.IsPremium = reader.GetBoolean(8);
                    post.Game = reader.GetValue(9).ToString();


                    post.Img = ImageGame(post.Game);

                    Posts.Add(post);
                    post = new Post();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


            return Posts;
        }

        public List<Post> PostListByTag(Tag tag)
        {
            CreateView("/Post?tag=" + tag, DateTime.Now, "Post");

            MySqlCommand cmd = new MySqlCommand("proc_post_list_tag", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_tag", tag));

            List<Post> Posts = new List<Post>();
            Post post = new Post();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    post.Id = int.Parse(reader.GetString(0));
                    post.Title = reader.GetValue(1).ToString();
                    post.Text = reader.GetValue(2).ToString();
                    post.Tag = (Tag)reader.GetValue(4);
                    post.CreateDate = DateTime.Parse(reader.GetValue(5).ToString());
                    post.IsOn = reader.GetBoolean(6);
                    post.IsPremium = reader.GetBoolean(7);
                    post.Game = reader.GetValue(8).ToString();


                    post.Img = ImageGame(post.Game);

                    Posts.Add(post);
                    post = new Post();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


            return Posts;
        }

        public List<Post> PostListByPlayer(int id_player)
        {
            //CreateView("/Post?tag=" + tag, DateTime.Now, "Post");

            MySqlCommand cmd = new MySqlCommand("proc_post_list_player", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            List<Post> Posts = new List<Post>();
            Post post = new Post();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    post.Id = int.Parse(reader.GetString(0));
                    post.Title = reader.GetValue(1).ToString();
                    post.Text = reader.GetValue(2).ToString();
                    post.Tag = (Tag)reader.GetValue(4);
                    post.CreateDate = DateTime.Parse(reader.GetValue(5).ToString());
                    post.IsOn = reader.GetBoolean(6);
                    post.IsPremium = reader.GetBoolean(7);
                    post.Game = reader.GetValue(8).ToString();

                    post.Img = ImageGame(post.Game);

                    Posts.Add(post);
                    post = new Post();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


            return Posts;
        }

        public Post PostSelect(int id_post)
        {
            CreateView("/Post?id_post=" + id_post, DateTime.Now, "Post");

            MySqlCommand cmd = new MySqlCommand("proc_post_list_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_post", id_post));

            Post post = new Post();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    post.Id = int.Parse(reader.GetString(0));
                    post.Title = reader.GetValue(1).ToString();
                    post.Text = reader.GetValue(2).ToString();
                    post.Tag = (Tag)reader.GetValue(4);
                    post.CreateDate = DateTime.Parse(reader.GetValue(5).ToString());
                    post.IsOn = reader.GetBoolean(6);
                    post.IsPremium = reader.GetBoolean(7);
                    post.Game = reader.GetValue(8).ToString();

                    post.Player = new Player();
                    post.Player.Id = int.Parse(reader.GetString(9));
                    post.Player.Name = reader.GetValue(10).ToString();
                    post.Player.Email = reader.GetValue(11).ToString();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


            return post;
        }

        public bool PostCreate(Post p)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_post_create", conn);
            MySqlDataReader reader;


            string tst = p.Tag.ToString();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_title", p.Title));
            cmd.Parameters.Add(new MySqlParameter("p_text", p.Text));
            cmd.Parameters.Add(new MySqlParameter("p_id_player", 1));
            cmd.Parameters.Add(new MySqlParameter("p_tag", p.Tag.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_date_create", p.CreateDate));
            cmd.Parameters.Add(new MySqlParameter("p_Game", p.Game));

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

        public bool PostDelete(Post p)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_post_delete", conn);
            MySqlDataReader reader;


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_post", p.Id));

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

        public bool PostUpdate(Post p)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_post_update", conn);
            MySqlDataReader reader;


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_post", p.Id));
            cmd.Parameters.Add(new MySqlParameter("p_title", p.Title));
            cmd.Parameters.Add(new MySqlParameter("p_text", p.Text));
            cmd.Parameters.Add(new MySqlParameter("p_id_create", p.Player.Id));
            cmd.Parameters.Add(new MySqlParameter("p_tag", p.Tag));
            cmd.Parameters.Add(new MySqlParameter("p_date_create", p.CreateDate));
            cmd.Parameters.Add(new MySqlParameter("p_Game", p.Game));

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

        private string ImageGame(string game)
        {
            string result = "";

            switch (game)
            {
                case "League of Legends" :
                    result = "https://d33jl3tgfli0fm.cloudfront.net/helix/images/games/league-of-legends/box.jpg";
                    break;

                case "Heroes of the Storm":
                    result = "https://battlefy-assets.s3.amazonaws.com/helix/images/games/heroes-of-the-storm/box2.jpg";
                    break;

                case "Hearthstone":
                    result = "https://s3.amazonaws.com/battlefy-assets/bracket-generator/images/games/hearthstone/box.png";
                    break;

                case "Counter Strike Global Offensive":
                    result = "http://mixturadosneo.com/Images/csgo-logo.png";
                    break;

                case "Overwatch":
                    result = "https://pbs.twimg.com/profile_images/633856419490480128/58pBUIoE_400x400.png";
                    break;

            }

            return result;
        }
    }
}