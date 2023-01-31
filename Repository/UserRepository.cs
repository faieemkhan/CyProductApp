using MVCCoreApplication.Context;
using MVCCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCCoreApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        // List<User> _users;
        readonly UserDbContext _context;

        public UserRepository( UserDbContext context)
        {
            _context = context;
           // _users = new List<User>();
        }

        public int RegisterUser(User user)
        {
            if (_context.Users.Count() == 0)
            {
                user.Id = 1;
            }
            else
            {
               //update id by 1
                int userIdToUpdate = _context.Users.Max(u => u.Id)+1;
                user.Id = userIdToUpdate;
            }


            _context.Users.Add(user);
              return _context.SaveChanges();
            
            
        }

        public void EditUser(User user)
        {
            _context.Users.Update(user);
        }
       

        public List<User> GetAllUser()
        {

            return _context.Users.ToList();
        }
           
        public List<User> GetAllUserLogin()
        {
            return _context.Users.ToList();
        }

        public User GetAllUserByName(string name)
        {
            return _context.Users.Where(u => u.Name == name).FirstOrDefault();
        }

        

        public User GetUserById(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

       

        public int DeleteUser(User userDetailsExistsToDelete)
        {
           _context.Users.Remove(userDetailsExistsToDelete);
            return _context.SaveChanges();
        }

        //public int DeleteUser(int id)
        //{
        //  _context.Users.Where(u => u.Id == id).FirstOrDefault();
        //    _context.Remove(id);
        //    return _context.SaveChanges();

        //}

        public User VerifyLogin(UserLoginCredential userLoginCredentials)
        {
            return _context.Users.Where(u =>u.Name == userLoginCredentials.Name && u.Password == userLoginCredentials.Password).FirstOrDefault();
        }

        public List<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }

        

        
        public CartItems GetCartItemsById(int id)
        {
            return _context.CartItems.Where(c => c.Id == id).FirstOrDefault();
        }

        public int RemoveCart(CartItems cartDetailsExistsToDelete)
        {
            _context.CartItems.Remove(cartDetailsExistsToDelete);
            return _context.SaveChanges();
        }

        
        public void AddToCart(Product product, int v)
        {
            CartItems cartItems = new CartItems()
            {
                ProductId=product.Id,
                ProductName=product.ProductName,
                ProductDescription=product.ProductDescription,
                ImageFile=product.ImageFile,
                NumberOfProduct=product.NumberOfProduct,
                ProductPrice=product.ProductPrice,
                category=product.category,
                UserId= v
               


            };
            _context.CartItems.Add(cartItems);
            _context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<CartItems> GetAllProductByUserId(int id)
        {
            return _context.CartItems.Where(p => p.UserId == id && p.IsOrdered == false).ToList();
        }

        public Product GetDetailsById(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public UserAddress AddAddress(UserAddress userAddress, int id)
        {
            UserAddress userAddress1 = null;
            try
            {
                userAddress.UserId = id;
                _context.UserAddresses.Add(userAddress);
                _context.SaveChanges();
                return userAddress;
            }
            catch (Exception ex)
            {
                return userAddress1;
            }
            
            
        }

        public UserAddress GetAddressByName(string fullName)
        {
            return _context.UserAddresses.Where(c => c.FullName == fullName).FirstOrDefault();
        }

        public CartItems GetOrderById(int id)
        {
            return _context.CartItems.Where(c => c.Id == id).FirstOrDefault();
        }

        public int CheckOut(CartItems cartDetailsExistsCheckout)
        {
            _context.CartItems.Remove(cartDetailsExistsCheckout);
            return _context.SaveChanges();
        }

        public bool PlaceOrder(int v, int id)
        {
            try
            {
                List<CartItems> cartItems = _context.CartItems.Where(p => p.UserId == id && p.IsOrdered == false).ToList();
                foreach (CartItems cartItems1 in cartItems)
                {
                    OrderTable orderTable = new OrderTable()
                    {
                        ProductId = cartItems1.ProductId,
                        ProductName = cartItems1.ProductName,
                        ProductDescription = cartItems1.ProductDescription,
                        ImageFile = cartItems1.ImageFile,
                        category = cartItems1.category,
                        NumberOfProduct = cartItems1.Quanitity,
                        ProductPrice = cartItems1.ProductPrice,
                        UserId = id,
                        AddressId = v
                    };
                    cartItems1.IsOrdered = true;
                    Product product = _context.Products.Where(p => p.Id == cartItems1.ProductId).FirstOrDefault();
                    product.NumberOfProduct -= cartItems1.Quanitity;
                    _context.Products.Update(product);
                    _context.CartItems.Update(cartItems1);
                    _context.Orders.Add(orderTable);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            

        }
    }
}

