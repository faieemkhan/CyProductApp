using MVCCoreApplication.Models;
using System.Collections.Generic;

namespace MVCCoreApplication.Services
{
    public interface IUserService
    {

        void EditUser(User user);
        List<User> GetAllUser();
        User GetUserById(int id);
        bool DeleteUser(int id);
       int RegisterUser(User user);
        User VerifyLogin(UserLoginCredential userLoginCredentials);
        List<User> GetAllUserLogin();
        List<Product> GetAllProduct();
        
      
       bool RemoveCart(int id);
        Product GetProductById(int id);
        bool AddToCart(Product product, int v);
        List<CartItems> GetAllProductByUserId(int id);
        Product GetDetailsById(int id);
       UserAddress AddAddress(UserAddress userAddress, int id);
        bool CheckOut(int id);
        bool PlaceOrder(int v, int id);
    }
}
