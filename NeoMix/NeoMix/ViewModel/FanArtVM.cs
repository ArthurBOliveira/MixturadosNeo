using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.ViewModel
{
    public class FanArtVM
    {
        public FanArt FanArt;
        public Admin Admin;

        public FanArtVM(FanArt fanart, Admin admin)
        {
            FanArt = fanart;
            Admin = admin;
        }
    }
}