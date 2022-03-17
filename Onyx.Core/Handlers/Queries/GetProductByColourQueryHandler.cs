using MediatR;
using AutoMapper;
using Onyx.Contracts.Data;
using Onyx.Contracts.DTO;
using Onyx.Core.Exceptions;

namespace Onyx.Core.Handlers.Queries
{
    public class GetProductByColourQuery : IRequest<IEnumerable<ProductDTO>>
    {
        public string Colour { get; }
        public GetProductByColourQuery(string colour)
        {
            Colour = colour;
        }
    }

    public class GetProductByColourQueryHandler : IRequestHandler<GetProductByColourQuery, IEnumerable<ProductDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetProductByColourQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetProductByColourQuery request, CancellationToken cancellationToken)
        {
            var product = await Task.FromResult(_repository.Products.Get(r => r.ProductColour == request.Colour));

            if (product == null || product.Count() == 0)
            {
                throw new EntityNotFoundException($"No product found for rolor {request.Colour}");
            }

            return _mapper.Map<IEnumerable<ProductDTO>>(product);
        }
    }
}