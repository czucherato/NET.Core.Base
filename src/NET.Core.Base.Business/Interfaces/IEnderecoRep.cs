using System;
using System.Threading.Tasks;
using NET.Core.Base.Business.Models;

namespace NET.Core.Base.Business.Interfaces
{
    public interface IEnderecoRep : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
