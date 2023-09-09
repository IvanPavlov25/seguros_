using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parcial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentasRepository _ventasRepository;

        public VentasController(IVentasRepository ventasRepository)
        {
            _ventasRepository = ventasRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Venta>> Get()
        {
            return await _ventasRepository.GetAllVentasAsync();
        }

        [HttpGet("{id}")]
        public async Task<Venta> Get(int id)
        {
            return await _ventasRepository.GetVentaByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Venta venta)
        {
            var result = await _ventasRepository.InsertVentaAsync(venta);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Venta venta)
        {
            var result = await _ventasRepository.UpdateVentaAsync(venta);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ventasRepository.DeleteVentaAsync(id);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
