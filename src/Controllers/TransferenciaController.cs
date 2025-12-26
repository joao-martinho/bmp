using Microsoft.AspNetCore.Mvc;
using Bmp.Models;
using Bmp.Services;
using Bmp.Dtos;

namespace Bmp.Controllers
{
    [ApiController]
    [Route("transferencias")]
    public class TransferenciaController : ControllerBase
    {
        private static readonly List<Conta> Contas = new();
        private static readonly List<Transferencia> transferencias = new();
        private readonly TransferenciaService service;

        public TransferenciaController(TransferenciaService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Transferir(TransferenciaRequest request)
        {
            var origem = Contas.Find(c => c.Id == request.ContaOrigemId);
            var destino = Contas.Find(c => c.Id == request.ContaDestinoId);

            if (origem == null || destino == null)
                return NotFound();

            service.Transferir(origem, destino, request.Valor);

            transferencias.Add(new Transferencia
            {
                ContaId = origem.Id,
                Tipo = "Debito",
                Valor = request.Valor,
                Data = DateTime.Now
            });

            transferencias.Add(new Transferencia
            {
                ContaId = destino.Id,
                Tipo = "Credito",
                Valor = request.Valor,
                Data = DateTime.Now
            });

            return Ok();
        }

        public static List<Transferencia> ObterTransferencias()
        {
            return transferencias;
        }
    }
}
