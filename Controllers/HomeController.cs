using ECommerce.Controllers.Base;
using ECommerce.Core.Model;
using ECommerce.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : AndControllerBase
    {
        AndDB db = new AndDB();
       
        // GET: Home
        public ActionResult Index()
        { 

            var data = db.Products.OrderByDescending(x => x.CreateDate).Take(5).ToList();
            return View(data);
        }
        public PartialViewResult GetMenu()
        {
            var db = new AndDB();
            var menus = db.Categories.Where(x => x.ParentID == 0).ToList();
            return PartialView(menus);
        }
        [Route("Uye-Giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Uye-Giris")]
        public ActionResult Login(string EMail, string Password)
        {
            var db = new AndDB();
            var users = db.Users.Where(x => x.EMail == EMail && x.Password == Password && x.IsActive==true && x.IsAdmin==false).ToList();
            if (users.Count == 1)
            {
                Session["LoginUser"] = users.FirstOrDefault();
                return Redirect("/");
            }
            else
            {
                ViewBag.Error = "Hatalı kullanıcı veya şifre";
                return View();
            }
            
        }

        [Route("Uye-Kayit")]
        public ActionResult CreateUser() {
            return View();
        }

        [HttpPost]
        [Route("Uye-Kayit")]
        public ActionResult CreateUser(User user) {
            try { 
            user.CreateDate = DateTime.Now;
            user.CreateUserID = 1;
            user.IsActive = true;
            user.IsAdmin = false;
            var db = new AndDB();
            db.Users.Add(user);
            db.SaveChanges();
                return Redirect("/");
            }
            catch (Exception ex) {
                return View();
            }
        }
    }
}