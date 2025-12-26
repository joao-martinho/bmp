using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Bmp.Controllers;
using Bmp.Services;
using Bmp.Models;
using Bmp.Dtos;

namespace Bmp.Tests.Controllers
{
    public class ContaControllerTests
    {
        private ContaController CriarController(out Mock<ContaService> serviceMock)
        {
            serviceMock = new Mock<ContaService>();
            return new ContaController(serviceMock.Object);
        }

        [Fact]
        public void Criar_DeveRetornarOkComContaCriada()
        {
            var controller = CriarController(out var serviceMock);

            var request = new CriarContaRequest
            {
                Numero = "123",
                Agencia = "0001",
                SaldoInicial = 100
            };

            var conta = new Conta
            {
                Numero = request.Numero,
                Agencia = request.Agencia,
                Saldo = request.SaldoInicial
            };

            serviceMock
                .Setup(s => s.Criar(request.Numero, request.Agencia, request.SaldoInicial))
                .Returns(conta);

            var resultado = controller.Criar(request);

            var ok = Assert.IsType<OkObjectResult>(resultado);
            var contaRetornada = Assert.IsType<Conta>(ok.Value);

            Assert.Equal("123", contaRetornada.Numero);
            Assert.Equal("0001", contaRetornada.Agencia);
            Assert.Equal(100, contaRetornada.Saldo);
            Assert.True(contaRetornada.Id > 0);
        }

        [Fact]
        public void Obter_DeveRetornarOkQuandoContaExiste()
        {
            var controller = CriarController(out var serviceMock);

            var conta = new Conta
            {
                Id = 1,
                Numero = "123",
                Agencia = "0001",
                Saldo = 100
            };

            controller.Criar(new CriarContaRequest
            {
                Numero = conta.Numero,
                Agencia = conta.Agencia,
                SaldoInicial = conta.Saldo
            });

            var resultado = controller.Obter(1);

            var ok = Assert.IsType<OkObjectResult>(resultado);
            var contaRetornada = Assert.IsType<Conta>(ok.Value);

            Assert.Equal(1, contaRetornada.Id);
        }

        [Fact]
        public void Obter_DeveRetornarNotFoundQuandoContaNaoExiste()
        {
            var controller = CriarController(out var serviceMock);

            var resultado = controller.Obter(999);

            Assert.IsType<NotFoundResult>(resultado);
        }
    }
}
