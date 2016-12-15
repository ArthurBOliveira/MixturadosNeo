using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class FanArt
    {
        private int _id;
        private string _title;
        private DateTime _date;
        private string _url;
        private string _author;

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

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public FanArt(string title, string url, string author)
        {
            Title = title;
            Date = DateTime.Now;
            Url = url;
            Author = author;
        }

        public FanArt() { }
    }
}