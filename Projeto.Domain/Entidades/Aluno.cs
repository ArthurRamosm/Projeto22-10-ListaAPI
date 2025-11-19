using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Entidades


{
    //class rica get publico e set privado
    public class Aluno
    {
        public Aluno(int IDAluno,string Nome, string CPF, string matricula, string email)
        {
            this.IDAluno = IDAluno;
            this.Nome = Nome;
            this.CPF = CPF;
            this.matricula = matricula;
            this.email = email;
        }

        public int IDAluno { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string matricula { get; private set; }
        public string email { get; private set; }

    }
    public static class AlunoFactory
    {
        public static Aluno NovoAluno(
            string pNome,
            string pCpf,
            string pMatricula,
            string pEmail
            )
        {
            return new Aluno(0, pNome, pCpf, pMatricula, pEmail);
        }

        public static Aluno AlunoExistente(int pidAlino, string pNome,
            string pCpf,
            string pMatricula,
            string pEmail
            )
        {
            return new Aluno(pidAlino, pNome, pCpf, pMatricula, pEmail);
        }
    }
}
