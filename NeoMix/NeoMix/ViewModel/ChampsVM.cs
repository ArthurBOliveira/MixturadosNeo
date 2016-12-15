using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.ViewModel
{
    public class ChampsVM
    {
        public int Id;
        public string Game;
        public string Name;
        public DateTime Date;
        public string Img;
        public string Url;
        public string ShortDesc;
        public bool IsPremium;
        public string Source;
        public string Stream;

        public ChampsVM(int id, string game, string name, DateTime date, string img, string url, string shortDesc, bool isPremium, string source)
        {
            Id = id;
            Game = game;            
            Name = name.Length > 25 ? name.Substring(0, 25) + "..." : name;
            Date = date;
            Img = img;
            Url = url;
            ShortDesc = shortDesc;
            IsPremium = isPremium;
            Source = source;
        }
    }
}