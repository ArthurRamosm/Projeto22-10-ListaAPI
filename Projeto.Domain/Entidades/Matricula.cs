using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Entidades
{
    public class Matricula
    {
        public Matricula(int idAluno, int idCurso,int idMatricula,  DateTime dataMatricula, bool ativa)
        {
            this.idAluno = idAluno;
            this.idCurso = idCurso;
            IdMatricula = idMatricula;
            DataMatricula = dataMatricula;
            Ativa = ativa;
        }

        public int idAluno { get; private set; }
        public int idCurso { get; private set; }

        public int IdMatricula { get; private set; }
        public DateTime DataMatricula { get; private set; }
        public bool Ativa { get; private set; }


    }
}
