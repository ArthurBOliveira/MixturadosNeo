using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NeoMix.Models;
using NeoMix.ViewModel;
using NeoMix.BLL;
using NeoMix.Util;
using System.Net;

namespace NeoMix.Controllers
{
    public class HomeController : Controller
    {
        private ChampionshipBLL _champBLL = new ChampionshipBLL();
        private NewsBLL _newsBLL = new NewsBLL();
        private HtmlMinerStream _minerStream = new HtmlMinerStream();
        private HtmlMiner _miner = new HtmlMiner();
        private RSSFeed _rssFeed = new RSSFeed();
        private FanArtBLL _fanArtBLL = new FanArtBLL();
        private SpotlightBLL _spotlightBLL = new SpotlightBLL();
        private AdminBLL _adminBLL = new AdminBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        //
        // GET: /Home/
        public ActionResult Index()
        {
            int views = 9;

            List<ChampsVM> champs = ConvertListModeltoVM(_champBLL.ChampionshipListHome(views));
            List<News> news = _newsBLL.NewsList();
            //List<Stream> stream = _minerStream.MineHome(views);
            List<FanArt> fanarts = _fanArtBLL.FanArtListHome(views);
            List<Spotlight> spotlight = _spotlightBLL.SpotlightList();

            foreach (News _news in news)
            {
                _news.NewsType = _rssFeed.DefineType(_news.Text, _news.Title);
            }

            List<News> newsHot = new List<News>();
            List<News> newsLOL = new List<News>();
            List<News> newsCS = new List<News>();
            List<News> newsCBLOL = new List<News>();
            List<News> newsESports = new List<News>();
            List<News> newsDOTA = new List<News>();
            List<News> newsOW = new List<News>();

            #region Gambi
            for (int i = 0; i < 3; i++)
            {
                newsHot.Add(news[i]);
                news.Remove(news[i]);
            }

            for (int i = 0; i < news.Count; i++)
            {
                if(news[i].NewsType == "League of Legends")
                    newsLOL.Add(news[i]);
                if (newsLOL.Count >= 3)
                    break;
            }

            for (int i = 0; i < news.Count; i++)
            {
                if (news[i].NewsType == "E-Sports")
                    newsESports.Add(news[i]);
                if (newsESports.Count >= 3)
                    break;
            }

            for (int i = 0; i < news.Count; i++)
            {
                if (news[i].NewsType == "Counter Strike")
                    newsCS.Add(news[i]);
                if (newsCS.Count >= 3)
                    break;
            }

            for (int i = 0; i < news.Count; i++)
            {
                if (news[i].NewsType == "CBLOL")
                    newsCBLOL.Add(news[i]);
                if (newsCBLOL.Count >= 3)
                    break;
            }

            for (int i = 0; i < news.Count; i++)
            {
                if (news[i].NewsType == "DOTA")
                    newsDOTA.Add(news[i]);
                if (newsDOTA.Count >= 3)
                    break;
            }

            for (int i = 0; i < news.Count; i++)
            {
                if (news[i].NewsType == "Overwatch")
                    newsOW.Add(news[i]);
                if (newsOW.Count >= 3)
                    break;
            }

            #endregion

            HomeVM hvm = new HomeVM(champs, newsHot, newsLOL, newsESports, newsCS, newsOW, newsDOTA, newsCBLOL, spotlight);

            //_rssFeed.FindPerWords();

            return View(hvm);
        }


        //GET: /Home/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        //
        // GET: /Home/About
        public ActionResult About()
        {
            return View("About");
        }

        //
        // GET: /Home/Contact
        public ActionResult Contact()
        {
            return View("Contact");
        }

        //
        // GET: /Home/Premium
        public ActionResult Premium()
        {
            return View("Premium");
        }

        //
        // GET: /Home/Event
        public ActionResult Event(int Event)
        {
            ChampsVM vm = new ChampsVM(Event, "LOL", "CMLOL", DateTime.Now, "", "www.google.com", "Altos HueHueHue", true, "Fonte");

            //ChampsVM vm = ConvertModeltoVM(_champBLL.ChampionshipSelect(Event));

            return View("Event", vm);
        }

        //
        // GET: /Home/Champ
        public ActionResult Champ(int id_champ)
        {
            Championship c = _champBLL.ChampionshipSelect(id_champ);

            return View("Champ", c);
        }

        //
        // GET: /Home/Champs
        public ActionResult Champs(string game)
        {
            List<ChampsVM> list = new List<ChampsVM>();
            List<Championship> fullList = new List<Championship>();

            if (string.IsNullOrEmpty(game))
                fullList = _champBLL.ChampionshipList();
            else
                fullList = _champBLL.ChampionshipListByGame(game);

            list = ConvertListModeltoVM(fullList);

            return View(list);
        }

        //
        // GET: /Home/ChampsPreview
        public JsonResult ChampsNewsPreview()
        {
            string champs = _champBLL.ChampionshipListByGamePreview();

            return Json(champs);
        }

        //
        // GET: /Home/ChampsPreview
        public ActionResult SpotlightCreate()
        {
            return View("SpotlightCreate");
        }
        //
        // GET: /Home/ChampsPreview
        public ActionResult SpotlightUpdate(int id_spotlight)
        {
            Spotlight s = _spotlightBLL.SpotlightSelect(id_spotlight);

            return View("SpotlightUpdate", s);
        }

        //
        // GET: /Home/Stats
        public ActionResult Stats()
        {
            StatsVM svm = new StatsVM(_adminBLL.ViewListByPage(), _adminBLL.ViewListByDate(), _adminBLL.ViewListByType());

            return View("Stats", svm);
        }

        //
        // GET: /Home/Login
        public ActionResult Login()
        {
            return View("Login");
        }

        #region Post
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult LoginScript(string login, string pass)
        {
            bool result = false;

            if (login == "Vp9J#cY^2Qjc6qpbz%g" && pass == "6FMD7$r2mTru5Ke!dAs")
            {
                _sessionHelper.Temp = true;

                result = true;
            }            

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreateChamps(string src, string html)
        {
            bool result = false;

            html = html.Replace('"', '\'');

            if (src == "LOL")
                result = _miner.MineLol(html);

            if (src == "XFIRE")
                result = _miner.MineXFire(html);

            if (src == "Battlefy")
                result = _miner.MineBattlefy(html);

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SpotlightCreate(string width, string height, string img, string title, string link, string type)
        {
            Spotlight s = new Spotlight(width, height, img, title, link, type);

            bool result = _spotlightBLL.SpotlightCreate(s);

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SpotlightUpdate(int id, string width, string height, string img, string title, string link, string type)
        {
            Spotlight s = new Spotlight(width, height, img, title, link, type);

            s.Id = id;

            bool result = _spotlightBLL.SpotlightUpdate(s);

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SpotlightDelete(int id)
        {
            bool result = _spotlightBLL.SpotlightDelete(id);

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreateChampsLink(string src, string link, string extra)
        {
            bool result = false;

            if (src == "LOL")
                result = _miner.MineLolJson(link);

            if (src == "XFIRE")
                result = _miner.MineXFire(link);

            if (src == "Battlefy")
                result = _miner.MineBattlefyJson(link, extra);

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Battlefy()
        {
            bool result = false;

            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/557aafd1abe0a24200d6617e/tournaments", "panda-gaming");
            //result= _miner.MineBattlefyJson("https://api.battlefy.com/organizations/566236b559192c5300d3fe02/tournaments", "ubisoft-brasil");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/5595744c9a2d9f4300c2fcc0/tournaments", "legendsbr");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/53ceda58da86072c094be449/tournaments", "randomcs");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/55c7f5ef6fd5257e008558fb/tournaments", "hawk-team");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/569e887a89036d4e00c9adea/tournaments", "xgaming");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/54d9b8d599fe6f42002b17b4/tournaments", "heroeshype");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/5658843b5081fa680002a8f2/tournaments", "IronstormTV.com");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/566437a8ea81eb45009b9d8f/tournaments", "summoners-torneios-online");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/56d8bbc2cdb9ed9312cdd08f/tournaments", "champions-br-e-sports");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/559ae9ea4b3629450023b9dc/tournaments", "star-gaming-league");
            result = _miner.MineBattlefyJson("https://api.battlefy.com/organizations/562d2e9a9f1d864600946355/tournaments", "xstars-gaming");

            result = _miner.MineBattlefyJson("https://dtmwra1jsgyb0.cloudfront.net/organizations/55987cfa2c60504500041cdf/tournaments", "mixturados");
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult XFire()
        {
            bool result = false;

            result = _miner.MineXFireJson("http://xfire.com/tournaments/loadall?ms=1454483661200&querytype=0&page=1&status=1&region=3");
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult GC()
        {
            bool result = false;

            result = _miner.MineGC("https://www.gamersclub.com.br/campeonatos");
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ESL()
        {
            bool result = false;

            result = _miner.MineESL();
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ESLPremier()
        {
            bool result = false;

            result = _miner.MineESLPremier();
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EsportGG()
        {
            bool result = false;

            result = _miner.MineESportGG();
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult RazerArena()
        {
            bool result = false;

            result = _miner.MineRazerArena();
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult FeedNews()
        {
            string result = _rssFeed.FeedRSS();
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SpotRefresh()
        {
            bool result = false;

            _spotlightBLL.RefreshSpotlight();
            return Json(result);
        }

        public ActionResult JsonFeedApp()
        {
            string result = _newsBLL.NewsFeedApp();

            return Content(result, "application/json");
        }
        #endregion

        #region Private
        private List<ChampsVM> ConvertListModeltoVM(List<Championship> list)
        {
            List<ChampsVM> result = new List<ChampsVM>();

            foreach (Championship c in list)
            {
                result.Add(new ChampsVM(c.Id, c.Game, c.Name, c.Date, c.Img, c.Link, c.Details, c.IsPremium, c.Source));
            }

            return result;
        }

        private ChampsVM ConvertModeltoVM(Championship c)
        {
            ChampsVM result = new ChampsVM(c.Id, c.Game, c.Name, c.Date, c.Img, c.Link, c.Details, c.IsPremium, c.Source);

            return result;
        }
        #endregion
    }
}
