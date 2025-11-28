using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Application.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _repo;
        private readonly ICursoRepository _cursoRepo;
        private readonly IAlunoRepository _alunoRepo;

        public MatriculaService(
            IMatriculaRepository repo,
            ICursoRepository cursoRepo,
            IAlunoRepository alunoRepo)
        {
            _repo = repo;
            _cursoRepo = cursoRepo;
            _alunoRepo = alunoRepo;
        }

        public void Adicionar(Matricula matricula)
        {
            var aluno = _alunoRepo.ObterPorID(matricula.IdAluno);
            if (aluno == null)
                throw new Exception("Aluno não encontrado.");

            var curso = _cursoRepo.ObterPorID(matricula.IdCurso);
            if (curso == null)
                throw new Exception("Curso não encontrado.");

            if (!curso.Ativo)
                throw new Exception("Aluno não pode ser matriculado em curso inativo.");

            var existente = _repo.Obter(matricula.IdAluno, matricula.IdCurso);
            if (existente != null)
                throw new Exception("Aluno já está matriculado neste curso.");

            _repo.Adicionar(matricula);
        }

        public void Atualizar(Matricula matricula)
        {
            _repo.Atualizar(matricula);
        }

        public void Desativar(int idAluno, int idCurso)
        {
            _repo.Desativar(idAluno, idCurso);
        }

        public List<Matricula> ObterTodos() => _repo.ObterTodos();

        public Matricula Obter(int idAluno, int idCurso)
            => _repo.Obter(idAluno, idCurso);
    }
}
}
