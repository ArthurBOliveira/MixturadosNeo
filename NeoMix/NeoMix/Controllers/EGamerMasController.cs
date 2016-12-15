using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoMix.Controllers
{
    public class EGamerMasController : Controller
    {
        //
        // GET: /EGamerMas/

        public ActionResult Index()
        {
            return View();
        }

        private int RaffleFullRandom(List<int> FaceId)
        {
            Random rng = new Random(DateTime.Now.Second);
            int result;

            result = rng.Next(FaceId.Count);

            result = FaceId[result];

            return result;
        }
    }
}
