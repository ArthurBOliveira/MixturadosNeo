using NeoMix.DAL;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.BLL
{
    public class PostBLL
    {
        private PostDAL _postDAL = new PostDAL();

        public List<Post> PostList()
        {
            return _postDAL.PostList();
        }

        public List<Post> PostListByTag(Tag tag)
        {
            return _postDAL.PostListByTag(tag);
        }

        public List<Post> PostListByPlayer(int id_player)
        {
            return _postDAL.PostListByPlayer(id_player);
        }

        public Post PostSelect(int id_post)
        {
            return _postDAL.PostSelect(id_post);
        }

        public bool PostCreate(Post post)
        {
            return _postDAL.PostCreate(post);
        }

        public bool PostUpdate(Post post)
        {
            return _postDAL.PostUpdate(post);
        }

        public bool PostDelete(Post post)
        {
            return _postDAL.PostDelete(post);
        }
    }
}