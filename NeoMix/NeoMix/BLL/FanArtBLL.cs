using NeoMix.DAL;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.BLL
{
    public class FanArtBLL
    {
        private FanArtDAL _FanArtDAL = new FanArtDAL();

        public List<FanArt> FanArtList()
        {
            return _FanArtDAL.FanArtList();
        }

        public bool FanArtCreate(FanArt n)
        {
            return _FanArtDAL.FanArtCreate(n);
        }

        public FanArt FanArtSelect(int id_FanArt)
        {
            return _FanArtDAL.FanArtSelect(id_FanArt);
        }

        public List<FanArt> FanArtListHome(int views)
        {
            return _FanArtDAL.FanArtSelectHome(views);
        }

        public bool SpotlightUpdate(FanArt n)
        {
            return _FanArtDAL.FanArtUpdate(n);
        }

        public string FanArtListByGamePreview()
        {
            string html = "";

            List<FanArt> FanArt = _FanArtDAL.FanArtSelectHome(6);
            int i = 0;

            foreach (FanArt n in FanArt)
            {
                if (i < 3)
                {

                    //html += "<li>";
                    //html += "<div class='twelve columns'>";
                    //html += "    <div class='plain box twelve'>";
                    //html += "        <section style='height: 255px;'>";
                    //html += "            <header style='background-color: #292929'>";
                    //html += "                <h3>";
                    //html += "                    <a style='color: #c11b50' href='/FanArt?id_FanArt=" + n.Id + "'>" + n.Title + "</a>";
                    //html += "                </h3>";
                    //html += "            </header>";
                    //html += "            <figure>";
                    //html += "                <a href='/FanArt?id_FanArt=" + n.Id + "'>";
                    //html += "                    <img style='border-radius: 15px; border: solid #e51c5e;' src='" + n.Img + "' alt='@n.Img'>";
                    //html += "                </a>";
                    //html += "            </figure>";
                    //html += "        </section>";
                    //html += "    </div>";
                    //html += "</div>";
                    //html += "</li>";

                    i++;
                }
            }

            return html;
        }
    }
}