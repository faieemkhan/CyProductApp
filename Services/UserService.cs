using MVCCoreApplication.UserException;
using MVCCoreApplication.Models;
using MVCCoreApplication.Repository;
using System.Collections.Generic;
using System;

namespace MVCCoreApplication.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
      

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
       

        public void EditUser(User user)
        {
            _userRepository.EditUser(user);
        }
        

        public List<User> GetAllUser()
        {
            return _userRepository.GetAllUser();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public bool DeleteUser(int id)
        {
           User userDetailsExistsToDelete= _userRepository.GetUserById(id);
            int userDeleteResult = _userRepository.DeleteUser(userDetailsExistsToDelete);
            if (userDeleteResult == 1)
            {
                return true;
            }
            else
            {
                throw new UserNotExistsException($"User with ::{id} Not Found!! ");
            }
        }



      

        public int RegisterUser(User user)
        {
            User userDetailsExists = _userRepository.GetAllUserByName(user.Name);
            if (userDetailsExists == null)
            {
                int userAddResult = _userRepository.RegisterUser(user);
                if (userAddResult == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                throw new UserAlreadyExixtsException($"Duplication ::{user.Name} Exists !!");
                // add exception massage code
                
            }
        }

        public User VerifyLogin(UserLoginCredential userLoginCredentials)
        {
           User userLoginExists =_userRepository.VerifyLogin(userLoginCredentials);
            if(userLoginExists != null) 
            {
                return userLoginExists;

            }
            else
            {
                throw new UserCredentialInvalidException($"{userLoginCredentials}");
            }
                  
        }

        public List<User> GetAllUserLogin()
        {
            return _userRepository.GetAllUserLogin();
        }

        public List<Product> GetAllProduct()
        {
            return _userRepository.GetAllProduct();
        }

        

       

        public bool RemoveCart(int id)
        {
            CartItems cartDetailsExistsToDelete = _userRepository.GetCartItemsById(id);
            int cartDeleteResult = _userRepository.RemoveCart(cartDetailsExistsToDelete);
            if (cartDeleteResult == 1)
            {
                return true;
            }
            else
            {
                return false;
                //throw new UserNotExistsException($"User with ::{id} Not Found!! ");
            }
        
    }

        

        public Product GetProductById(int id)
        {
            return _userRepository.GetProductById(id);
        }

        public bool AddToCart(Product product, int v)
        {
            _userRepository.AddToCart(product, v);
            return true;
        }

        public List<CartItems> GetAllProductByUserId(int id)
        {
            List<CartItems> cartItems = _userRepository.GetAllProductByUserId(id);
            return cartItems;
        }

        public Product GetDetailsById(int id)
        {
            return _userRepository.GetDetailsById(id);
        }

        public UserAddress AddAddress(UserAddress userAddress, int id)
        {

            //int UserAddressExists = _userRepository.GetAddressByName(userAddress.FullName);
            // if (UserAddressExists == 1)

            UserAddress addressResult = _userRepository.AddAddress(userAddress, id);
            return addressResult;
        }

        public bool CheckOut(int id)
        {
            CartItems cartDetailsExistsCheckout = _userRepository.GetCartItemsById(id);
            int cartResult = _userRepository.CheckOut(cartDetailsExistsCheckout);
            if (cartResult == 1)
            {
                return true;
            }
            else
            {
                return false;
                //throw new UserNotExistsException($"User with ::{id} Not Found!! ");
            }
        }

        public bool PlaceOrder(int v, int id)
        {
            bool isPlaceOrder = _userRepository.PlaceOrder(v,id);
            if(isPlaceOrder)
            {
                return true;
            }
            return false;
        }
    }
}
