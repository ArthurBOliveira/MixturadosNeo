using NeoMix.DAL;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.BLL
{
    public class SpotlightBLL
    {
        private NewsDAL _newsDAL = new NewsDAL();
        private SpotlightDAL _SpotlightDAL = new SpotlightDAL();

        public List<Spotlight> SpotlightList()
        {
            return _SpotlightDAL.SpotlightList();
        }

        public bool SpotlightCreate(Spotlight n)
        {
            return _SpotlightDAL.SpotlightCreate(n);
        }

        public Spotlight SpotlightSelect(int id_Spotlight)
        {
            return _SpotlightDAL.SpotlightSelect(id_Spotlight);
        }

        public bool SpotlightUpdate(Spotlight n)
        {
            return _SpotlightDAL.SpotlightUpdate(n);
        }

        public bool SpotlightDelete(int id)
        {
            return _SpotlightDAL.SpotlightDelete(id);
        }

        public void RefreshSpotlight()
        {
            List<News> AllNews = _newsDAL.NewsList();
            int x = 0;

            News NewsTop = new News();
            News NewsLeft = new News();
            News NewsMid = new News();
            News NewsRight = new News();

            foreach (News n in AllNews)
            {
                if (n.Date >= DateTime.Now.AddDays(-3))
                    n.Views = _newsDAL.getNewsViews(n.Id);
            }

            x = AllNews.Max(n => n.Views);
            NewsTop = AllNews.Find(n => n.Views == x);            
            AllNews.Remove(NewsTop);

            x = AllNews.Max(n => n.Views);
            NewsLeft = AllNews.Find(n => n.Views == x);
            AllNews.Remove(NewsLeft);

            x = AllNews.Max(n => n.Views);
            NewsMid = AllNews.Find(n => n.Views == x);
            AllNews.Remove(NewsMid);

            x = AllNews.Max(n => n.Views);
            NewsRight = AllNews.Find(n => n.Views == x);       


            //foreach (News n in AllNews)
            //{
            //    if (n.Date >= DateTime.Now.AddDays(-3))
            //    {
            //        n.Views = _newsDAL.getNewsViews(n.Id);

            //        if (NewsTop.Views < n.Views)
            //        {

            //            NewsTop = n;
            //        }
            //        else
            //        {
            //            if (NewsLeft.Views < n.Views)
            //            {
            //                NewsLeft = n;
            //            }
            //            else
            //            {
            //                if (NewsMid.Views < n.Views)
            //                {
            //                    NewsMid = n;
            //                }
            //                else
            //                {
            //                    if (NewsRight.Views < n.Views)
            //                    {
            //                        NewsRight = n;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            _SpotlightDAL.SpotlightClean();

            Spotlight SpotTop = new Spotlight("3", "2", NewsTop.Img, NewsTop.Title, "http://mixturadosneo.com/news?id_news=" + NewsTop.Id, "News");
            Spotlight SpotLeft = new Spotlight("1", "1", NewsLeft.Img, NewsLeft.Title, "http://mixturadosneo.com/news?id_news=" + NewsLeft.Id, "News");
            Spotlight SpotMid = new Spotlight("1", "1", NewsMid.Img, NewsMid.Title, "http://mixturadosneo.com/news?id_news=" + NewsMid.Id, "News");
            Spotlight SpotRight = new Spotlight("1", "1", NewsRight.Img, NewsRight.Title, "http://mixturadosneo.com/news?id_news=" + NewsRight.Id, "News");

            _SpotlightDAL.SpotlightCreate(SpotTop);
            _SpotlightDAL.SpotlightCreate(SpotLeft);
            _SpotlightDAL.SpotlightCreate(SpotMid);
            _SpotlightDAL.SpotlightCreate(SpotRight);
        }
    }
}