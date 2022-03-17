using AutoMapper;
using MediatR;
using Onyx.Contracts.Data;
using Onyx.Contracts.DTO;

namespace Onyx.Core.Handlers.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Products.GetAll());

            return _mapper.Map<IEnumerable<ProductDTO>>(entities);
        }
    }
}