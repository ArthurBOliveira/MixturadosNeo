using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoMix.Models;
using NeoMix.DAL;

namespace NeoMix.BLL
{
    public class AdminBLL
    {
        private AdminDAL _adminDAL = new AdminDAL();

        public Admin Login(string nick, string password)
        {
            return _adminDAL.AdminLogin(nick, password);
        }

        public Admin SelectByNick(string nick)
        {
            return _adminDAL.AdminSelectByNick(nick);
        }

        public Stats ViewListByPage()
        {
            return _adminDAL.ViewListByPage();
        }

        public Stats ViewListByDate()
        {
            return _adminDAL.ViewListByDate();
        }

        public Stats ViewListByType()
        {
            return _adminDAL.ViewListByType();
        }
    }
}