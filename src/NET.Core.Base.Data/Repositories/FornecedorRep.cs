using System;
using System.Threading.Tasks;
using NET.Core.Base.Data.Context;
using NET.Core.Base.Business.Models;
using Microsoft.EntityFrameworkCore;
using NET.Core.Base.Business.Interfaces;

namespace NET.Core.Base.Data.Repositories
{
    public class FornecedorRep : Repository<Fornecedor>, IFornecedorRep
    {
        public FornecedorRep(NetCoreBaseContext db)
            : base(db) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(e => e.Endereco).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(p => p.Produtos)
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
