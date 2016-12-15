using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class Post
    {
        private int _id;
        private string _title;
        private string _text;
        private Player _player;
        private Tag _tag;
        private DateTime _createDate;
        private bool _isOn;
        private bool _isPremium;
        private string _game;
        private string _img;

        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }

        public string Game
        {
            get { return _game; }
            set { _game = value; }
        }

        public bool IsPremium
        {
            get { return _isPremium; }
            set { _isPremium = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public Tag Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public bool IsOn
        {
            get { return _isOn; }
            set { _isOn = value; }
        }

        public Post() { }

        public Post(string title, string text, Tag tag, string game)
        {
            Title = title;
            Text = text;
            Tag = tag;
            CreateDate = DateTime.Now;
            Game = game;
        }
    }
}