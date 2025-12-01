using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Interfaces;
using Projeto.Domain;

namespace Projeto.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculaController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;

        public MatriculaController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var todos = _matriculaService.ObterTodos();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var mat = _matriculaService.ObterTodos().FirstOrDefault(m => m.IdMatricula == id);
            if (mat == null) return NotFound();
            return Ok(mat);
        }

        [HttpPost]
        public IActionResult Post([FromBody] MatriculaDTO dto)
        {
            if (dto == null) return BadRequest();

            // Mapear DTO para entidade Matricula.
            var matricula = new Matricula(
                dto.idAluno,
                dto.idCurso,
                dto.IdMatricula, // pode ser 0 para nova matrícula
                dto.DataMatricula ?? DateTime.UtcNow,
                dto.Ativa
            );

            _matriculaService.Adicionar(matricula);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var mat = _matriculaService.ObterTodos().FirstOrDefault(m => m.IdMatricula == id);
            if (mat == null) return NotFound();

            // A interface fornece Desativar(idAluno, idCurso)
            _matriculaService.Desativar(mat.idAluno, mat.idCurso);
            return Ok();
        }
    }

    // DTO local simples para aceitar payloads no controller.
    public class MatriculaDTO
    {
        public int idAluno { get; set; }
        public int idCurso { get; set; }
        public int IdMatricula { get; set; }
        public DateTime? DataMatricula { get; set; }
        public bool Ativa { get; set; } = true;
    }
}
