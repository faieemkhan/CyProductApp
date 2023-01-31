using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCCoreApplication.Aspect_Oriented_Programming;
using MVCCoreApplication.Logging;
using MVCCoreApplication.Models;
using MVCCoreApplication.Services;
using System;
using System.Collections.Generic;

namespace MVCCoreApplication.Controllers
{
    [ServiceFilter(typeof(UserLogger))]
    [ExceptionHandler]
    public class UserController : Controller
    {
      
        readonly IUserService _userService;
      
        readonly ITokenGeneratorService _tokenGeneratorService;
        readonly IConfiguration _configuration;



        // UserRepository UserRepository = new UserRepository();
        // Consturctor 
        public UserController(IUserService userService ,ITokenGeneratorService tokenGeneratorService,IConfiguration configuration)
        {
           
            _userService = userService;
            _tokenGeneratorService = tokenGeneratorService;
            _configuration = configuration;
        }
        //Action-Index
        public IActionResult GetAllUsers()
        {
            //List<User> _users = _userService.GetAllUser();
            //return View(_users);
            List<Product> product = _userService.GetAllProduct();
            return View(product);
        }
        public ActionResult RemoveCart(CartItems cartItems)
        {
            _userService.RemoveCart(cartItems.Id);
            return RedirectToAction("ShowAllCart");
        }
        public ActionResult AddToCart(int id)
        {
            string userToken =HttpContext.Session.GetString("userToken");
            if (userToken == null)
            {

                TempData["CartMessage"] = "Login Please !!";
                return RedirectToAction("Login");
            }

            else
            { 

                string userId = HttpContext.Session.GetString("UserId");
                Product product = _userService.GetProductById(id);
               
                bool addItems = _userService.AddToCart(product, Convert.ToInt32(userId));
                if (addItems)
                {
                    TempData["itemAddedMessage"] = "Item Added Successfully !!";
                    return RedirectToAction("ShowAllCart");
                   
                }
                else
                {
                    TempData["itemNotAddedMessage"] = "Item Not Added !!";
                    return RedirectToAction("ShowAllCart");
                }
               
            }

            

        }
        public ActionResult DetailsOfProduct(int id)
        {
            Product productDetailsById = _userService.GetDetailsById(id);
            return View(productDetailsById);
        }
        
        public ActionResult ShowAllCart(int id)
        {
            string userToken = HttpContext.Session.GetString("userToken");
            if (userToken == null)
            {

                
                return RedirectToAction("Login");
            }
            else
            {
                string userId = HttpContext.Session.GetString("UserId");
                List<CartItems> cartItems = _userService.GetAllProductByUserId(Convert.ToInt32(userId));
                return View(cartItems);
            }
            
        }
         public ActionResult AddAddress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAddress(UserAddress userAddress)
        {
            if (ModelState.IsValid)
            {
                string userId = HttpContext.Session.GetString("UserId");
                UserAddress userAddress1 = _userService.AddAddress(userAddress, Convert.ToInt32(userId));
                if (userAddress1 != null)
                {
                    TempData["UserAddressMessage"] = "User Address Added Successfully !!";
                    bool isPlaceOrder = _userService.PlaceOrder( userAddress1.Id, Convert.ToInt32(userId));
                    if (isPlaceOrder)
                    {
                        return RedirectToAction("Orders");
                    }
                    return RedirectToAction("ShowAllCart");
                }
                TempData["UserAddressNotMessage"] = "try againe!!";
                return View(userAddress);


            }
            else
            {
                return View(userAddress);
            }
        }
        public  ActionResult CheckOut(CartItems cartItems)
        {
            _userService.CheckOut(cartItems.Id);
            return RedirectToAction("AddAddress");
        }
       
        public ActionResult Orders()
        {
            return View();
        }
        

        public ActionResult RegisterUser()
        {
            return View();
        }


        [HttpPost]

        public ActionResult RegisterUser(User user)
        {
            if(ModelState.IsValid)
            {
               int addUserStatus = _userService.RegisterUser(user);
                if (addUserStatus == 1)
                {
                    TempData["UserMessage"] = "User Registered Successfully !!";
                    return RedirectToAction("GetAllUsers");
                }
                TempData["UserMessage"] = "User Exists, choose some other name!!";
                return View(User);


            }
            else
            {
                return View(User);
            }
           



        }
            public ActionResult Login()
            {
                return View();
            }
            [HttpPost]
        public ActionResult Login(UserLoginCredential userLoginCredentials)
        {
            
               User userLoginStatus = _userService.VerifyLogin(userLoginCredentials);
             string userToken = _tokenGeneratorService.GenerateToken(userLoginStatus.Id, userLoginStatus.Name);
            HttpContext.Session.SetString("userToken",userToken);
            HttpContext.Session.SetString("UserId",Convert.ToString(userLoginStatus.Id));
            //generate a Token
            //Add Token Service
            return RedirectToAction("GetAllUsers");

        }
        //[Authorize]
            public ActionResult Dashboard()
        {
            string userToken = HttpContext.Session.GetString("userToken");
            if(userToken == null)
            {
                return RedirectToAction("Login");
            }
            var userSecretKey = _configuration["JwtValidationDetails:UserApplicationSecretKey"];
            var userIssuer = _configuration["JwtValidationDetails:UserIssuer"];
            var userAudience = _configuration["JwtValidationDetails:UserAudience"];
           bool isTokenValid =  _tokenGeneratorService.IsTokenValid(userSecretKey,userIssuer,userAudience,userToken);
            if (!isTokenValid)
            {
                return RedirectToAction("Login");
            }
            List<User> _users = _userService.GetAllUserLogin();
            return View(_users);
           
        }

        public ActionResult UserDetails(int id)
        {
            User userDetailsById = _userService.GetUserById(id);
            return View(userDetailsById);
        }
       
     public ActionResult DeleteUser(User user)
        {
            
                _userService.DeleteUser(user.Id);
            return RedirectToAction("GetAllUsers");
        }
        public ActionResult EditUser(int id)
        {
            User userEditById = _userService.GetUserById(id);
            return View(userEditById);
        }
        [HttpPost]

        public ActionResult EditUser(User user)
        {
            
            _userService.EditUser(user);

            return RedirectToAction("GetAllUsers");
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
       



    }
}
