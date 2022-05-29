using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using testeWipro.Application.Utils;
using testeWipro.Domain;
using testeWipro.Services;

namespace testeWipro.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        [HttpPost]
        [Route("Cadastrar-locador")]
        public async Task<IActionResult> CadastrarLocador(Cliente locador)
        {
            return new ResponseHelper().CreateResponse(await _clienteService.CadastrarLocador(locador));
        }

        [HttpPut]
        [Route("Ativar-OU-Desativar-Cliente")]
        public async Task<IActionResult> AtivarOuDesativarCliente(int clienteId)
        {
            return new ResponseHelper().CreateResponse(await _clienteService.AtivarOuDesativarCliente(clienteId));
        }
    }
}
