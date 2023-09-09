using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace Parcial
{
    public class VentaRepositorio : IVentasRepository
    {
        private readonly string _connectionString;

        public VentaRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Venta>> GetAllVentasAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var ventas = await connection.QueryAsync<Venta>("SELECT * FROM Venta");
                return ventas.ToList();
            }
        }

        public async Task<Venta> GetVentaByIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var venta = await connection.QueryFirstOrDefaultAsync<Venta>("SELECT * FROM Venta WHERE idVenta = @Id", new { Id = id });
                return venta;
            }
        }

        public async Task<bool> InsertVentaAsync(Venta venta)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync("INSERT INTO Venta(idEmpleado, idCliente, idProducto, fechaVenta) VALUES (@IdEmpleado, @IdCliente, @IdProducto, @FechaVenta)", venta);
                return result > 0;
            }
        }

        public async Task<bool> UpdateVentaAsync(Venta venta)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync("UPDATE Venta SET idEmpleado = @IdEmpleado, idCliente = @IdCliente, idProducto = @IdProducto, fechaVenta = @FechaVenta WHERE idVenta = @IdVenta", venta);
                return result > 0;
            }
        }

        public async Task<bool> DeleteVentaAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync("DELETE FROM Venta WHERE idVenta = @Id", new { Id = id });
                return result > 0;
            }
        }
    }
}
