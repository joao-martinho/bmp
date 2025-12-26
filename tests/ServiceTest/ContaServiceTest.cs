using Xunit;
using Bmp.Services;
using Bmp.Models;

namespace Bmp.Tests.Services
{
    public class ContaServiceTests
    {
        private readonly ContaService service = new ContaService();

        [Fact]
        public void Criar_DeveCriarContaComDadosInformados()
        {
            var numero = "123";
            var agencia = "0001";
            var saldoInicial = 100m;

            var conta = service.Criar(numero, agencia, saldoInicial);

            Assert.NotNull(conta);
            Assert.Equal(numero, conta.Numero);
            Assert.Equal(agencia, conta.Agencia);
            Assert.Equal(saldoInicial, conta.Saldo);
        }

        [Fact]
        public void AtualizarSaldo_DeveIncrementarSaldoQuandoValorPositivo()
        {
            var conta = new Conta
            {
                Saldo = 100m
            };

            service.AtualizarSaldo(conta, 50m);

            Assert.Equal(150m, conta.Saldo);
        }

        [Fact]
        public void AtualizarSaldo_DeveDecrementarSaldoQuandoValorNegativo()
        {
            var conta = new Conta
            {
                Saldo = 100m
            };

            service.AtualizarSaldo(conta, -30m);

            Assert.Equal(70m, conta.Saldo);
        }
    }
}
