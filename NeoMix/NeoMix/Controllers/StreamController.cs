using NeoMix.Models;
using NeoMix.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoMix.Controllers
{
    public class StreamController : Controller
    {
        private HtmlMinerStream _minerStream = new HtmlMinerStream();

        //
        // GET: /Stream/
        public ActionResult Index()
        {
            List<Stream> streams = _minerStream.MineTwitch();

            return View("Index", streams);
        }

    }
}
