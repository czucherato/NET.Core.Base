using AutoMapper;
using NET.Core.Base.Mvc.ViewModels;
using NET.Core.Base.Business.Models;

namespace NET.Core.Base.Mvc.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
