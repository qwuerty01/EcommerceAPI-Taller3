using EcommerceAPI.DB;
using EcommerceAPI.DTO;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;
using EcommerceAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductoRepository(AppDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<Producto>> GetProductos()
        {
            return await _context.Producto.ToListAsync();
        }

        public async Task<string> CreateProducto(ProductoDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.Nombre))
                throw new ArgumentException("El nombre del producto no puede estar vacío.");

            if (item.Precio < 0)
                throw new ArgumentException("El precio no puede ser negativo.");

            if (item.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");

            string imagenUrl = null;
            if (!string.IsNullOrWhiteSpace(item.ImagenBase64))
            {
                imagenUrl = await _cloudinaryService.UploadImageAsync(item.ImagenBase64);
            }

            var nuevoProducto = new Producto
            {
                Nombre = item.Nombre,
                Descripcion = item.Descripcion,
                Precio = item.Precio,
                Stock = item.Stock,
                ImagenUrl = imagenUrl
            };

            await _context.Producto.AddAsync(nuevoProducto);
            await _context.SaveChangesAsync();

            return "El producto fue creado con éxito.";
        }

        public async Task<string> UpdateProducto(ProductoDTO item, int id)
        {
            var productoExiste = await _context.Producto.FirstOrDefaultAsync(p => p.Id == id);

            if (productoExiste == null)
                throw new ArgumentException("El producto no se encuentra registrado.");

            if (string.IsNullOrWhiteSpace(item.Nombre))
                throw new ArgumentException("El nombre del producto no puede estar vacío.");

            if (item.Precio < 0)
                throw new ArgumentException("El precio no puede ser negativo.");

            if (item.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");

            productoExiste.Nombre = item.Nombre;
            productoExiste.Descripcion = item.Descripcion;
            productoExiste.Precio = item.Precio;
            productoExiste.Stock = item.Stock;

            if (!string.IsNullOrWhiteSpace(item.ImagenBase64))
            {
                productoExiste.ImagenUrl = await _cloudinaryService.UploadImageAsync(item.ImagenBase64);
            }

            await _context.SaveChangesAsync();

            return "El producto fue actualizado con éxito.";
        }

        public async Task<string> DeleteProducto(int id)
        {
            var productoExiste = await _context.Producto.FirstOrDefaultAsync(p => p.Id == id);

            if (productoExiste == null)
                throw new ArgumentException("El producto no se encuentra registrado.");

            _context.Producto.Remove(productoExiste);
            await _context.SaveChangesAsync();

            return "El producto fue eliminado con éxito.";
        }
    }
}
