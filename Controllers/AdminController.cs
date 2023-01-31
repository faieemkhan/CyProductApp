using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCCoreApplication.Models;
using MVCCoreApplication.Services;
using System.Collections.Generic;

namespace MVCCoreApplication.Controllers
{
    public class AdminController : Controller
    {
        readonly IAdminService _adminService;
        readonly ITokenGeneratorService _tokenGeneratorService;
        readonly IConfiguration _configuration;

        public AdminController(IAdminService adminService, ITokenGeneratorService tokenGeneratorService, IConfiguration configuration)
        {
            _adminService = adminService;
            _tokenGeneratorService = tokenGeneratorService;
            _configuration = configuration;
        }
        public IActionResult AllAdmins()
        {

            List<Admin> _admins = _adminService.AllAdmins();
            return View(_admins);
        }
        [HttpGet]
        public ActionResult RegisterAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                int addAdminStatus = _adminService.RegisterAdmin(admin);
                if (addAdminStatus == 1)
                {
                    TempData["AdminMessage"] = "Admin Registered Successfully !!";
                    return RedirectToAction("AllAdmins");
                }
                TempData["AdminMessage"] = "name Exists, choose some other name!!";
                return View(admin);


            }
            else
            {
                return View(admin);
            }
        }

        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(AdminLoginCredentials adminLoginCredentials)
        {
            Admin adminLoginStatus = _adminService.VerifyingLogin(adminLoginCredentials);
            string adminToken = _tokenGeneratorService.GenerateToken(adminLoginStatus.Id, adminLoginStatus.UserName);
            HttpContext.Session.SetString("userToken", adminToken);
            //generate a Token
            //Add Token Serviceing
            return RedirectToAction("AdminDashboard");
        }
        public ActionResult AllProduct()
        {
            List<Product> product = _adminService.GetAllProduct();
            return View(product);
        }
        public ActionResult AdminDashboard()
        {
            string adminToken = HttpContext.Session.GetString("userToken");
            if (adminToken == null)
            {


                return RedirectToAction("AdminLogin");
            }
            else
            {
                
                List<Product> product = _adminService.GetAllProduct();
                return View(product);
            }
           

        }
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                int addProductStatus = _adminService.AddProduct(product);
                if (addProductStatus == 1)
                {
                    TempData["ProductMessage"] = "Product Added Successfully !!";
                    return RedirectToAction("AdminDashboard");
                }
                TempData["ProductMessage"] = "please Added Again!!";
                return View(product);


            }
            else
            {
                return View(product);
            }
        }
        public ActionResult EditProduct(int id)
        {
            Product productEditById = _adminService.GetProductById(id);
            return View(productEditById);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            _adminService.EditProduct(product);

            return RedirectToAction("AdminDashboard");
        }
        public ActionResult ProductDetails(int id)
        {
            Product productDetailsById = _adminService.GetProductById(id);
            return View(productDetailsById);
        }
        public ActionResult DeleteProduct (Product product)
        {
            _adminService.DeleteProduct(product.Id);
            return RedirectToAction("AdminDashboard");
        }

    }
}
