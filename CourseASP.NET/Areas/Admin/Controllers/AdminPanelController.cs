using CourseASP.NET.Areas.Admin.Models;
using CourseASP.NET.Entities;
using CourseASP.NET.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CourseASP.NET.Areas.Admin.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ApplicationUserManager userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminPanelController()
        {
            context = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        // GET: Admin/AdminPanel
        public ActionResult Index()
        {
            var adminModel = GetAdminViewModel();

            return View(adminModel);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string id, string role)
        {
            var user = context.Users
                              .FirstOrDefault(x => x.Id.Equals(id));

            var currentRoleId = context.Set<IdentityUserRole>().FirstOrDefault(x => x.UserId.Equals(id)).RoleId;

            var currentRole = roleManager.FindById(currentRoleId).Name;

            var removeResult = await userManager.RemoveFromRoleAsync(user.Id, currentRole);

            var addResult = await userManager.AddToRoleAsync(user.Id, role);

            var adminModel = GetAdminViewModel();

            return View(adminModel);
        }
        private AdminViewModel GetAdminViewModel()
        {
            var roles = GetRoles();

            var usersModels = GetUserViewModels();

            var adminModel = new AdminViewModel
            {
                Users = usersModels,
                Roles = roles
            };

            return adminModel;
        }
        private IEnumerable<string> GetRoles()
        {
            var roles = context.Set<IdentityRole>()
                               .Select(x => x.Name)
                               .ToArray();

            return roles;
        }
        private IEnumerable<UserViewModel> GetUserViewModels()
        {
            var usersModels = context.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                Email = x.Email,
                RoleId = context.Set<IdentityUserRole>().FirstOrDefault(y => y.UserId.Equals(x.Id)).RoleId,
            }).Where(y => y.RoleId != null).ToList();

            foreach (var user in usersModels)
            {
                user.Role = roleManager.FindById(user.RoleId).Name;
            }

            return usersModels;
        }

        [HttpPost]
        public ActionResult CreateRole(CreateRoleViewModel model)
        {
            var role = new IdentityRole(model.Name);

            var result = roleManager.Create(role);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var category = new Category { Name = model.Name };

            context.Categories.Add(category);

            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult Categories()
        {
            var categories = context.Categories;
            List<CategoriesViewModel> model = new List<CategoriesViewModel>();
            foreach (var item in categories)
            {
                model.Add(new CategoriesViewModel {Name = item.Name,Id=item.Id });
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteCategories(int id)
        {
            var c = context.Categories.FirstOrDefault(move => move.Id == id);
            CategoriesViewModel category = new CategoriesViewModel
            {
                Name = c.Name,
                Id = c.Id
            };
            
            return View(category);


        }
        [HttpPost]
        public ActionResult DeleteCategories(CategoriesViewModel model)
        {
            
            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
                Category category = context.Categories.FirstOrDefault(x => x.Id == model.Id);
                context.Categories.Remove(category);
                context.SaveChanges();
                return RedirectToAction("Categories");
            }
        }

    }
}
