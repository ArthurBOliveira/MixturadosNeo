using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class News
    {
        private int _id;
        private string _img;
        private string _source;
        private string _text;
        private string _author;
        private DateTime _date;
        private string _newsType;
        private string _title;
        private bool _isPremium;
        private string _shortDesc;
        private string _comments;
        private string _url;
        private int _views;
        private string _link;
        private bool _isPublished;

        public bool IsPublished
        {
            get { return _isPublished; }
            set { _isPublished = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public int Views
        {
            get { return _views; }
            set { _views = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string NewsType
        {
            get { return _newsType; }
            set { _newsType = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public bool IsPremium
        {
            get { return _isPremium; }
            set { _isPremium = value; }
        }

        public string ShortDesc
        {
            get { return _shortDesc; }
            set { _shortDesc = value; }
        }

        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public News(string img, string url, string text, string author, string newsType, string title, string shortDesc, string link, bool isPublished)
        {
            Img = img;
            Source = url;
            Text = text;
            Author = author;
            NewsType = newsType;
            Title = title;
            Date = DateTime.Now;
            ShortDesc = shortDesc;
            Comments = "";
            Link = link;
            IsPublished = isPublished;
        }

        public News() { }
    }
}