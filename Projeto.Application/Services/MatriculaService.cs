using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Application.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ICursoRepository _cursoRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository, IAlunoRepository alunoRepository, ICursoRepository cursoRepository)
        {
            _matriculaRepository = matriculaRepository;
            _alunoRepository = alunoRepository;
            _cursoRepository = cursoRepository;
        }

        public void Adicionar(Matricula matricula)
        {
            var aluno = _alunoRepository.ObterPorID(matricula.idAluno) ?? throw new Exception("Aluno não encontrado ou inexistente.");

            var curso = _cursoRepository.ObterPorID(matricula.idCurso) ?? throw new Exception("Curso não encontrado ou inexistente.");

            // Corrigido: propriedade correta é 'ativo' (minúsculo)
            if (!curso.ativo)
                throw new Exception("Não é possível matricular em um curso inativo.");

            var matriculasAlunos = _matriculaRepository.ObterTodos().Where(m => m.idAluno == matricula.idAluno).ToList();
            bool jaMatriculado = matriculasAlunos.Any(m => m.idCurso == matricula.idCurso);

            if (jaMatriculado)
                throw new Exception("Aluno já está matriculado neste curso.");

            // Corrigido: 'IdMatricula' (com I maiúsculo) e não 'idMatricula'
            var novaMatricula = new Matricula(matricula.idAluno, matricula.idCurso, matricula.IdMatricula, DateTime.Now, true);

            _matriculaRepository.Adicionar(novaMatricula);
        }

        public List<Matricula> ObterTodos()
        {
            var matriculas = _matriculaRepository.ObterTodos();

            // CA1860: Use Count == 0 ao invés de !Any()
            if (matriculas == null || matriculas.Count == 0)
                throw new Exception("Nenhuma matrícula encontrada.");

            return matriculas;
        }

        public List<Matricula> ObterPorAluno(int IDAluno)
        {
            var matriculas = _matriculaRepository.ObterTodos().Where(m => m.idAluno == IDAluno).ToList();

            if (matriculas == null || matriculas.Count == 0)
                throw new Exception("Nenhuma matrícula encontrada para o aluno informado.");

            return matriculas;
        }

        public List<Matricula> ObterPorCurso(int IDCurso)
        {
            var matriculas = _matriculaRepository.ObterTodos().Where(m => m.idCurso == IDCurso).ToList();
            if (matriculas == null || matriculas.Count == 0)
                throw new Exception("Nenhuma matrícula encontrada para o curso informado.");

            return matriculas;
        }

        public void Atualizar(Matricula matricula)
        {
            _matriculaRepository.Atualizar(matricula);
        }

        public void Desativar(int idAluno, int idCurso)
        {
            _matriculaRepository.Desativar(idAluno, idCurso);
        }

        public Matricula Obter(int idAluno, int idCurso)
        {
            var matricula = _matriculaRepository.Obter(idAluno, idCurso) ?? throw new Exception("Matrícula não encontrada para o aluno e curso informados.");
            return matricula;
        }
    }
}
