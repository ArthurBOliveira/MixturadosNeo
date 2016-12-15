using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class Stream
    {
        private int _id;
        private int _views;
        private string _link;
        private string _title;
        private string _logo;
        private string _game;
        private string _name;
        private string _source;
        private bool _isPremium;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Views
        {
            get { return _views; }
            set { _views = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        public string Game
        {
            get { return _game; }
            set { _game = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public bool IsPremium
        {
            get { return _isPremium; }
            set { _isPremium = value; }
        }
    }
}