using System;
using System.Threading.Tasks;
using NET.Core.Base.Business.Models;

namespace NET.Core.Base.Business.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        Task<bool> Adicionar(Fornecedor fornecedor);

        Task<bool> Atualizar(Fornecedor fornecedor);

        Task<bool> Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}
