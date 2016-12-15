using NeoMix.BLL;
using NeoMix.Models;
using NeoMix.Util;
using NeoMix.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoMix.Controllers
{
    public class NewsController : Controller
    {
        private NewsBLL _newsBLL = new NewsBLL();
        private AdminBLL _adminBLL = new AdminBLL();
        private RSSFeed _rssFeed = new RSSFeed();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        public ActionResult Index(string id_news)
        {
            News n = new News();
            List<News> News = new List<News>();
            int id;

            int.TryParse(id_news, out id);

            if (id != 0)
            {
                NewsVM nvm;
                Admin a;

                n = _newsBLL.NewSelect(id);
                a = _adminBLL.SelectByNick(n.Author);

                nvm = new NewsVM(n, a);

                return View("News", nvm);
            }
            else
            {
                if(id_news != null)
                    News = _newsBLL.NewsListBySubTitle(id_news);
                else
                    News = _newsBLL.NewsList();

                foreach (News news in News)
                {
                    news.NewsType = _rssFeed.DefineType(news.Text, news.Title);
                }

                return View("index", News);
            }
        }

        //
        // GET: /News/Create
        public ActionResult Create()
        {
            if (_sessionHelper.Temp == true)
                return View("create");
            else
                return View("../Home/Login");
        }

        //
        // GET: /News/Update
        public ActionResult Update(int id_news)
        {
            if (_sessionHelper.Temp == true)
            {
                News n = _newsBLL.NewSelect(id_news);

                if (n.Id == 0)
                    n = _newsBLL.NewsSelectBot(id_news);

                return View("update", n);
            }
            else
                return View("../Home/Login");

        }

        public ActionResult BaseNews(string id_news)
        {
            if (_sessionHelper.Temp == true)
            {
                News n = new News();
                List<News> News = new List<News>();
                int id;

                int.TryParse(id_news, out id);

                if (id != 0)
                {
                    NewsVM nvm;
                    Admin a;

                    n = _newsBLL.NewSelect(id);
                    a = _adminBLL.SelectByNick(n.Author);

                    nvm = new NewsVM(n, a);

                    return View("News", nvm);
                }
                else
                {
                    News = _newsBLL.NewsBaseList();

                    return View("index", News);
                }
            }
            else
                return View("../Home/Login");
        }

        //
        // GET: /Home/ChampsPreview
        public JsonResult NewsPreview()
        {
            string news = _newsBLL.NewsListByGamePreview();

            return Json(news);
        }

        //
        // GET: /Home/ChampsPreview
        public JsonResult NewsPreviewSource(string source)
        {
            string news = _newsBLL.NewsListByGamePreview(source);

            return Json(news);
        }

        //
        // GET: /News/Unplished
        public ActionResult Unplished(int id_news)
        {
            if (_sessionHelper.Temp == true)
            {
                News n = _newsBLL.NewsSelectBot(id_news);

                NewsVM nvm;
                Admin a;

                a = _adminBLL.SelectByNick(n.Author);

                nvm = new NewsVM(n, a);

                return View("News", nvm);
            }
            else
                return View("../Home/Login");
        }

        #region Post
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(string title, string text, string author, string url, string img, string newsType, string shortDesc, string link)
        {
            News n = new News(img, url, text, author, newsType, title, shortDesc, link, true);

            bool result = _newsBLL.NewsCreate(n);

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(int id_news, string title, string text, string author, string url, string img, string newsType, string shortDesc, string comments, string link, bool isPublished)
        {
            News n = new News(img, url, text, author, newsType, title, shortDesc, link, isPublished);

            n.Id = id_news;
            n.Comments = comments;

            bool result = _newsBLL.NewUpdate(n);

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CheckRSS()
        {
            string result = _rssFeed.FeedRSS();

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(int id_news)
        {
            bool result = _newsBLL.NewsDelete(id_news);

            return Json(result);
        }
        #endregion
    }
}
