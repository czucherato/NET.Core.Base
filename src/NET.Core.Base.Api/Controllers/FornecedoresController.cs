using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NET.Core.Base.Api.Extensions;
using NET.Core.Base.Api.ViewModels;
using NET.Core.Base.Business.Models;
using NET.Core.Base.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace NET.Core.Base.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FornecedoresController : MainController
    {
        public FornecedoresController(
            IEnderecoRep enderecoRep,
            IFornecedorRep fornecedorRep,
            IFornecedorService fornecedorServ,
            IMapper mapper,
            INotificador notificador,
            IUser user)
            : base(user, notificador)
        {
            _enderecoRep = enderecoRep;
            _fornecedorRep = fornecedorRep;
            _fornecedorServ = fornecedorServ;
            _mapper = mapper;
        }

        private readonly IEnderecoRep _enderecoRep;
        private readonly IFornecedorRep _fornecedorRep;
        private readonly IFornecedorService _fornecedorServ;
        private readonly IMapper _mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
        {
            IEnumerable<FornecedorViewModel> fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRep.ObterTodos());
            return Ok(fornecedor);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            FornecedorViewModel fornecedor = await ObterFornecedorProdutosEndereco(id);
            if (fornecedor == null) return NotFound();
            return fornecedor;
        }

        [HttpPost]
        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel forncedorViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _fornecedorServ.Adicionar(_mapper.Map<Fornecedor>(forncedorViewModel));

            return CustomResponse(forncedorViewModel);
        }

        [HttpPut("{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel forncedorViewModel)
        {
            if (id != forncedorViewModel.Id) return BadRequest();
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorServ.Atualizar(_mapper.Map<Fornecedor>(forncedorViewModel));

            return CustomResponse(forncedorViewModel);
        }

        [HttpDelete("{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Remover")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            FornecedorViewModel forncedorViewModel = await ObterFornecedorEndereco(id);
            if (forncedorViewModel == null) return NotFound();
            await _fornecedorServ.Remover(id);

            return CustomResponse(id);
        }

        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<ActionResult<EnderecoViewModel>> ObterEnderecoPorId(Guid id)
        {
            return CustomResponse(_mapper.Map<EnderecoViewModel>(await _enderecoRep.ObterPorId(id)));
        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id) return BadRequest();
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorServ.AtualizarEndereco(_mapper.Map<Endereco>(enderecoViewModel));
            return CustomResponse(enderecoViewModel);
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRep.ObterFornecedorProdutosEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRep.ObterFornecedorEndereco(id));
        }
    }
}
