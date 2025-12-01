using Microsoft.AspNetCore.Mvc;
using Projeto.API.Dto.Request;
using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;

namespace Projeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpPost]
        public IActionResult Adicionar(NovoCursoRequest novoCursoRequest)
        {
            _cursoService.Adicionar(
                CursoFactory.NovoCurso(
                    novoCursoRequest.nome,
                    novoCursoRequest.nomeCoordenador,
                    novoCursoRequest.cargaHoraria
                    ));
            return Ok("Curso adicionado com sucesso!");
        }

        [HttpPut]
        public IActionResult Atualizar(AtualizarCursoRequest atualizarCursoRequest)
        {
            try
            {
               
                var cursoAtualizado = new Curso
                (
                    atualizarCursoRequest.cursoID,
                    atualizarCursoRequest.nome,
                    atualizarCursoRequest.nomeCoordenador,
                    atualizarCursoRequest.ativo
                );

                _cursoService.Atualizar(cursoAtualizado);

                return Ok("Curso atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idCurso}")]
        public IActionResult Deletar(int idCurso)
        {
            try
            {
                _cursoService.Deletar(idCurso);
                return Ok("Curso deletado com sucesso!");
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
                var cursosobtidos = _cursoService.ObterTodos();
                return Ok(cursosobtidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idCurso}")]
        public IActionResult ObterPorId(int idCurso)
        {
            try
            {
               
                var curso = _cursoService.ObterPorID(idCurso);
                if (curso == null)
                {
                    return NotFound("Curso não encontrado.");
                }
                return Ok(curso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
