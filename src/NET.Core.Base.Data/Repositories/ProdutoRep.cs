using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using NET.Core.Base.Data.Context;
using NET.Core.Base.Business.Models;
using Microsoft.EntityFrameworkCore;
using NET.Core.Base.Business.Interfaces;

namespace NET.Core.Base.Data.Repositories
{
    public class ProdutoRep : Repository<Produto>, IProdutoRep
    {
        public ProdutoRep(NetCoreBaseContext db)
            : base(db) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Include(f => f.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await Db.Produtos.AsNoTracking()
                .Include(f => f.Fornecedor).OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
