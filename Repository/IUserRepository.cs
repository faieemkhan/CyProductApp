using MVCCoreApplication.Models;
using System.Collections.Generic;

namespace MVCCoreApplication.Repository
{
    public interface IUserRepository
    {
   int RegisterUser(User user);
       void EditUser(User user);


        List<User> GetAllUser();
        User GetAllUserByName(string name);
        User GetUserById(int id);
        //int DeleteUser(int id);
        int DeleteUser(User userDetailsExistsToDelete);
       User VerifyLogin(UserLoginCredential userLoginCredentials);
        List<User> GetAllUserLogin();
        List<Product> GetAllProduct();

       
        CartItems GetCartItemsById(int id);
        int RemoveCart(CartItems cartDetailsExistsToDelete);
        void AddToCart(Product product, int v);
        Product GetProductById(int id);
        List<CartItems> GetAllProductByUserId(int id);
        Product GetDetailsById(int id);
        UserAddress AddAddress(UserAddress userAddress, int id);
        UserAddress GetAddressByName(string fullName);
        int CheckOut(CartItems cartDetailsExistsCheckout);
        bool PlaceOrder(int v, int id);
    }
}
