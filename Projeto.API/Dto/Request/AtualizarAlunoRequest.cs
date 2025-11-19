namespace Projeto.API.Dto.Request
{
    public class AtualizarAlunoRequest
    {
        public int IDAluno { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string matricula { get; set; }
        public string email { get; set; }
    }
}
