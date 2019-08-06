using System;
using System.Threading.Tasks;
using NET.Core.Base.Business.Models;

namespace NET.Core.Base.Business.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);

        Task Atualizar(Produto produto);

        Task Remover(Guid id);
    }
}
