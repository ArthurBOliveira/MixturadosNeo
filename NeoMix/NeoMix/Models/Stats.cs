using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class Stats
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private List<int> _views;

        public List<int> Views
        {
            get { return _views; }
            set { _views = value; }
        }
        private List<string> _collumn;

        public List<string> Collumn
        {
            get { return _collumn; }
            set { _collumn = value; }
        }
        private List<DateTime> _date;

        public List<DateTime> Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Stats() 
        {
            Date = new List<DateTime>();
            Views = new List<int>();
            Collumn = new List<string>();
        }
    }
}