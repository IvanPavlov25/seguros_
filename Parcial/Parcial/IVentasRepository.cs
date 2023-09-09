using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parcial
{
    public interface IVentasRepository
    {
        Task<IEnumerable<Venta>> GetAllVentasAsync();
        Task<Venta> GetVentaByIdAsync(int id);
        Task<bool> InsertVentaAsync(Venta venta);
        Task<bool> UpdateVentaAsync(Venta venta);
        Task<bool> DeleteVentaAsync(int id);
    }
}
