using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Models
{
    public class Player
    {
        private int _id;
        private string _name;
        private string _email;
        private string _pass;
        private DateTime _createDate;

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

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public Player() { }

        public Player(string name, string email, string pass)
        {
            Name = name;
            Email = Email;
            Pass = pass;
            CreateDate = DateTime.Now;
        }
    }
}