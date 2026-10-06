using EcommerceAPI.DTO;
using EcommerceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _productoRepository.GetProductos();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductoDTO item)
        {
            try
            {
                var respuesta = await _productoRepository.CreateProducto(item);
                return Ok(respuesta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductoDTO item)
        {
            try
            {
                var respuesta = await _productoRepository.UpdateProducto(item, id);
                return Ok(respuesta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var respuesta = await _productoRepository.DeleteProducto(id);
                return Ok(respuesta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
