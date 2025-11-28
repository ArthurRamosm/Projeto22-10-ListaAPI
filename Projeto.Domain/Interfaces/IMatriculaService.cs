using Projeto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Interfaces
{
    public interface IMatriculaService
    {
        void Adicionar(Matricula matricula);
        void Atualizar(Matricula matricula);
        void Desativar(int idAluno, int idCurso);
        List<Matricula> ObterTodos();
        Matricula Obter(int idAluno, int idCurso);
    }
}
