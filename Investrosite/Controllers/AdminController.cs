using Investrosite.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Investrosite.Models.Database;
using System.Web.Security;


namespace Investrosite.Controllers
{
    [AdminAccess]
    public class AdminController : Controller
    {
        // GET: Admin
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult login(Admin e)
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = (from n in db.Admins
                        where n.Password.Equals(e.Password) &&
                        n.Email.Equals(e.Email)
                        select n).FirstOrDefault();
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.Email, true);
                Session["Name"] = data.Name.ToString();
                Session["Role"] = data.Role.ToString();
                Session["Id"] = data.Id.ToString();
                ViewBag.Msg = "Append";
                return RedirectToAction(actionName: "Home", controllerName: "Admin");
            }
            ViewBag.Msg = "Error";
            return View();
        }

        
        [HttpGet]
        public ActionResult Home()
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Posts.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Adminlist()
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Admins.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Entrepreneurlist()
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Entrepreneurs.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Managerlist()
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Managers.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Investorlist()
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Investors.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult CreateManager()
        {
            return View(new Manager());
        }
        [HttpPost]
        public ActionResult CreateManager(Manager add)
        {

            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                db.Managers.Add(add);
                db.SaveChanges();
                ViewBag.Msg = "Added Successfully";
                return RedirectToAction(actionName: "Managerlist", controllerName: "Admin");
            }
            ViewBag.Msg = "Error";
            return View();
        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View(new Admin());
        }
        [HttpPost]
        public ActionResult CreateAdmin(Admin add)
        {

            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                db.Admins.Add(add);
                db.SaveChanges();
                ViewBag.Msg = "Added Successfully";
                return RedirectToAction(actionName: "Adminlist", controllerName: "Admin");
            }
            ViewBag.Msg = "Error";
            return View();
        }

        [HttpGet]
        public ActionResult DeleteAdmin(int id)
        {
            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = (from u in db.Admins
                            where u.Id == id
                            select u).FirstOrDefault();
                db.Admins.Remove(data);
                db.SaveChanges();
                return RedirectToAction(actionName: "Adminlist", controllerName: "Admin");
            }
            return View();

        }

        [HttpGet]
        public ActionResult DeleteManager(int id)
        {
            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = (from u in db.Managers
                            where u.Id == id
                            select u).FirstOrDefault();
                db.Managers.Remove(data);
                db.SaveChanges();
                return RedirectToAction(actionName: "Managerlist", controllerName: "Admin");
            }
            return View();

        }

        [HttpGet]
        public ActionResult DeleteEntrepreneur(int id)
        {
            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = (from u in db.Entrepreneurs
                            where u.Id == id
                            select u).FirstOrDefault();
                db.Entrepreneurs.Remove(data);
                db.SaveChanges();
                return RedirectToAction(actionName: "Entrepreneurlist", controllerName: "Admin");
            }
            return View();

        }


        [HttpGet]
        public ActionResult DeleteInvestor(int id)
        {
            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = (from u in db.Investors
                            where u.Id == id
                            select u).FirstOrDefault();
                db.Investors.Remove(data);
                db.SaveChanges();
                return RedirectToAction(actionName: "Investorlist", controllerName: "Admin");
            }
            return View();

        }

        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = (from n in db.Admins where n.Id == id select n).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin add)
        {
            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = (from n in db.Admins where n.Id == add.Id select n).FirstOrDefault();
                db.Entry(data).CurrentValues.SetValues(add);
                db.SaveChanges();
                return RedirectToAction(actionName: "Adminlist", controllerName: "Admin");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditManager(int id)
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = (from n in db.Managers where n.Id == id select n).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult EditManager(Manager add)
        {
            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                var data = (from n in db.Managers where n.Id == add.Id select n).FirstOrDefault();
                db.Entry(data).CurrentValues.SetValues(add);
                db.SaveChanges();
                return RedirectToAction(actionName: "Managerlist", controllerName: "Admin");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Paymentlist()
        {

            investrositeEntities1 db = new investrositeEntities1();
            var data = (from p in db.Payments
                        join e in db.Entrepreneurs
                        on p.Eid equals e.Id
                        join i in db.Investors
                        on p.Iid equals i.Id
                        select new ViewModel1
                        {
                            Id = p.Id,
                            Amount = p.Amount,
                            E_Name = e.Name,
                            I_Name = i.Name
                        }).ToList();
            return View(data);
            //var data = db.Payments.ToList();
            //return View(data);
        }


        [HttpGet]
        public ActionResult SearchEntrepreneur(string search)
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Entrepreneurs.Where(x => x.Name.StartsWith(search) || search == null).ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult SearchInvestor(string search)
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Investors.Where(x => x.Name.StartsWith(search) || search == null).ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult SearchManager(string search)
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Managers.Where(x => x.Name.StartsWith(search) || search == null).ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult SearchAdmin(string search)
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Admins.Where(x => x.Name.StartsWith(search) || search == null).ToList();
            return View(data);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Report()
        {
            return View(new Report());
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Report(Report add)
        {

            if (ModelState.IsValid)
            {
                investrositeEntities1 db = new investrositeEntities1();
                db.Reports.Add(add);
                db.SaveChanges();
                ViewBag.Msg = "Added Successfully";
                return RedirectToAction(actionName: "Report", controllerName: "Admin");
            }
            ViewBag.Msg = "Error";
            return View();
        }

        [HttpGet]
        public ActionResult Reportlist()
        {
            investrositeEntities1 db = new investrositeEntities1();
            var data = db.Reports.ToList();
            return View(data);
        }



    }
}