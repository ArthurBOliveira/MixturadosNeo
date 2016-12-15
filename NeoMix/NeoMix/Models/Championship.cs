using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class Championship
    {
        private int _id;
        private string _game;
        private string _name;
        private DateTime _date;
        private string _img;
        private string _link;
        private string _owner;
        private string _details;
        private string _prize;
        private bool _isLocal;
        private string _place;
        private bool _isPremium;
        private string _source;
        private string _stream;

        public string Stream
        {
            get { return _stream; }
            set { _stream = value; }
        }       

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        #region Attributes

        public bool IsPremium
        {
            get { return _isPremium; }
            set { _isPremium = value; }
        }

        public string Place
        {
            get { return _place; }
            set { _place = value; }
        }

        public bool IsLocal
        {
            get { return _isLocal; }
            set { _isLocal = value; }
        }

        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
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

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }

        public string Prize
        {
            get { return _prize; }
            set { _prize = value; }
        }
        #endregion

        public Championship() { }
    }
}