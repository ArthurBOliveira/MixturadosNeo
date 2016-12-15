using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class View
    {
        private int _id;
        private string _page;
        private DateTime _date;
        private string _type;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public View() { }

        public View(string page, DateTime date, string type)
        {
            Page = page;
            Date = date;
            Type = type;
        }
    }
}