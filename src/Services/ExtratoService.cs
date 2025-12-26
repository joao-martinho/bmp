using Bmp.Dtos;
using Bmp.Models;

namespace Bmp.Services
{
    public class ExtratoService
    {
        public IEnumerable<ExtratoResponse> Gerar(
            IEnumerable<Transferencia> transferencias,
            int contaId,
            DateTime inicio,
            DateTime fim)
        {
            return transferencias
                .Where(l => l.ContaId == contaId && l.Data >= inicio && l.Data <= fim)
                .OrderBy(l => l.Data)
                .Select(l => new ExtratoResponse
                {
                    Data = l.Data,
                    Tipo = l.Tipo,
                    Valor = l.Valor
                });
        }
    }
}
