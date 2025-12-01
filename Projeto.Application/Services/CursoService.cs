using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Application.Services
{
    public class CursoService(ICursoRepository cursoRepository) : ICursoService
    {
        private readonly ICursoRepository _cursoRepository = cursoRepository;

        public void Adicionar(Curso curso)
        {
            if (string.IsNullOrEmpty(curso.Nome))
                throw new Exception("O nome do curso é obrigatório.");

            if (_cursoRepository.ObterTodos().Any(c => c.Nome == curso.Nome))
                throw new Exception("Já existe um curso com esse nome.");

            if (string.IsNullOrEmpty(curso.NomeCoordenador))
                throw new Exception("O nome do coordenador é obrigatório.");

            // Removido CargaHoraria pois não existe na entidade Curso

            _cursoRepository.Adicionar(curso);
        }

        public void Atualizar(Curso curso)
        {
            Curso buscaCurso = _cursoRepository.ObterPorID(curso.idCurso);

            if (buscaCurso is null)
                throw new Exception("Curso não encontrado ou não existente.");
            _cursoRepository.Atualizar(curso);
        }

        public void Deletar(int IDcurso)
        {
            Curso buscaCurso = _cursoRepository.ObterPorID(IDcurso);

            if (buscaCurso is null)
                throw new Exception("Curso não encontrado ou não existente.");

            _cursoRepository.Deletar(IDcurso);
        }

        public bool VerificarSeAtivo(int IDcurso)
        {
            var curso = _cursoRepository.ObterPorID(IDcurso);
            if (curso is null)
                throw new Exception("Curso não encontrado ou não existente.");
            return curso.ativo;
        }

        public List<Curso> ObterTodos()
        {
            var cursos = _cursoRepository.ObterTodos();

            if (cursos.Count == 0)
                throw new Exception("Nenhum curso cadastrado.");

            return cursos;
        }

        public Curso ObterPorID(int IDcurso)
        {
            var curso = _cursoRepository.ObterPorID(IDcurso);
            if (curso is null)
                throw new Exception("Curso não encontrado ou não existente.");

            if (!curso.ativo)
                throw new Exception("Curso inativo.");

            return curso;
        }

        public Curso ObterPorNome(string nome)
        {
            var curso = _cursoRepository.ObterPorNome(nome);
            if (curso is null)
                throw new Exception("Curso não encontrado ou não existente.");

            if (!curso.ativo)
                throw new Exception("Curso inativo.");

            return curso;
        }
    }
}
