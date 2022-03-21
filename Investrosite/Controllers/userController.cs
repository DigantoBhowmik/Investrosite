using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Investrosite.Models.Database;
using System.Web.Security;
using Investrosite.Auth;

namespace Investrosite.Controllers
{

    public class userController : Controller
    {
        // GET: register
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Entrepreneur e, Investor i)
        {
            if(e.Role== "Entrepreneur")
            {
                if (ModelState.IsValid)
                {
                    investrositeEntities1 db = new investrositeEntities1();
                    db.Entrepreneurs.Add(e);
                    db.SaveChanges();
                    ViewBag.Msg = "Append";
                    Session["Name"] = e.Name.ToString();
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                ViewBag.Msg = "Error";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    investrositeEntities1 db = new investrositeEntities1();
                    db.Investors.Add(i);
                    db.SaveChanges();
                    ViewBag.Msg = "Append";
                    Session["Name"] = i.Name.ToString();
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                ViewBag.Msg = "Error";
                return View();
            }
            ViewBag.Msg = "Error2";
            return View();
            
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Entrepreneur e, Investor i)
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = (from n in db.Entrepreneurs
                        where n.Password.Equals(e.Password) &&
                        n.Email.Equals(e.Email)
                        select n).FirstOrDefault();
            var data1 = (from s in db.Investors
                        where s.Password.Equals(i.Password) &&
                        s.Email.Equals(i.Email)
                        select s).FirstOrDefault();
            if (data != null )
            {
                Session["Name"] = data.Name.ToString();
                Session["Role"] = data.Role.ToString();
                Session["Id"] = data.Id;
                ViewBag.Msg = "Append";
                return RedirectToAction(actionName: "Index", controllerName: "Home"); 
            }
            else if(data1 != null)
            {
                Session["Name"] = data1.Name.ToString();
                Session["Role"] = data1.Role.ToString();
                Session["Id"] = data1.Id;
                ViewBag.Msg = "Append";
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            ViewBag.Msg = "Your information is not correct!";
            return View();
        }
        [HttpGet]
        public ActionResult editprofile()
        {
            if(Session["Name"] != null)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = db.Entrepreneurs.Find(Session["Id"]);
                return View(data);
            }
            return RedirectToAction(actionName: "Login", controllerName: "User");

        }
        [HttpPost]
        public ActionResult editprofile(Entrepreneur e)
        {
            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = db.Entrepreneurs.Find(Session["Id"]);
                db.Entry(data).CurrentValues.SetValues(e);
                db.SaveChanges();
                ViewBag.Msg = "Append";
                Session["Name"] = data.Name.ToString();
                Session["Role"] = data.Role.ToString();
                Session["Id"] = data.Id;
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return View();
        }
        [HttpGet]
        public ActionResult mypost()
        {
            if (Session["Name"] != null)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var id = (int)Session["Id"];
                var data = (from p in db.Posts
                            join e in db.Entrepreneurs
                            on p.eid equals e.Id
                            where p.eid == id
                            select new ViewModel
                            {
                                Id = p.Id,
                                Description = p.Description,
                                Name = e.Name,
                                eid = p.eid
                            }).ToList();
                return View(data);
            }
            return RedirectToAction(actionName: "Login", controllerName: "User");
            //var data = db.Posts.Where(n => n.eid==id).ToList();
            //return View(data);
        }
        [HttpGet]
        public ActionResult profile(int id)
        {
            if (Session["Name"] != null)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = (from n in db.Entrepreneurs where n.Id == id select n).FirstOrDefault();
                return View(data);

            }
            return RedirectToAction(actionName: "Login", controllerName: "User");
        }
        [HttpGet]
        public ActionResult showpost(int id)
        {
            if (Session["Name"] != null)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = (from p in db.Posts
                            join e in db.Entrepreneurs
                            on p.eid equals e.Id
                            where p.eid == id
                            select new ViewModel
                            {
                                Id = p.Id,
                                Description = p.Description,
                                Name = e.Name,
                                eid = p.eid
                            }).ToList();
                return View(data);
            }
            return RedirectToAction(actionName: "Login", controllerName: "User");
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}