using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Application.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _repo;

        public CursoService(ICursoRepository repo)
        {
            _repo = repo;
        }

        public void Adicionar(Curso curso)
        {
            _repo.Adicionar(curso);
        }

        public void Atualizar(Curso curso)
        {
            _repo.Atualizar(curso);
        }

        public void Deletar(int idCurso)
        {
            _repo.Deletar(idCurso);
        }

        public List<Curso> ObterTodos() => _repo.ObterTodos();

        public Curso ObterPorID(int idCurso)
            => _repo.ObterPorID(idCurso);

        public Curso ObterPorNome(string nome)
            => _repo.ObterPorNome(nome);
    }
}
