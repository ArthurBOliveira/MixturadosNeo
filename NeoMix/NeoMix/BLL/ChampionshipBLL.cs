using NeoMix.DAL;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.BLL
{
    public class ChampionshipBLL
    {
        private ChampionshipDAL _champDAL = new ChampionshipDAL();

        public bool ChampionshipCreate(Championship c, List<Championship> champs)
        {
            return _champDAL.ChampionshipCreate(c, champs);
        }

        public List<Championship> ChampionshipListByGame(string game)
        {
            List<Championship> champs = _champDAL.ChampionshipListByGame(game);
            Championship ad = new Championship();

            ad.Date = new DateTime(1993, 4, 28);

            for (int i = 0; i <= champs.Count; i++)
            {
                if (i == 21)
                {
                    ad.Name = "topo";
                    champs.Insert(i + 0, ad);
                }
                if (i == 43)
                {
                    ad.Name = "interno";
                    champs.Insert(i + 0, ad);
                }
                if (i == 65)
                {
                    ad.Name = "baixo";
                    champs.Insert(i + 0, ad);
                }
            }

            return champs;
        }

        public List<Championship> ChampionshipList()
        {
            List<Championship> champs = _champDAL.ChampionshipList();
            Championship ad = new Championship();

            ad.Date = new DateTime(1993, 4, 28);

            for (int i = 0; i <= champs.Count; i++)
            {
                if (i == 21)
                {
                    ad.Name = "topo";
                    champs.Insert(i + 0, ad);
                }
                if (i == 43)
                {
                    ad.Name = "interno";
                    champs.Insert(i + 0, ad);
                }
                if (i == 65)
                {
                    ad.Name = "baixo";
                    champs.Insert(i + 0, ad);
                }
            }

            return champs;
        }

        public Championship ChampionshipSelect(int id_champ)
        {
            return _champDAL.ChampionshipSelect(id_champ);
        }

        public List<Championship> ChampionshipListHome(int p)
        {
            return _champDAL.ChampionshipSelectHome(p);
        }

        public string ChampionshipListByGamePreview()
        {
            string html = "";

            List<Championship> champs = _champDAL.ChampionshipSelectHome(6);
            int i = 0;

            foreach (Championship c in champs)
            {
                if (i < 2)
                {
                    //html += "<li class=\"twelve columns\"><div class=\"left\" style=\"margin-right: 5px;\">";
                    //html += "<img style=\"height: 74px; width: 74px;\" src=\"";
                    //html += c.Img + "\">";                
                    //html += "</div><div class=\"latest-news-headline\"><h4><a href=\"";
                    //html += "/home/champ?id_champ=" + c.Id.ToString() + "\">";
                    //html += c.Name + "</a>";
                    //html += "</h4>";
                    //html += "<span class=\"bubble\"><span>" + c.Date.ToString("dd/MM/yyyy") + "</span></span>";
                    //html +="</div></li>";

                    html += "<article style='height: 115px;' class=\"twelve columns\"><div class=\"four columns\" style=\"border: 0; margin-right: 5px;\">";
                    html += "<a class=\"download-type-icon\" href=\"/home/champ?id_champ=" + c.Id + "\">";
                    html += "<img style=\"height: 100px; border-radius: 15px; width: 100px;\" src=\"" + c.Img + "\"></a> </div><div class=\"figure-download\"><h5>";
                    html += c.Game;
                    html += "</h5><h5 style=\"color: #fc7100\">";
                    html += c.Name;
                    html += "</h5><h5 style='float: right;'><img src=\"/Images/NeoDesign/Calendar.png\" style=\"float: left; width: 27px; margin-right: 5px;\" /><span style=\"color: #fc7100\"><span>" + c.Date.ToString("dd/MM/yyyy") + "</span></span></h5></div></article>";

                    i++;
                }
            }

            return html;
        }

        #region
        public List<View> ViewList()
        {
            return _champDAL.ViewList();
        }
        #endregion
    }
}