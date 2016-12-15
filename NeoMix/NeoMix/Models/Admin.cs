using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class Admin
    {
        private int _id;
        private string _name;
        private string _nick;
        private string _link;
        private string _img;
        private string _desc;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Nick
        {
            get { return _nick; }
            set { _nick = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }

        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public Admin() { }

        public Admin(string name, string nick, string link, string img, string desc)
        {
            Name = name;
            Nick = nick;
            Link = link;
            Img = img;
            Desc = desc;
        }
    }
}