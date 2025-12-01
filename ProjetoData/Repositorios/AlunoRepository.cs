using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Projeto.Data.Repositorios //
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly string _connectionString;

        public AlunoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string não pode ser nula.");
        }

        public void Adicionar(Aluno aluno)
        {
            var sql = "INSERT INTO Aluno (cpf,nome,email,matricula) VALUES (@cpf, @nome, @email, @matricula)";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@cpf", aluno.CPF);
            cmd.Parameters.AddWithValue("@nome", aluno.Nome);
            cmd.Parameters.AddWithValue("@email", aluno.email);
            cmd.Parameters.AddWithValue("@matricula", aluno.matricula);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Atualizar(Aluno aluno)
        {
            var sql = "UPDATE Aluno SET nome = @nome, cpf = @cpf, matricula = @matricula, email = @email WHERE idAluno = @idAluno";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idAluno", aluno.IDAluno);
            cmd.Parameters.AddWithValue("@nome", aluno.Nome);
            cmd.Parameters.AddWithValue("@cpf", aluno.CPF);
            cmd.Parameters.AddWithValue("@matricula", aluno.matricula);
            cmd.Parameters.AddWithValue("@email", aluno.email);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Deletar(int idAluno)
        {
            var sql = "DELETE FROM Aluno WHERE idAluno = @idAluno";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idAluno", idAluno);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public Aluno? ObterPorId(int idAluno)
        {
            var sql = "SELECT idAluno, nome, cpf, matricula, email FROM Aluno WHERE idAluno = @idAluno";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idAluno", idAluno);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Aluno(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                );
            }
            return null;
        }

        public Aluno? ObterPorcpf(string cpf)
        {
            var sql = "SELECT idAluno, nome, cpf, matricula, email FROM Aluno WHERE cpf = @cpf";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@cpf", cpf);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Aluno(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                );
            }
            return null;
        }

        public Aluno? ObterPorMAtricula(string matricula)
        {
            var sql = "SELECT idAluno, nome, cpf, matricula, email FROM Aluno WHERE matricula = @matricula";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@matricula", matricula);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Aluno(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                );
            }
            return null;
        }

        public List<Aluno> ObterTodos()
        {
            var lista = new List<Aluno>();
            var sql = "SELECT idAluno, nome, cpf, matricula, email FROM Aluno";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var aluno = new Aluno(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                );
                lista.Add(aluno);
            }
            return lista;
        }

        public Aluno? ObterPorID(int IDAluno)
        {
            throw new NotImplementedException();
        }
    }
}
