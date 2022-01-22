using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<GetProductViewModel>
    {
        public Guid Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetProductViewModel> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _repository.Product.GetProductByIdAsync(query.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                return _mapper.Map<GetProductViewModel>(product);
            }
        }
    }
}
