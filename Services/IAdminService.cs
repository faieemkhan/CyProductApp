using MVCCoreApplication.Models;
using System.Collections.Generic;

namespace MVCCoreApplication.Services
{
    public interface IAdminService
    {
        int AddProduct(Product product);
        List<Admin> AllAdmins();
       int DeleteProduct(int id);
        void EditProduct(Product product);
List<Product> GetAllProduct();
        Product GetProductById(int id);
        int RegisterAdmin(Admin admin);
        Admin VerifyingLogin(AdminLoginCredentials adminLoginCredentials);
    }
}
