using Bmp.Models;

namespace Bmp.Services
{
    public class ContaService
    {
        public Conta Criar(string numero, string agencia, decimal saldoInicial)
        {
            return new Conta
            {
                Numero = numero,
                Agencia = agencia,
                Saldo = saldoInicial
            };
        }

        public void AtualizarSaldo(Conta conta, decimal valor)
        {
            conta.Saldo += valor;
        }
    }
}
