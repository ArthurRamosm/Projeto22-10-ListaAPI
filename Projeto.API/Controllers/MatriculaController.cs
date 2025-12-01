using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Interfaces;
using Projeto.Domain;

namespace Projeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController(IMatriculaService matriculaService) : ControllerBase 
    {
        private readonly IMatriculaService _matriculaService = matriculaService;

        [HttpPost("{idAluno}/{idCurso}")]
        public IActionResult Adicionar(int idAluno, int idCurso)
        {
            try
            {
               
                _matriculaService.Adicionar(new Domain.Entidades.Matricula(idAluno, idCurso, 0, DateTime.Now, true));
                return Ok("Matrícula realizada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            try
            {
                var matriculasObtidas = _matriculaService.ObterTodos();
                return Ok(matriculasObtidas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
