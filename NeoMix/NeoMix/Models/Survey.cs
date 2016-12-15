using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class Survey
    {
        private int _id;        
        private string _title;
        private List<string> _options;
        private DateTime _date;

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

        public List<string> Options
        {
            get { return _options; }
            set { _options = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Survey() { }

        public Survey(string title, List<string> options, DateTime date)
        {
            Title = title;
            Options = options;
            Date = date;
        }

    }
}