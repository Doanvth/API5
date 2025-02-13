using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API5.Data;
using API5.Models;

namespace API5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //API GET - Lấy tất cả sản phẩm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null || products.Count == 0)
            {
                return NotFound("Không có sản phẩm nào.");
            }
            return Ok(products);
        }

        //API GET - Lấy sản phẩm theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Không tìm thấy sản phẩm với ID = {id}");
            }
            return Ok(product);
        }

        //API POST - Thêm sản phẩm
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name) || product.Price <= 0)
            {
                return BadRequest("Tên sản phẩm và giá không được để trống hoặc nhỏ hơn 0.");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        //API PUT - Cập nhật sản phẩm
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID không khớp.");
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound($"Không tìm thấy sản phẩm với ID = {id}");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        //API DELETE - Xóa sản phẩm theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Không tìm thấy sản phẩm với ID = {id}");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
