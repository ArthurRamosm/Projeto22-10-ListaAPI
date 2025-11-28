using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projeto.Data.Repositorios
{
    class CursoRepository : ICursoRepository
    {
        private readonly List<Curso> _cursos = new();

        public void Adicionar(Curso curso)
        {
            _cursos.Add(curso);
        }

        public void Atualizar(Curso curso)
        {
            var existente = ObterPorID(curso.IdCurso);
            if (existente != null)
            {
                existente.Nome = curso.Nome;
                existente.NomeCoordenador = curso.NomeCoordenador;
                existente.Ativo = curso.Ativo;
            }
        }

        public void Deletar(int idCurso)
        {
            var curso = ObterPorID(idCurso);
            if (curso != null)
                _cursos.Remove(curso);
        }

        public List<Curso> ObterTodos() => _cursos;

        public Curso? ObterPorID(int idCurso)
            => _cursos.FirstOrDefault(c => c.idCurso == idCurso);

        public Curso? ObterPorNome(string nome)
            => _cursos.FirstOrDefault(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }
}
