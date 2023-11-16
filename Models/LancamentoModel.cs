namespace Projeto.Models
{
    public class LancamentoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set;}
        public int Valor { get; set; }
        public Boolean Avulso { get; set; }
        public string Status { get; set; }
    }
}
