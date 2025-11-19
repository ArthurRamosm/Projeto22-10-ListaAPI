using Microsoft.AspNetCore.Mvc;
using Projeto.API.Dto.Request;
using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;

namespace Projeto.API.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet("api/aluno/obter-todos")]
        public IActionResult ObterTodos()
        {
            var LIstaAluno = _alunoService.ObterTodos();

            if (LIstaAluno is null) return NotFound("Nenhum aluno encontrado");     

            return Ok(LIstaAluno);
        }

        [HttpGet("api/aluno/obter-por-id/{id}")]
        public IActionResult ObterPorId(int IdAluno) 
        { 
            var aluno =_alunoService.ObterPorID(IdAluno);
            if (aluno is null) return NotFound("Aluno nâo encontrado");
            return Ok(aluno);
        }
        [HttpGet("api/aluno/obter-por-cpf/{cpf}")]
        public IActionResult ObterPorCpf(string cpf)
        { 
        var aluno = _alunoService.ObterPOrcpf(cpf);
            if (aluno is null) return NotFound("Aluno nâo encontrado");
            return Ok(aluno);
        }

        [HttpGet("api/aluno/obter-por-matricula}")]
        public IActionResult ObterPorMatricula(string matricula)
        {
            var aluno = _alunoService.ObterPorMAtricula(matricula);
            if (aluno is null) return NotFound("Aluno nâo encontrado");
            return Ok(aluno);

        }

        [HttpDelete("api/aluno/remover")]
        public IActionResult Remover(int Idaluno)
        {
            var aluno = _alunoService.ObterPorID(Idaluno);
            if (aluno is null) return NotFound("Aluno não encontardo");
            _alunoService.Deletar(Idaluno);
            return NoContent();
        }


        [HttpPost("api/aluno/adicionar")]
        public IActionResult Adicionar( NovoAlunoRequest novoAlunoRequest)

        {
            _alunoService.Adicionar(
                AlunoFactory.NovoAluno(
                  novoAlunoRequest.nome,
                  novoAlunoRequest.cpf,
                  novoAlunoRequest.matricula,
                  novoAlunoRequest.email
                ));
            return Ok();
        }
        [HttpPut("api/aluno/atualizar")]
        public IActionResult Atualizar(AtualizarAlunoRequest atualizarAlunoRequest)
        {
            _alunoService.Atualizar(
                AlunoFactory.AlunoExistente(
                  atualizarAlunoRequest.IDAluno,
                  atualizarAlunoRequest.nome,
                  atualizarAlunoRequest.cpf,
                  atualizarAlunoRequest.matricula,
                  atualizarAlunoRequest.email
                ));
            return Ok("Aluno Atualizado com sucesso");
        }

            
    }

}
