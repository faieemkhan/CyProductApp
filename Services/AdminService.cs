using MVCCoreApplication.Models;
using MVCCoreApplication.Repository;
using MVCCoreApplication.UserException;
using System.Collections.Generic;

namespace MVCCoreApplication.Services
{
    public class AdminService : IAdminService
    {
        readonly IAdminRepository _adminRepository;


        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public int AddProduct(Product product)
        {
            Product ProductDetailsExists = _adminRepository.GetAllProductByName(product.ProductName);
            if (ProductDetailsExists == null)
            {
                int productResult = _adminRepository.AddProduct(product);
                if (productResult == 1)
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
                return 0;
               // throw new UserAlreadyExixtsException($"Duplication ::{product.ProductName} Exists !!");
                // add exception massage code

            }
        }

        public List<Admin> AllAdmins()
        {
            return _adminRepository.AllAdmins();
        }

        public int DeleteProduct(int id)
        {
            Product productDetailsExistsToDelete = _adminRepository.GetProductById(id);
            int productDeleteResult = _adminRepository.DeleteProduct(productDetailsExistsToDelete);
            if (productDeleteResult == 1)
            {
                return 1;
            }
            else
            {
                throw new UserNotExistsException($"User with ::{id} Not Found!! ");
            }
        }

        public void EditProduct(Product product)
        {
            _adminRepository.EditProduct(product);
        }

        public List<Product> GetAllProduct()
        {
            return _adminRepository.GetAllProduct();
        }

        public Product GetProductById(int id)
        {
            return _adminRepository.GetProductById(id);
        }

        public int RegisterAdmin(Admin admin)
        {
            Admin adminDetailsExists = _adminRepository.GetAllUserByName(admin.UserName);
            if (adminDetailsExists == null)
            {
                int userAddResult = _adminRepository.RegisterAdmin(admin);
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
                throw new UserAlreadyExixtsException($"Duplication ::{admin.UserName} Exists !!");
                // add exception massage code

            }
        }

        public Admin VerifyingLogin(AdminLoginCredentials adminLoginCredentials)
        {
            Admin adminLoginExists = _adminRepository.VerifyingLogin(adminLoginCredentials);
            if (adminLoginExists != null)
            {
                return adminLoginExists;

            }
            else
            {
                throw new UserCredentialInvalidException($"{adminLoginCredentials}");
            }
        }
    }
}
