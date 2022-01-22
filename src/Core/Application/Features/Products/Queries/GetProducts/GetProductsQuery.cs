using Application.Interfaces;
using Application.Parameters;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQuery : QueryStringParameters, IRequest<PagedList<GetProductsViewModel>>
    {
        public GetProductsQuery()
        {
            OrderBy = "name";
        }

        public string WithTheName { get; set; }
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedList<GetProductsViewModel>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<PagedList<GetProductsViewModel>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _repository.Product.GetProductsAsync(query);
            return  _mapper.Map<PagedList<GetProductsViewModel>>(products);
        }
    }
}
