using NeoMix.BLL;
using NeoMix.Models;
using NeoMix.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoMix.Controllers
{
    public class FanArtController : Controller
    {
        private FanArtBLL _FanArtBLL = new FanArtBLL();
        private AdminBLL _adminBLL = new AdminBLL();

        //
        // GET: /FanArt/Create
        public ActionResult Index(string id_FanArt)
        {
            FanArt f = new FanArt();
            List<FanArt> FanArt = new List<FanArt>();
            int id;

            int.TryParse(id_FanArt, out id);

            if (id != 0)
            {
                FanArtVM nvm;
                Admin a;

                f = _FanArtBLL.FanArtSelect(id);
                a = _adminBLL.SelectByNick(f.Author);

                nvm = new FanArtVM(f, a);

                return View("FanArt", nvm);
            }
            else
            {
                FanArt = _FanArtBLL.FanArtList();

                return View("index", FanArt);
            }
        }

        //
        // GET: /FanArt/Create
        public ActionResult Create()
        {
            return View("create");
        }

        //
        // GET: /FanArt/Update
        public ActionResult Update(int id_fanart)
        {
            FanArt n = _FanArtBLL.FanArtSelect(id_fanart);

            return View("update", n);
        }

        //
        // GET: /Home/ChampsPreview
        public JsonResult FanArtPreview()
        {
            string FanArt = _FanArtBLL.FanArtListByGamePreview();

            return Json(FanArt);
        }

        #region Post
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(string title, string url, string author)
        {
            FanArt f = new FanArt(title, url, author);

            bool result = _FanArtBLL.FanArtCreate(f);

            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(int id_FanArt, string title, string url, string author)
        {
            FanArt n = new FanArt(title, url, author);

            n.Id = id_FanArt;

            bool result = _FanArtBLL.SpotlightUpdate(n);

            return Json(result);
        }
        #endregion

    }
}
