using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Data.Repositorios
{
    class MatriculaRepository : IMatriculaRepository
    {
        private readonly List<Matricula> _matriculas = new();

        public void Adicionar(Matricula matricula)
        {
            _matriculas.Add(matricula);
        }

        public void Atualizar(Matricula matricula)
        {
            var existente = Obter(matricula.IdAluno, matricula.IdCurso);
            if (existente != null)
            {
                existente.Ativa = matricula.Ativa;
                existente.DataMatricula = matricula.DataMatricula;
            }
        }

        public void Desativar(int idAluno, int idCurso)
        {
            var matricula = Obter(idAluno, idCurso);
            if (matricula != null)
                matricula.Ativa = false;
        }

        public List<Matricula> ObterTodos() => _matriculas;

        public Matricula? Obter(int idAluno, int idCurso)
            => _matriculas.FirstOrDefault(m => m.IdAluno == idAluno && m.IdCurso == idCurso);
    }
}
