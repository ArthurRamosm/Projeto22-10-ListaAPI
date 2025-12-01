using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Interfaces;

namespace Projeto.API.Controllers
{
    public class CursoController : Controller
    {
        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_cursoService.ObterTodos());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var curso = _cursoService.ObterPorID(id);
            if (curso == null) return NotFound();
            return Ok(curso);
        }

        [HttpPost]
        public IActionResult Post(CursoDTO dto)
        {
            _cursoService.Adicionar(dto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(CursoDTO dto)
        {
            _cursoService.Atualizar(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _cursoService.Deletar(id);
            return Ok();
        }
    }
}
