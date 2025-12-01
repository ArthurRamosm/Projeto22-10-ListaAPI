using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Projeto.Data.Repositorios
{
    // Plano (pseudocódigo detalhado):
    // 1) Converter a declaração da classe para usar um construtor normal em vez de "primary constructor".
    //    - Adicionar campo readonly _connectionString inicializado dentro do construtor.
    // 2) Corrigir nomes de propriedades/parametros: a entidade usa `Ativa` e `IdMatricula`.
    //    - Trocar usos de `Ativo` por `Ativa` quando se refere à propriedade do objeto.
    //    - Ao mapear para a coluna do banco, manter o nome da coluna (por exemplo "Ativo") se o DB usar esse nome.
    // 3) Ajustar SELECTs para trazer `IdMatricula` (construtor da entidade exige esse campo).
    //    - SELECT IdMatricula, idAluno, idCurso, DataMatricula, Ativo ...
    //    - Construir `new Matricula(idAluno, idCurso, idMatricula, DataMatricula, ativa)` com a ordem esperada.
    // 4) Implementar membros faltantes da interface `IMatriculaRepository`: `Atualizar`, `Desativar`, `Obter`.
    //    - `Atualizar` -> UPDATE por IdMatricula.
    //    - `Desativar` -> UPDATE definindo Ativo = 0 por idAluno e idCurso.
    //    - `Obter` -> SELECT único por idAluno e idCurso, retorna null se não existir.
    // 5) Garantir que todos os comandos usem parâmetros e abram/fechem conexão corretamente.
    // 6) Não alterar linhas não relacionadas e manter estilo consistente.

    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly string _connectionString;

        public MatriculaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string não pode ser nula.");
        }

        public void Adicionar(Matricula matricula)
        {
            var sql = @"INSERT INTO Matriculas (idAluno, idCurso, DataMatricula, Ativo) 
                        VALUES (@idAluno, @idCurso, @DataMatricula, @Ativo)";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idAluno", matricula.idAluno);
            cmd.Parameters.AddWithValue("@idCurso", matricula.idCurso);
            cmd.Parameters.AddWithValue("@DataMatricula", matricula.DataMatricula);
            // coluna do banco permanece "Ativo", valor vem de propriedade `Ativa`
            cmd.Parameters.AddWithValue("@Ativo", matricula.Ativa);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Atualizar(Matricula matricula)
        {
            var sql = @"UPDATE Matriculas
                        SET DataMatricula = @DataMatricula, Ativo = @Ativo
                        WHERE IdMatricula = @IdMatricula";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@DataMatricula", matricula.DataMatricula);
            cmd.Parameters.AddWithValue("@Ativo", matricula.Ativa);
            cmd.Parameters.AddWithValue("@IdMatricula", matricula.IdMatricula);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Desativar(int idAluno, int idCurso)
        {
            var sql = @"UPDATE Matriculas SET Ativo = 0 WHERE idAluno = @idAluno AND idCurso = @idCurso";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idAluno", idAluno);
            cmd.Parameters.AddWithValue("@idCurso", idCurso);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Matricula> ObterPorAluno(int IDaluno)
        {
            var sql = @"SELECT IdMatricula, idAluno, idCurso, DataMatricula, Ativo 
                        FROM Matriculas WHERE idAluno = @idAluno";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idAluno", IDaluno);

            conn.Open();

            using var reader = cmd.ExecuteReader();

            var matriculas = new List<Matricula>();
            while (reader.Read())
            {
                
                matriculas.Add(new Matricula(
                    reader.GetInt32(1), 
                    reader.GetInt32(2), 
                    reader.GetInt32(0), 
                    reader.GetDateTime(3), 
                    reader.GetBoolean(4) 
                ));
            }
            return matriculas;
        }

        public List<Matricula> ObterPorCurso(int IDcurso)
        {
            var sql = @"SELECT IdMatricula, idAluno, idCurso, DataMatricula, Ativo 
                        FROM Matriculas WHERE idCurso = @idCurso";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idCurso", IDcurso);

            conn.Open();

            using var reader = cmd.ExecuteReader();
            var listaCurso = new List<Matricula>();
            while (reader.Read())
            {
                listaCurso.Add(new Matricula(
                    reader.GetInt32(1), 
                    reader.GetInt32(2),
                    reader.GetInt32(0), 
                    reader.GetDateTime(3), 
                    reader.GetBoolean(4) 
                ));
            }
            return listaCurso;
        }

        public List<Matricula> ObterTodos()
        {
            var sql = "SELECT IdMatricula, idAluno, idCurso, DataMatricula, Ativo FROM Matriculas";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            conn.Open();

            using var reader = cmd.ExecuteReader();
            var listaAlunos = new List<Matricula>();
            while (reader.Read())
            {
                var matricula = new Matricula(
                    reader.GetInt32(1), 
                    reader.GetInt32(2), 
                    reader.GetInt32(0), 
                    reader.GetDateTime(3),
                    reader.GetBoolean(4) 
                );
                listaAlunos.Add(matricula);
            }
            return listaAlunos;
        }

        public Matricula? Obter(int idAluno, int idCurso)
        {
            var sql = @"SELECT IdMatricula, idAluno, idCurso, DataMatricula, Ativo 
                        FROM Matriculas WHERE idAluno = @idAluno AND idCurso = @idCurso";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idAluno", idAluno);
            cmd.Parameters.AddWithValue("@idCurso", idCurso);

            conn.Open();

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new Matricula(
                reader.GetInt32(1), 
                reader.GetInt32(2), 
                reader.GetInt32(0), 
                reader.GetDateTime(3), 
                reader.GetBoolean(4) 
            );
        }
    }
}   