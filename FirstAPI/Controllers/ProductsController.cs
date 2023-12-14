using FirstAPI.Data;
using FirstAPI.Interfaces;
using FirstAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly MyDbContext _context;
        public readonly IProductRepository _productRepository;
        public ProductsController(MyDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productsDomain = await _productRepository.GetById(id);

            if (productsDomain != null)
            {
                var productDTO = new ProductDTO()
                {
                    Id = productsDomain.Id,
                    Name = productsDomain.Name,
                    Description = productsDomain.Description,
                    Price = productsDomain.Price,
                    StockQuantity = productsDomain.StockQuantity
                };
                return Ok(productDTO);
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Create([FromBody] AddProductRequestDto products)
        {
            var product = _productRepository.CreateProduct(products);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpDelete]
        [Route("{id:Guid}")] 
        public async Task<IActionResult> Delete (Guid id)
        {
            var products = await _productRepository.DeleteProduct(id);
            
            if(!products)
                return BadRequest();
            return NoContent();
        }
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] AddProductRequestDto addProductRequest, Guid productId) 
        {
            var product = _productRepository.UpdateProduct(productId, addProductRequest);
            if(product != null)
                return CreatedAtAction(nameof(GetById), new { id = product.Id}, product);
            return BadRequest();

        }


       



    }
}
