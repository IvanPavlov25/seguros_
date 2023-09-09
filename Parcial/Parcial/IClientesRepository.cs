using System.Collections.Generic;
using System.Threading.Tasks;
using model;  // Asegúrate de que este namespace es correcto

namespace Parcial
{
    public interface IClientesRepository
    {
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<Cliente> InsertClienteAsync(Cliente cliente);
        Task<Cliente> UpdateClienteAsync(Cliente cliente);
        Task<bool> DeleteClienteAsync(int id);
    }
}

