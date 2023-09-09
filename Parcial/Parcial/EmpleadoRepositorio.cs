using model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Parcial
{
    public class EmpleadoRepositorio : IEmpleadosRepository
    {
        private readonly string _connectionString;

        public EmpleadoRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Empleado>> GetEmpleadosAsync()
        {
            var empleados = new List<Empleado>();

            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var command = new MySqlCommand("SELECT * FROM Empleado", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        empleados.Add(new Empleado
                        {
                            idEmpleado = reader.GetInt32("idEmpleado"),
                            nombre = reader.GetString("nombre"),
                            apellido = reader.GetString("apellido"),
                            email = reader.GetString("email"),
                            telefono = reader.GetString("telefono")
                        });
                    }
                }
            }

            return empleados;
        }

        public async Task<Empleado> GetEmpleadoByIdAsync(int id)
        {
            Empleado empleado = null;

            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var command = new MySqlCommand("SELECT * FROM Empleado WHERE idEmpleado = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        empleado = new Empleado
                        {
                            idEmpleado = reader.GetInt32("idEmpleado"),
                            nombre = reader.GetString("nombre"),
                            apellido = reader.GetString("apellido"),
                            email = reader.GetString("email"),
                            telefono = reader.GetString("telefono")
                        };
                    }
                }
            }

            return empleado;
        }

        public async Task InsertEmpleadoAsync(Empleado empleado)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var command = new MySqlCommand($"INSERT INTO Empleado (nombre, apellido, email, telefono) VALUES (@nombre, @apellido, @email, @telefono)", connection);
                command.Parameters.AddWithValue("@nombre", empleado.nombre);
                command.Parameters.AddWithValue("@apellido", empleado.apellido);
                command.Parameters.AddWithValue("@email", empleado.email);
                command.Parameters.AddWithValue("@telefono", empleado.telefono);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateEmpleadoAsync(Empleado empleado)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var command = new MySqlCommand($"UPDATE Empleado SET nombre=@nombre, apellido=@apellido, email=@email, telefono=@telefono WHERE idEmpleado = @idEmpleado", connection);
                command.Parameters.AddWithValue("@nombre", empleado.nombre);
                command.Parameters.AddWithValue("@apellido", empleado.apellido);
                command.Parameters.AddWithValue("@email", empleado.email);
                command.Parameters.AddWithValue("@telefono", empleado.telefono);
                command.Parameters.AddWithValue("@idEmpleado", empleado.idEmpleado);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> DeleteEmpleadoAsync(int id)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var command = new MySqlCommand($"DELETE FROM Empleado WHERE idEmpleado = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                var result = await command.ExecuteNonQueryAsync();

                return result > 0;
            }
        }
    }
}
