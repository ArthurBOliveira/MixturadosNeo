using NeoMix.BLL;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoMix.Controllers
{
    public class PostController : Controller
    {
        private PostBLL _postBLL = new PostBLL();

        //
        // GET: /Post/Index
        public ActionResult Index()
        {
            List<Post> posts = _postBLL.PostList();

            return View("Index", posts);
        }

        //
        // GET: /Post/Create

        public ActionResult Create()
        {
            return View("Create");
        }

        //
        // POST: /Post/Create

        [HttpPost]
        public JsonResult Create(string Title, string Text, Tag Tag, string Game)
        {
            Post p = new Post(Title, Text, Tag,  Game);

            bool result = _postBLL.PostCreate(p);

            return Json(result);
        }

        //
        // GET: /Post/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Post/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Post/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
