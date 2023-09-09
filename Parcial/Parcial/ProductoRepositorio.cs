using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using model;

namespace Parcial
{
    public class ProductoRepositorio : IProductosRepository
    {
        private readonly string _connectionString;

        public ProductoRepositorio(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Producto>> GetAllProductosAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Producto>("SELECT * FROM Producto");
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Producto>("SELECT * FROM Producto WHERE idProducto = @id", new { id });
        }

        public async Task<bool> InsertProductoAsync(Producto producto)
        {
            using var connection = CreateConnection();
            var rowsAffected = await connection.ExecuteAsync("INSERT INTO Producto (nombre, descripcion, precio) VALUES (@nombre, @descripcion, @precio)", producto);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateProductoAsync(Producto producto)
        {
            using var connection = CreateConnection();
            var rowsAffected = await connection.ExecuteAsync("UPDATE Producto SET nombre = @nombre, descripcion = @descripcion, precio = @precio WHERE idProducto = @idProducto", producto);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            using var connection = CreateConnection();
            var rowsAffected = await connection.ExecuteAsync("DELETE FROM Producto WHERE idProducto = @id", new { id });
            return rowsAffected > 0;
        }

        Task IProductosRepository.InsertProductoAsync(Producto producto)
        {
            throw new NotImplementedException();
        }

        Task IProductosRepository.UpdateProductoAsync(Producto producto)
        {
            throw new NotImplementedException();
        }

        Task IProductosRepository.DeleteProductoAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
