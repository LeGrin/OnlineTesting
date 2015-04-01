using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMPR_testing_Lib.Domain;
using SMPR_testing_Lib.Repository;
using WebMatrix.WebData;
using System.Web.Security;

namespace SMPR_testing.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        ISmprRepository _repository;

        public AdminController(ISmprRepository repo)
        {
            _repository = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditLecturers()
        {
            IQueryable<User> lecturers = _repository.Users.Where(x => x.GroupId == _repository.Groups.Where(m => m.Name == "Преподаватели").FirstOrDefault().Id);
            return View(lecturers);
        }

        [HttpPost]
        public RedirectToRouteResult AddLecturer(string lecturerLogin, string lecturerName)
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var roles = (SimpleRoleProvider)Roles.Provider;
            if (membership.GetUser(lecturerLogin, false) == null)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("GroupId", _repository.Groups.FirstOrDefault(x => x.Name == "Преподаватели").Id);
                values.Add("Name", lecturerName);

                membership.CreateUserAndAccount(lecturerLogin, lecturerLogin, values);
                roles.AddUsersToRoles(new string[] { lecturerLogin }, new string[] { "lecturer" });

            }
            return RedirectToAction("EditLecturers");
        }

    }
}
