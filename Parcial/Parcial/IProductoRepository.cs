using model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parcial
{
    public interface IProductosRepository
    {
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task InsertProductoAsync(Producto producto);
        Task UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
    }

    class IProductoRepository
    {
    }
}
