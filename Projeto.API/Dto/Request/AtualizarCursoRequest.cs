namespace Projeto.API.Dto.Request
{
    public class AtualizarCursoRequest
    {
        public int cursoID { get; set; }
        public string nome { get; set; }
        public string nomeCoordenador { get; set; }
        public double cargaHoraria { get; set; }
        public bool ativo { get; set; }
    }
}
