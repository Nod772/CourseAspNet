using CourseASP.NET.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseASP.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ApplicationUserManager userManager;
        public HomeController()
        {
            context = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            if (userId != null)
            {
            
                var roleId = context.Set<IdentityUserRole>()
                              .FirstOrDefault(x => x.UserId==(userId)).RoleId;


                var role = context.Roles.FirstOrDefault(x => x.Id.Equals(roleId));

                if (role.Name.Equals("Admin"))
                {
                    RedirectToAction("Index", "AdminPanel", new { area = "Admin" });
                }
                else if (role.Name.Equals("Manager"))
                {
                    RedirectToAction("Index", "ManagerPanel", new { area = "Saller" });
                }
            }
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