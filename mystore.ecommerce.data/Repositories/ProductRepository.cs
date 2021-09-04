using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext;
using mystore.ecommerce.dbcontext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommercedbContext _context;

        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(EcommercedbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _context.Product.Include(p=>p.CategoryNavigation).ToList();               
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string categpry)
        {
            try
            {
                return _context.Product.Where(p=>p.CategoryNavigation.CategoryName == categpry).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            try
            {
                return _context.ProductCategory.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void AddEntity(object entity)
        {
            _context.Add(entity);
        }
    }
}
