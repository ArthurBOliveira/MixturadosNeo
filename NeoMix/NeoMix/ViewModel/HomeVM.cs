using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.ViewModel
{
    public class HomeVM
    {
        public List<ChampsVM> Champs;
        public List<News> NewsHot;
        //public List<Stream> Streams;
        public List<Spotlight> Spotlight;

        public List<News> NewsLOL;
        public List<News> NewsCS;
        public List<News> NewsEsports;
        public List<News> NewsCBLOL;
        public List<News> NewsDOTA;
        public List<News> NewsOW;

        public HomeVM(List<ChampsVM> champs, List<News> newsHot, List<News> newsLol, List<News> newsEsports, List<News> newsCSGO, List<News> newsOW, List<News> newsDOTA, List<News> newsCBLOL, List<Spotlight> spotlight)
        {
            Champs = champs;
            NewsHot = newsHot;
            //Streams = streams;
            Spotlight = spotlight;

            NewsLOL = newsLol;
            NewsCS = newsCSGO;
            NewsEsports = newsEsports;
            NewsCBLOL = newsCBLOL;
            NewsDOTA = newsDOTA;
            NewsOW = newsOW;
        }
    }
}