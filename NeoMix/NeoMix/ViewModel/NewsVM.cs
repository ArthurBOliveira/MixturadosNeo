using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.ViewModel
{
    public class NewsVM
    {
        public News News;
        public Admin Admin;

        public NewsVM(News news, Admin admin)
        {
            News = news;
            Admin = admin;
        }
    }
}