using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Application.Service
{
    public class AlunoService : IAlunoService
    {

        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public void Adicionar(Aluno aluno)
        {
            Aluno buscaAluno = _alunoRepository.ObterPOrcpf(aluno.CPF);
            if(buscaAluno != null)
            {
                throw new Exception("Já existe um aluno cadastrado com esse CPF");
            }
            buscaAluno = _alunoRepository.ObterPorMAtricula(aluno.matricula);
            if(buscaAluno != null)
            {
                throw new Exception("Já existe um aluno cadastrado com essa matricula");
            }
                _alunoRepository.Adicionar(aluno);
        }

        public void Atualizar(Aluno aluno)
        {
            Aluno buscaAluno = _alunoRepository.ObterPorID(aluno.IDAluno);
            if(buscaAluno == null)
            {
                throw new Exception("Aluno não encotratado");
            }

            buscaAluno = _alunoRepository.ObterPOrcpf(aluno.CPF);

            if (buscaAluno.IDAluno != aluno.IDAluno)
            {
                throw new Exception("Ja Existe um Aluno com esse cpf");
            }
        }

        public void Deletar(int IDAluno)
        {
            Aluno buscaAluno = _alunoRepository.ObterPorID(IDAluno);
            if(buscaAluno == null)
            {
                throw new Exception("Aluno não encontrato");
            }
            _alunoRepository.Deletar(IDAluno);
        }

        public Aluno ObterPOrcpf(string cpf)
        {
            return _alunoRepository.ObterPOrcpf(cpf);
        }

        public Aluno ObterPorID(int IDAluno)
        {
            return _alunoRepository.ObterPorID(IDAluno);
        }

        public Aluno ObterPorMAtricula(string matricula)
        {
            return _alunoRepository.ObterPorMAtricula(matricula);
        }

        public List<Aluno> ObterTodos()
        {
            return _alunoRepository.ObterTodos();
        }
    }
}
