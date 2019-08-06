using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NET.Core.Base.Business.Models;

namespace NET.Core.Base.Business.Interfaces
{
    public interface IProdutoRep : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);

        Task<IEnumerable<Produto>> ObterProdutosFornecedores();

        Task<Produto> ObterProdutoFornecedor(Guid id);
    }
}
