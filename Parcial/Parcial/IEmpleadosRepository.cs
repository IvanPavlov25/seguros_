using model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parcial
{
    public interface IEmpleadosRepository
    {
        Task<IEnumerable<Empleado>> GetEmpleadosAsync();
        Task<Empleado> GetEmpleadoByIdAsync(int id);
        Task InsertEmpleadoAsync(Empleado empleado);
        Task UpdateEmpleadoAsync(Empleado empleado);
        Task<bool> DeleteEmpleadoAsync(int id);
    }
}
