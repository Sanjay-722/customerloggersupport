using customerloggersupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace customerloggersupport.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserInfo objUser)
        {
            if (ModelState.IsValid)
            {
                using (CustomerDBEntities1 db = new CustomerDBEntities1())
                {
                    var obj = db.UserInfoes.Where(a => a.UserId.Equals(objUser.UserId) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserId"] = obj.UserId.ToString();
                        return RedirectToAction("AddCustPage");
                    }
                }
            }
            return View(objUser);
        }
        public ActionResult AddCustPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustPage(CustLogInfo obj)

        {
            if (ModelState.IsValid)
            {
                CustomerDBEntities1 db = new CustomerDBEntities1();
                db.CustLogInfoes.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(obj);
        }
    }
}