using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace NeoMix.Util
{
    public class FaceMiner
    {
        public bool MineFace()
        {
            bool result = false;
            Championship c = new Championship();           

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString("https://www.facebook.com/mixturadosneo/");

            string[] news = html.Split(new string[] { "<div class=\"" }, StringSplitOptions.None);

            for (int i = 1; i < news.Length; i++)
            {
                string[] aux = news[i].Split('"');

                c.Name = aux[15].Split('<')[0].Substring(1);
                c.Link = "https://www.gamersclub.com.br" + aux[4];
                c.Img = "https://www.gamersclub.com.br" + aux[10];
                c.Date = DateTime.Parse(aux[21].Split('>')[2].Substring(1, 10));
                c.Game = "CSGO";
                c.Owner = "GamersClub";
                c.Details = "";
                c.Owner = "";
                c.Place = "";
                c.Prize = "";
                c.IsLocal = false;

                c = new Championship();
            }

            return result;
        }
    }
}