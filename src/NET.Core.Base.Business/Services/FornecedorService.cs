using System;
using System.Linq;
using System.Threading.Tasks;
using NET.Core.Base.Business.Models;
using NET.Core.Base.Business.Interfaces;
using NET.Core.Base.Business.Validations;

namespace NET.Core.Base.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        public FornecedorService(
            IFornecedorRep fornecedorRep,
            IEnderecoRep enderecoRep,
            INotificador notificador)
            : base(notificador)
        {
            _fornecedorRep = fornecedorRep;
            _enderecoRep = enderecoRep;
        }

        private readonly IFornecedorRep _fornecedorRep;
        private readonly IEnderecoRep _enderecoRep;

        public async Task<bool> Adicionar(Fornecedor fornecedor)
        {
            // Validar o estado da entidade!
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                && !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return false;

            if (_fornecedorRep.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return false;
            }

            await _fornecedorRep.Adicionar(fornecedor);
            return true;
        }

        public async Task<bool> Atualizar(Fornecedor fornecedor)
        {
            if (_fornecedorRep.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return false;
            }

            await _fornecedorRep.Atualizar(fornecedor);
            return true;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRep.Atualizar(endereco);
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_fornecedorRep.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadatrados!");
                return false;
            }

            await _fornecedorRep.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _fornecedorRep?.Dispose();
            _enderecoRep?.Dispose();
        }
    }
}
