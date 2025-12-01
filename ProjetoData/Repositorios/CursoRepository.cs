using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Projeto.Data.Repositorios
{
    public class CursoRepository : ICursoRepository
    {
        private readonly string _connectionString;

        public CursoRepository(IConfiguration configuration)
        {
            
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não configurada.");
        }

        public void Adicionar(Curso curso)
        {
            var sql = @"INSERT INTO Curso (nome, nomeCoordenador, ativo)
                        VALUES (@nome, @coordenador, @ativo)";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nome", curso.Nome);
            cmd.Parameters.AddWithValue("@coordenador", curso.NomeCoordenador);
           
            cmd.Parameters.AddWithValue("@ativo", curso.ativo);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Atualizar(Curso curso)
        {
            var sql = @"UPDATE Curso SET 
                        nome = @nome,
                        nomeCoordenador = @coordenador,
                        ativo = @ativo
                        WHERE idCurso = @idCurso";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            
            cmd.Parameters.AddWithValue("@idCurso", curso.idCurso);
            cmd.Parameters.AddWithValue("@nome", curso.Nome);
            cmd.Parameters.AddWithValue("@coordenador", curso.NomeCoordenador);
            cmd.Parameters.AddWithValue("@ativo", curso.ativo);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Deletar(int idCurso)
        {
            var sql = @"DELETE FROM Curso WHERE idCurso = @idCurso";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idCurso", idCurso);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public Curso? ObterPorID(int idCurso)
        {
            var sql = @"SELECT idCurso, nome, nomeCoordenador, ativo 
                        FROM Curso WHERE idCurso = @idCurso";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idCurso", idCurso);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
               
                return new Curso(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetBoolean(3)
                );
            }
            return null;
        }

        public Curso? ObterPorNome(string nome)
        {
            var sql = @"SELECT idCurso, nome, nomeCoordenador, ativo 
                        FROM Curso WHERE nome = @nome";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nome", nome);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Curso(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetBoolean(3)
                );
            }
            return null;
        }

        public List<Curso> ObterTodos()
        {
            var lista = new List<Curso>();
            var sql = @"SELECT idCurso, nome, nomeCoordenador, ativo FROM Curso";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Curso(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetBoolean(3)
                ));
            }

            return lista;
        }
    }
}