using CourseASP.NET.Areas.Saller.Models;
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

namespace CourseASP.NET.Areas.Saller.Controllers
{
    public class SallerController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ApplicationUserManager userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationUser user;


        public SallerController()
        {
            context = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
              var a = User.Identity.GetUserId();
               user = userManager.FindById(a);
        }

        // GET: Saller/Saller
        public ActionResult Index()
        {
            var sallerModel = GetSallerViewModel();

            return View(sallerModel);
        }

        private async Task<SallerViewModel> GetSallerViewModel()
        {

            var products = await Task.Run(() => GetProducts());

            var orders = GetOrders();

            var sallerModel = new SallerViewModel
            {
                Products = products,
                Orders = orders
            };

            return sallerModel;
        }

        private IEnumerable<OrderViewModel> GetOrders()
        {
            var orders = context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                Price = x.Price,
                SaleDate = x.SaleDate,
                Products = context.Products.Where(u => u.SallerShop.Id == user.Id).ToList()

            });
            return orders;
        }

        private IEnumerable<ProductViewModel> GetProducts()
        {
            var prod = from item in context.Products
                       where item.SallerShop.Id == user.Id
                       select new
                       {
                           item.Id,
                           item.ID_Category,
                           item.Image,
                           item.Name,
                           item.Orders,
                           item.Price,
                           item.SallerShop,
                           item.Description

                       };

            var products = prod.Select(item => new ProductViewModel
            {
                Price = item.Price,
                Name = item.Name,
                Description = item.Description,
                Id = item.Id,
                ID_Category = item.ID_Category,
                Image = item.Image,

            }).ToList();
            if (products != null)
            {

                foreach (var item in products)
                {
                    item.Category = context.Categories.FirstOrDefault(x => x.Id == item.ID_Category).Name;
                }
            }


            return products;
        }

        [HttpPost]
        public async Task<ActionResult> Index(string id, string role)
        {
            var adminModel = await Task.Run(() => GetSallerViewModel());
            //   var adminModel = GetSallerViewModel();

            return View(adminModel);
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var product = new Product
            {

                Name = model.Name,
                Image = model.Image,
                Description = model.Description,
                Price = model.Price
            };
            product.SallerShop = user;


            context.Products.Add(product);

            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}