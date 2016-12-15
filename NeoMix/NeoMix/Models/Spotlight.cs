using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class Spotlight
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _width;

        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }
        private string _height;

        public string Height
        {
            get { return _height; }
            set { _height = value; }
        }
        private string _img;


        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }
        private string _link;

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Spotlight() { }

        public Spotlight(string width, string height, string img, string title, string link, string type) 
        {
            Width = width;
            Height = height;
            Img = img;
            Title = title;
            Link = link;
            Type = type;
        }
    }
}