using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Bmp.Controllers;
using Bmp.Services;
using Bmp.Models;
using Bmp.Dtos;
using System.Reflection;

namespace Bmp.Tests.Controllers
{
    public class TransferenciaControllerTests
    {
        private TransferenciaController CriarController(
            out Mock<TransferenciaService> serviceMock)
        {
            serviceMock = new Mock<TransferenciaService>();
            return new TransferenciaController(serviceMock.Object);
        }

        private void PopularContas(params Conta[] contas)
        {
            var campo = typeof(TransferenciaController)
                .GetField("Contas", BindingFlags.Static | BindingFlags.NonPublic);

            var lista = (List<Conta>)campo.GetValue(null);
            lista.Clear();
            lista.AddRange(contas);
        }

        private void LimparTransferencias()
        {
            var campo = typeof(TransferenciaController)
                .GetField("transferencias", BindingFlags.Static | BindingFlags.NonPublic);

            var lista = (List<Transferencia>)campo.GetValue(null);
            lista.Clear();
        }

        [Fact]
        public void Transferir_DeveRetornarNotFound_QuandoContaNaoExiste()
        {
            LimparTransferencias();
            PopularContas();

            var controller = CriarController(out var serviceMock);

            var request = new TransferenciaRequest
            {
                ContaOrigemId = 1,
                ContaDestinoId = 2,
                Valor = 100
            };

            var resultado = controller.Transferir(request);

            Assert.IsType<NotFoundResult>(resultado);
        }

        [Fact]
        public void Transferir_DeveRetornarOk_QuandoTransferenciaValida()
        {
            LimparTransferencias();

            var contaOrigem = new Conta { Id = 1, Saldo = 500 };
            var contaDestino = new Conta { Id = 2, Saldo = 200 };

            PopularContas(contaOrigem, contaDestino);

            var controller = CriarController(out var serviceMock);

            var request = new TransferenciaRequest
            {
                ContaOrigemId = 1,
                ContaDestinoId = 2,
                Valor = 100
            };

            var resultado = controller.Transferir(request);

            Assert.IsType<OkResult>(resultado);

            serviceMock.Verify(
                s => s.Transferir(contaOrigem, contaDestino, 100),
                Times.Once);
        }

        [Fact]
        public void Transferir_DeveRegistrarDebitoECredito()
        {
            LimparTransferencias();

            var contaOrigem = new Conta { Id = 1, Saldo = 500 };
            var contaDestino = new Conta { Id = 2, Saldo = 200 };

            PopularContas(contaOrigem, contaDestino);

            var controller = CriarController(out var serviceMock);

            var request = new TransferenciaRequest
            {
                ContaOrigemId = 1,
                ContaDestinoId = 2,
                Valor = 100
            };

            controller.Transferir(request);

            var transferencias = TransferenciaController.ObterTransferencias();

            Assert.Equal(2, transferencias.Count);

            Assert.Contains(transferencias, t =>
                t.ContaId == 1 &&
                t.Tipo == "Debito" &&
                t.Valor == 100);

            Assert.Contains(transferencias, t =>
                t.ContaId == 2 &&
                t.Tipo == "Credito" &&
                t.Valor == 100);
        }
    }
}
