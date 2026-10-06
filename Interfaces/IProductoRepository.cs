using EcommerceAPI.DTO;
using EcommerceAPI.Models;

namespace EcommerceAPI.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetProductos();
        Task<string> CreateProducto(ProductoDTO item);
        Task<string> UpdateProducto(ProductoDTO item, int id);
        Task<string> DeleteProducto(int id);
    }
}
