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

        // Lấy tất cả sản phẩm
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        // Lấy sản phẩm theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return product;
        }

        // Thêm sản phẩm
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            //_context.Products.Add(product);
            //await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

            //Tìm Category trước khi lưu để tránh EF tự tạo mới
            var category = await _context.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                return BadRequest("CategoryId không tồn tại");
            }

            product.Category = null; // Bỏ object Category để tránh lỗi tự tạo mới
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPost("muti")]
        public async Task<ActionResult> CreateMultipleProducts([FromBody] List<Product> products)
        {
            if (products == null || !products.Any())
            {
                return BadRequest("Danh sách sản phẩm không được rỗng");
            }

            foreach (var product in products)
            {
                var category = await _context.Categories.FindAsync(product.CategoryId);
                if (category == null)
                {
                    return BadRequest($"CategoryId {product.CategoryId} không tồn tại");
                }
                product.Category = null; // Tránh EF Core tạo Category mới
            }

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"{products.Count} sản phẩm đã được thêm thành công!" });
        }


        // Cập nhật sản phẩm
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id) return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Xóa sản phẩm
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
