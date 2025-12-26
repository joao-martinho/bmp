using Microsoft.AspNetCore.Mvc;
using Bmp.Models;
using Bmp.Dtos;
using Bmp.Services;

namespace Bmp.Controllers
{
    [ApiController]
    [Route("contas")]
    public class ContaController : ControllerBase
    {
        private static readonly List<Conta> Contas = new();
        private readonly ContaService service;

        public ContaController(ContaService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Criar(CriarContaRequest request)
        {
            var conta = service.Criar(request.Numero, request.Agencia, request.SaldoInicial);
            conta.Id = Contas.Count + 1;
            Contas.Add(conta);
            return Ok(conta);
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var conta = Contas.Find(c => c.Id == id);
            if (conta == null)
                return NotFound();
            return Ok(conta);
        }
    }
}
