using Projeto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Interfaces
{
    public interface ICursoService
    {
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
        void Deletar(int idCurso);
        List<Curso> ObterTodos();
        Curso ObterPorID(int idCurso);
        Curso ObterPorNome(string nome);
    }
}
