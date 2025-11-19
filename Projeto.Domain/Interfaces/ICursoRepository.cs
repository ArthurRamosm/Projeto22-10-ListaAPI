using Projeto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Interfaces
{
    public interface ICursoRepository
    {
        public void AdicionarCurso( Curso curso );

        public void AtualizarCurso(Curso curso);


    }
}
