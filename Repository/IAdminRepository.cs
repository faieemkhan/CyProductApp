using MVCCoreApplication.Models;
using System.Collections.Generic;

namespace MVCCoreApplication.Repository
{
    public interface IAdminRepository
    {
        int AddProduct(Product product);
        List<Admin> AllAdmins();
        int DeleteProduct(Product productDetailsExistsToDelete);
        void EditProduct(Product product);
        List<Product> GetAllProduct();
        Product GetAllProductByName(string productName);
        Admin GetAllUserByName(string userName);
        Product GetProductById(int id);
        int RegisterAdmin(Admin admin);
        Admin VerifyingLogin(AdminLoginCredentials adminLoginCredentials);
    }
}
