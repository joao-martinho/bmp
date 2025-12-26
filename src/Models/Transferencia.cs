namespace Bmp.Models
{
    public class Transferencia
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
    }
}
