using MVCCoreApplication.Context;
using MVCCoreApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MVCCoreApplication.Repository
{
    public class AdminRepository : IAdminRepository
    {
        readonly UserDbContext _context;
        public AdminRepository(UserDbContext context)
        {
            _context = context;
           
        }

        public int AddProduct(Product product)
        {
            


            _context.Products.Add(product);
            return _context.SaveChanges();
        }

        public List<Admin> AllAdmins()
        {
            return _context.Admins.ToList();
        }

        public int DeleteProduct(Product productDetailsExistsToDelete)
        {
            _context.Products.Remove(productDetailsExistsToDelete);
            return _context.SaveChanges();
        }

        public void EditProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public List<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }

        public Product GetAllProductByName(string productName)
        {
            return _context.Products.Where(p => p.ProductName == productName).FirstOrDefault();
        }

        public Admin GetAllUserByName(string userName)
        {
            return _context.Admins.Where(a => a.UserName == userName).FirstOrDefault();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public int RegisterAdmin(Admin admin)
        {
            

            _context.Admins.Add(admin);
            return _context.SaveChanges();
        }

        public Admin VerifyingLogin(AdminLoginCredentials adminLoginCredentials)
        {
            return _context.Admins.Where(a => a.Email == adminLoginCredentials.Email && a.Password == adminLoginCredentials.Password).FirstOrDefault();
        }
    }
}
