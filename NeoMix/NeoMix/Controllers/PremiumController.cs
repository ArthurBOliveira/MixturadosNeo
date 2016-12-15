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
    public class PremiumController : Controller
    {
        private ChampionshipBLL _champBLL = new ChampionshipBLL(); 

        //
        // GET: /Premium/
        public ActionResult Index()
        {
            List<Championship> champs = _champBLL.ChampionshipList();

            List<ChampsVM> champsvm = ConvertModeltoVM(champs);

            return View(champs);
        }

        private List<ChampsVM> ConvertModeltoVM(List<Championship> list)
        {
            List<ChampsVM> result = new List<ChampsVM>();

            foreach (Championship c in list)
            {
                result.Add(new ChampsVM(c.Id, c.Game, c.Name, c.Date, c.Img, c.Link, c.Details, c.IsPremium, c.Source));
            }

            return result;
        }
    }
}
