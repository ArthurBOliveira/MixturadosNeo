using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class Coach
    {
        private int _id;
        private string _name;
        private string _nick;
        private string _game;
        private string _link;
        private string _img;
        private string _desc;
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

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

        public string Game
        {
            get { return _game; }
            set { _game = value; }
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

        public Coach() { }

        public Coach(string name, string nick, string game, string link, string img, string desc)
        {
            Name = name;
            Nick = game;
            Link = link;
            Img = img;
            Desc = desc;
            Date = DateTime.Now;
        }
    }
}