using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using testeWipro.Application.Utils;
using testeWipro.Domain;
using testeWipro.Services;
using static testeWipro.Models.FilmeModels;

namespace testeWipro.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmeController
    {
        private readonly FilmeService _filmeService;
        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        [Route("Cadastrar-Filme")]
        public async Task<IActionResult> CadastrarFilme(Filme filme)
        {
            return new ResponseHelper().CreateResponse(await _filmeService.CadastrarFilme(filme));
        }

        [HttpPost]
        [Route("Locar-Filme")]
        public async Task<IActionResult> LocarFilme(FilmeModelSomenteIds model)
        {
            return new ResponseHelper().CreateResponse(await _filmeService.LocarFilme(model));
        }

        [HttpPut]
        [Route("Devolver-Filme")]
        public async Task<IActionResult> DevolverFilme(FilmeModelSomenteIds model)
        {
            return new ResponseHelper().CreateResponse(await _filmeService.DevolverFilme(model));
        }

        [HttpPut]
        [Route("Ativar-OU-Desativar-Filme")]
        public async Task<IActionResult> AtivarOuDesativarFilme(int filmeId)
        {
            return new ResponseHelper().CreateResponse(await _filmeService.AtivarOuDesativarFilme(filmeId));
        }
    }
}
