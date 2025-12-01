using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Data.Repositorios
{
    public class CursoRepository : ICursoRepository
    {
        private readonly List<Curso> _cursos = new();

        public void Adicionar(Curso curso)
        {
            if (curso == null) throw new ArgumentNullException(nameof(curso));
            _cursos.Add(curso);
        }

        public void Atualizar(Curso curso)
        {
            if (curso == null) throw new ArgumentNullException(nameof(curso));

            var index = _cursos.FindIndex(c => c.idCurso == curso.idCurso);
            if (index >= 0)
            {
                // Substitui o objeto na lista para evitar depender de setters possivelmente inacessíveis
                _cursos[index] = curso;
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
            => _cursos.FirstOrDefault(c => 
                !string.IsNullOrEmpty(c.Nome) && 
                c.Nome.Equals(nome ?? string.Empty, StringComparison.OrdinalIgnoreCase));
    }
}
