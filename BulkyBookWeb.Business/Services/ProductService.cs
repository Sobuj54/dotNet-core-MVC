using BulkyBook.Business.Services.IServices;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly DBcontext _context;

        public ProductService(DBcontext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product {id} was not found.");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool includeCategory=false)
        {
            if (includeCategory)
            {
                return await _context.Products.Include(u => u.Category).ToListAsync();
            }
            else
            {
               return await _context.Products.ToListAsync();
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
