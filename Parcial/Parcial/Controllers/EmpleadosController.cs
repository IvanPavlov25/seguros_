using Microsoft.AspNetCore.Mvc;
using model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parcial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosRepository _repository;

        public EmpleadosController(IEmpleadosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Empleado>> Get()
        {
            return await _repository.GetEmpleadosAsync();
        }

        [HttpGet("{id}")]
        public async Task<Empleado> GetById(int id)
        {
            return await _repository.GetEmpleadoByIdAsync(id);
        }

        [HttpPost]
        public async Task Insert([FromBody] Empleado empleado)
        {
            await _repository.InsertEmpleadoAsync(empleado);
        }

        [HttpPut]
        public async Task Update([FromBody] Empleado empleado)
        {
            await _repository.UpdateEmpleadoAsync(empleado);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteEmpleadoAsync(id);
        }
    }
}
