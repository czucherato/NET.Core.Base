using System;
using System.Threading.Tasks;
using NET.Core.Base.Data.Context;
using NET.Core.Base.Business.Models;
using Microsoft.EntityFrameworkCore;
using NET.Core.Base.Business.Interfaces;

namespace NET.Core.Base.Data.Repositories
{
    public class EnderecoRep : Repository<Endereco>, IEnderecoRep
    {
        public EnderecoRep(NetCoreBaseContext db)
            : base(db) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .Include(f => f.Fornecedor).FirstOrDefaultAsync(e => e.FornecedorId == fornecedorId);
        }
    }
}
