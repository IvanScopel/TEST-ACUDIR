using Dominio;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace Acudir.Test.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {

        private readonly IPersonaRepository _repository;

        public PersonaController(IPersonaRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll(string? nombre, int? edad)
        {
            var personas = _repository.GetAll(nombre, edad);
            return Ok(personas);
        }

        [HttpPost]
        public IActionResult Add(Persona persona)
        {
            try
            {
                _repository.Add(persona); // Intenta agregar la persona al repositorio
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al intentar agregar la persona. Inténtalo de nuevo más tarde.");
            }
        }

        [HttpPut]
        public IActionResult Update(Persona persona)
        {
            _repository.Update(persona);
            return NoContent();
        }
    }
}
