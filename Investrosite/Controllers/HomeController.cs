using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Investrosite.Models.Database;

namespace Investrosite.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = (from p in db.Posts
                        join e in db.Entrepreneurs
                        on p.eid equals e.Id
                               select new ViewModel
                              {
                                  Id = p.Id,
                                  Description = p.Description,
                                  Name = e.Name,
                                  eid=p.eid
                              }).ToList();
            return View(data);
            //var data = db.Posts.ToList();
            //return View(data);
        }
        [HttpGet]
        public ActionResult post()
        {
            return View(new Post());
        }
        [HttpPost]
        public ActionResult post(Post post)
        {
            
            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                db.Posts.Add(post);
                db.SaveChanges();
                ViewBag.Msg = "Append";
                return View();
            }
            ViewBag.Msg = "Error";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}