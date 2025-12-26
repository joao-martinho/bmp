using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Bmp.Controllers;
using Bmp.Services;

namespace Bmp.Tests.Controllers
{
    public class ExtratoControllerTests
    {
        private ExtratoController CriarController(
            out Mock<ExtratoService> serviceMock)
        {
            serviceMock = new Mock<ExtratoService>();
            return new ExtratoController(serviceMock.Object);
        }

        [Fact]
        public void Gerar_DeveRetornarOkComExtrato()
        {
            var controller = CriarController(out var serviceMock);

            var contaId = 1;
            var inicio = new DateTime(2024, 1, 1);
            var fim = new DateTime(2024, 1, 31);

            var extratoEsperado = new object();

            serviceMock
                .Setup(s => s.Gerar(
                    It.IsAny<IEnumerable<object>>(),
                    contaId,
                    inicio,
                    fim))
                .Returns(extratoEsperado);

            var resultado = controller.Gerar(contaId, inicio, fim);

            var ok = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal(extratoEsperado, ok.Value);
        }
    }
}
