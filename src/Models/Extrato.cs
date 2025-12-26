namespace Bmp.Models
{
    public class Extrato
    {
        public int ContaId { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public IEnumerable<Transferencia> Movimentacoes { get; set; }
    }
}
