using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Entidades
{
    public class Curso
    {
        public Curso(int IdCurso, string nome, string nomeCoordenador, bool ativo)
        {
            IdCurso = idCurso;
            Nome = nome;
            NomeCoordenador = nomeCoordenador;
            this.ativo = ativo;
        }

        public int idCurso { get; private set;}
        public string Nome { get; private set; }
        public string NomeCoordenador { get; private set; }
        public bool ativo { get; private set; }
    }
}
