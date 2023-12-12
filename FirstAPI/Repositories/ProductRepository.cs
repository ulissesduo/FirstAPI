using FirstAPI.Data;
using FirstAPI.Interfaces;
using FirstAPI.Models.Domain;
using FirstAPI.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly MyDbContext _context;
        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public ProductDTO CreateProduct(AddProductRequestDto addProductRequest)
        {
            var product = new Products 
            {
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                Price = addProductRequest.Price,
                StockQuantity = addProductRequest.StockQuantity,

            };

            _context.Products.Add(product);
            _context.SaveChanges();


            var productDto = new ProductDTO 
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
            };
            return productDto;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (products == null)
                return false;

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Products>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Products> GetById(Guid id)
        {
            var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return products;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public ProductDTO UpdateProduct(Guid productId, AddProductRequestDto addProductRequest)
        {
            //get the record by id using first or default method
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            //create something similar to create method. Use the class itself modified with the data of parameter
            //check if the product is different of null
            if (product != null)
            {
                //update
                product.Name = addProductRequest.Name;
                product.Description = addProductRequest.Description;
                product.Price = addProductRequest.Price;
                product.StockQuantity = addProductRequest.StockQuantity;
                _context.SaveChanges();

                //update dto
                var productDTO = new ProductDTO
                {
                    Id = product.Id,
                    Description = product.Description,
                    Name = product.Name,
                    StockQuantity = product.StockQuantity
                };

                return productDTO;

            }
            else 
            {
                //badrequest
                return null;
            }
          
        }

        public ProductDTO UpdateSpecificAttProduct(Guid productId, AddProductRequestDto addProductRequest)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null) 
            {
                //update
                product.Name = addProductRequest.Name;
                product.Description = addProductRequest.Description;
                product.Price = addProductRequest.Price;
                product.StockQuantity = addProductRequest.StockQuantity;
                _context.SaveChanges();

                var productDTO = new ProductDTO
                {
                    Id = product.Id,
                    Description = product.Description,
                    Name = product.Name,
                    StockQuantity = product.StockQuantity
                };

                return productDTO;

            }
            else 
            {
                //badrequest
                return null;
            }
        }
    }
}
