using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMPR_testing.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {

            if (TempData["LoginError"] != null) {
                ModelState.AddModelError("", TempData["LoginError"].ToString());
            }

            ViewBag.IsUserAuthenticated = User.Identity.IsAuthenticated;

            return View("NewIndex");
        }

        [Authorize(Roles = "lecturer")]
        public ActionResult About() {
            return View();
        }

        public ActionResult Contact() {
            return View();
        }
    }
}
