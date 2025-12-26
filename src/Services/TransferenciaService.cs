using Bmp.Models;

namespace Bmp.Services
{
    public class TransferenciaService
    {
        private readonly CalendarioService _calendario;

        public TransferenciaService(CalendarioService calendario)
        {
            _calendario = calendario;
        }

        public void Transferir(Conta origem, Conta destino, decimal valor)
        {
            if (!_calendario.EhDiaUtil(DateTime.Today))
                throw new InvalidOperationException();

            if (origem.Saldo < valor)
                throw new InvalidOperationException();

            origem.Saldo -= valor;
            destino.Saldo += valor;
        }
    }
}
