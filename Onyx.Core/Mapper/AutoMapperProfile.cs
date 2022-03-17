using AutoMapper;
using Onyx.Contracts.Data.Entities;
using Onyx.Contracts.DTO;

namespace Onyx.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}
