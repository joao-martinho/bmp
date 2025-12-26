namespace Bmp.Dtos
{
    public class TransferenciaRequest
    {
        public int ContaOrigemId { get; set; }
        public int ContaDestinoId { get; set; }
        public decimal Valor { get; set; }
    }
}
