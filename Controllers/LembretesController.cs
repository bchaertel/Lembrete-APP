using LembreteApp.Models;
using LembreteApp.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using LembreteApp.Interfaces;

namespace LembreteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LembretesController : ControllerBase
    {
        private readonly ILembreteService _lembreteService;

        public LembretesController(ILembreteService lembreteService)
        {
            _lembreteService = lembreteService;
        }

        [HttpGet]
        public ActionResult<List<Lembrete>> Get()
        {
            var lembretes = _lembreteService.Get();
            return Ok(lembretes); // Retorna um OkObjectResult que encapsula a lista de Lembrete
        }

        [HttpGet("{id:length(24)}", Name = "GetLembrete")]
        public ActionResult<Lembrete> Get(string id)
        {
            var lembrete = _lembreteService.Get(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            return lembrete;
        }

        [HttpPost]
        public ActionResult<Lembrete> Create(Lembrete lembrete)
        {
            if (string.IsNullOrEmpty(lembrete.Nome) || lembrete.Data <= DateTime.Now)
            {
                return BadRequest("Nome deve estar preenchido e Data deve ser válida e no futuro.");
            }

            _lembreteService.Create(lembrete);

            return CreatedAtRoute("GetLembrete", new { id = lembrete.Id?.ToString() }, lembrete);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Lembrete lembreteIn)
        {
            var lembrete = _lembreteService.Get(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            _lembreteService.Update(id, lembreteIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var lembrete = _lembreteService.Get(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            _lembreteService.Remove(lembrete.Id);

            return NoContent();
        }
    }
}
