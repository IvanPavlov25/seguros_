using Microsoft.AspNetCore.Mvc;
using model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosRepository _productosRepository;

        public ProductosController(IProductosRepository productosRepository)
        {
            _productosRepository = productosRepository ?? throw new ArgumentNullException(nameof(productosRepository));
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productosRepository.GetAllProductosAsync();
            return Ok(productos);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _productosRepository.GetProductoByIdAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<IActionResult> CreateProducto([FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest();
            }

            await _productosRepository.InsertProductoAsync(producto);
            return CreatedAtRoute("GetProducto", new { id = producto.idProducto }, producto);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] Producto producto)
        {
            if (producto == null || producto.idProducto != id)
            {
                return BadRequest();
            }

            await _productosRepository.UpdateProductoAsync(producto);
            return NoContent();
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _productosRepository.GetProductoByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            await _productosRepository.DeleteProductoAsync(id);
            return NoContent();
        }
    }
}

