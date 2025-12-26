using Xunit;
using Bmp.Services;
using System;

namespace Bmp.Tests.Services
{
    public class CalendarioServiceTests
    {
        private readonly CalendarioService service = new CalendarioService();

        [Fact]
        public void EhDiaUtil_DeveRetornarFalse_ParaSabado()
        {
            var data = new DateTime(2024, 6, 1);

            var resultado = service.EhDiaUtil(data);

            Assert.False(resultado);
        }

        [Fact]
        public void EhDiaUtil_DeveRetornarFalse_ParaDomingo()
        {
            var data = new DateTime(2024, 6, 2);

            var resultado = service.EhDiaUtil(data);

            Assert.False(resultado);
        }

        [Fact]
        public void EhDiaUtil_DeveRetornarTrue_ParaDiaDeSemana()
        {
            var data = new DateTime(2024, 6, 3);

            var resultado = service.EhDiaUtil(data);

            Assert.True(resultado);
        }
    }
}
