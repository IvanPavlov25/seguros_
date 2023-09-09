using model;
using MySql.Data.MySqlClient;
using System.Data;

namespace Parcial
{
    public class ClienteRepositorio : IClientesRepository
    {
        private readonly string _connectionString;

        public ClienteRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            var clientes = new List<Cliente>();

            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var command = new MySqlCommand("SELECT * FROM Cliente", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        clientes.Add(new Cliente
                        {
                            idCliente = reader.GetInt32("idCliente"),
                            nombre = reader.GetString("nombre"),
                            apellido = reader.GetString("apellido"),
                            email = reader.GetString("email"),
                            telefono = reader.GetString("telefono")
                        });
                    }
                }
            }

            return clientes;
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            Cliente cliente = null;

            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                // Usamos parámetros en la consulta.
                var command = new MySqlCommand("SELECT * FROM Cliente WHERE idCliente = @id", connection);

                // Asignamos el valor al parámetro.
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cliente = new Cliente
                        {
                            idCliente = reader.GetInt32("idCliente"),
                            nombre = reader.GetString("nombre"),
                            apellido = reader.GetString("apellido"),
                            email = reader.GetString("email"),
                            telefono = reader.GetString("telefono")
                        };
                    }
                }
            }
            return cliente;
        }

        public async Task<Cliente> InsertClienteAsync(Cliente cliente)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var command = new MySqlCommand($"INSERT INTO Cliente (nombre, apellido, email, telefono) VALUES ('{cliente.nombre}', '{cliente.apellido}', '{cliente.email}', '{cliente.telefono}')", connection);
                await command.ExecuteNonQueryAsync();
            }

            return cliente;
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var command = new MySqlCommand($"UPDATE Cliente SET nombre='{cliente.nombre}', apellido='{cliente.apellido}', email='{cliente.email}', telefono='{cliente.telefono}' WHERE idCliente = {cliente.idCliente}", connection);
                await command.ExecuteNonQueryAsync();
            }

            return cliente;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var command = new MySqlCommand($"DELETE FROM Cliente WHERE idCliente = {id}", connection);
                var result = await command.ExecuteNonQueryAsync();

                return result > 0;
            }
        }

        public Task<IEnumerable<Cliente>> getClientes()
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> getClienteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task insertCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Task updateCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Task deleteCliente(int id)
        {
            throw new NotImplementedException();
        }
    }

   
}
