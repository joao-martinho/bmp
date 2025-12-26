using Xunit;
using Bmp.Services;
using Bmp.Models;
using Bmp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bmp.Tests.Services
{
    public class ExtratoServiceTests
    {
        private readonly ExtratoService service = new ExtratoService();

        [Fact]
        public void Gerar_DeveFiltrarPorContaId()
        {
            var transferencias = new List<Transferencia>
            {
                new Transferencia
                {
                    ContaId = 1,
                    Data = new DateTime(2024, 1, 10),
                    Tipo = "Credito",
                    Valor = 100
                },
                new Transferencia
                {
                    ContaId = 2,
                    Data = new DateTime(2024, 1, 10),
                    Tipo = "Debito",
                    Valor = 50
                }
            };

            var resultado = service.Gerar(
                transferencias,
                contaId: 1,
                inicio: new DateTime(2024, 1, 1),
                fim: new DateTime(2024, 1, 31))
                .ToList();

            Assert.Single(resultado);
            Assert.Equal(1, transferencias.First().ContaId);
        }

        [Fact]
        public void Gerar_DeveFiltrarPorPeriodo()
        {
            var transferencias = new List<Transferencia>
            {
                new Transferencia
                {
                    ContaId = 1,
                    Data = new DateTime(2024, 1, 5),
                    Tipo = "Credito",
                    Valor = 100
                },
                new Transferencia
                {
                    ContaId = 1,
                    Data = new DateTime(2024, 2, 1),
                    Tipo = "Debito",
                    Valor = 50
                }
            };

            var resultado = service.Gerar(
                transferencias,
                contaId: 1,
                inicio: new DateTime(2024, 1, 1),
                fim: new DateTime(2024, 1, 31))
                .ToList();

            Assert.Single(resultado);
            Assert.Equal(new DateTime(2024, 1, 5), resultado[0].Data);
        }

        [Fact]
        public void Gerar_DeveOrdenarPorDataCrescente()
        {
            var transferencias = new List<Transferencia>
            {
                new Transferencia
                {
                    ContaId = 1,
                    Data = new DateTime(2024, 1, 20),
                    Tipo = "Debito",
                    Valor = 30
                },
                new Transferencia
                {
                    ContaId = 1,
                    Data = new DateTime(2024, 1, 10),
                    Tipo = "Credito",
                    Valor = 100
                }
            };

            var resultado = service.Gerar(
                transferencias,
                contaId: 1,
                inicio: new DateTime(2024, 1, 1),
                fim: new DateTime(2024, 1, 31))
                .ToList();

            Assert.Equal(
                new DateTime(2024, 1, 10),
                resultado[0].Data);

            Assert.Equal(
                new DateTime(2024, 1, 20),
                resultado[1].Data);
        }

        [Fact]
        public void Gerar_DeveMapearParaExtratoResponse()
        {
            var transferencia = new Transferencia
            {
                ContaId = 1,
                Data = new DateTime(2024, 1, 15),
                Tipo = "Credito",
                Valor = 200
            };

            var resultado = service.Gerar(
                new[] { transferencia },
                contaId: 1,
                inicio: new DateTime(2024, 1, 1),
                fim: new DateTime(2024, 1, 31))
                .Single();

            Assert.Equal(transferencia.Data, resultado.Data);
            Assert.Equal(transferencia.Tipo, resultado.Tipo);
            Assert.Equal(transferencia.Valor, resultado.Valor);
        }

        [Fact]
        public void Gerar_DeveRetornarVazioQuandoNaoHaResultados()
        {
            var transferencias = new List<Transferencia>();

            var resultado = service.Gerar(
                transferencias,
                contaId: 1,
                inicio: new DateTime(2024, 1, 1),
                fim: new DateTime(2024, 1, 31));

            Assert.Empty(resultado);
        }
    }
}
