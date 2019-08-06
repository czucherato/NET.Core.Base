using System;
using System.Threading.Tasks;
using NET.Core.Base.Business.Models;
using NET.Core.Base.Business.Interfaces;
using NET.Core.Base.Business.Validations;

namespace NET.Core.Base.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        public ProdutoService(
            IProdutoRep produtoRep,
            INotificador notificador)
            : base(notificador)
        {
            _produtoRep = produtoRep;
        }

        private readonly IProdutoRep _produtoRep;

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRep.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRep.Atualizar(produto);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRep.Remover(id);
        }

        public void Dispose()
        {
            _produtoRep?.Dispose();
        }
    }
}
