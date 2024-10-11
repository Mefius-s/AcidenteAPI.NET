using Fiap.Api.Acidentes.Services;
using Microsoft.AspNetCore.Mvc;
using Fiap.Api.Acidentes.ViewModel;
using Fiap.Api.Acidentes.Models;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Fiap.Api.Acidentes.Data.Contexts;
using Fiap.Api.Acidentes.Exception;

namespace Fiap.Api.Acidentes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AcidenteController : ControllerBase
    {
        private readonly IAcidenteService _service;
        private readonly IMapper _mapper;

        public AcidenteController(IAcidenteService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        //----------------------------LISTAR ACIDENTES PAGINADO---------------------------------------//
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<AcidentePaginacaoReferenciaViewModel> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var acidentes = _service.ListarAcidentesReferencia(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<AcidenteViewModel>>(acidentes);

            if (!viewModelList.Any())
            {
                return NoContent();
            }

            var viewModel = new AcidentePaginacaoReferenciaViewModel
            {
                Acidentes = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().Id
            };

            return Ok(viewModel);
        }


        //----------------------------ACIDENTE POR ID---------------------------------------//
        [HttpGet("{id}")]
        [Authorize(Roles = "analista, gerente")]
        public ActionResult<AcidenteViewModel> Get([FromRoute] long id)
        {
            try
            {
                var acidente = _service.ObterAcidentePorId(id);
                if (acidente == null)
                    return NotFound();

                var viewModel = _mapper.Map<AcidenteViewModel>(acidente);
                return Ok(viewModel);
            }
            catch (NotFoundException ex)
            {
                // Exceção de recurso não encontrado (404)
                return NotFound($"Acidente não encontrado com ID {id}");
            }
            catch (ApplicationException ex)
            {
                // Outras exceções genéricas (500)
                return StatusCode(500, $"Erro interno ao processar a solicitação: {ex.Message}");
            }

        }


        //----------------------------CRIAR ACIDENTE---------------------------------------//
        [HttpPost]
        [Authorize(Roles = "analista, gerente")]
        public ActionResult Post([FromBody] AcidenteViewModel viewModel)
        {
            var acidente = _mapper.Map<AcidenteModel>(viewModel);
            _service.Criar(acidente);
            return CreatedAtAction(nameof(Get), new { id = acidente.Id }, acidente);
        }


        //----------------------------ATUALIZAR ACIDENTE---------------------------------------//
        [HttpPut("{id}")]
        [Authorize(Roles = "gerente")]
        public ActionResult Put(long id, [FromBody] AcidenteViewModel viewModel)
        {
            var acidenteExiste = _service.ObterAcidentePorId(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            _mapper.Map(viewModel, acidenteExiste);
            _service.Atualizar(acidenteExiste);
            return NoContent();
        }


        //----------------------------EXCLUIR ACIDENTE---------------------------------------//
        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public ActionResult Delete(long id)
        {
            _service.Excluir(id);
            return NoContent();
        }
    }
}
