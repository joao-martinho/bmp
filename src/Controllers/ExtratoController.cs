using Microsoft.AspNetCore.Mvc;
using Bmp.Services;

namespace Bmp.Controllers
{
    [ApiController]
    [Route("extrato")]
    public class ExtratoController : ControllerBase
    {
        private readonly ExtratoService service;

        public ExtratoController(ExtratoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Gerar(
            int contaId,
            DateTime inicio,
            DateTime fim)
        {
            var transferencias = TransferenciaController.ObterTransferencias();
            var extrato = service.Gerar(transferencias, contaId, inicio, fim);
            return Ok(extrato);
        }
    }
}
