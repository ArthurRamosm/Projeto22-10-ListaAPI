using Projeto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Interfaces
{
    public interface IAlunoService
    {
        public void Adicionar(Aluno aluno);
        public void Atualizar(Aluno aluno);
        void Deletar(int IDAluno);
        public List<Aluno> ObterTodos();
        public Aluno ObterPorID(int IDAluno);
        public Aluno ObterPOrcpf(string cpf);
        public Aluno ObterPorMAtricula(string matricula);
    }
}
