using System;
using System.Threading.Tasks;
using NET.Core.Base.Business.Models;

namespace NET.Core.Base.Business.Interfaces
{
    public interface IFornecedorRep : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);

        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
    }
}
