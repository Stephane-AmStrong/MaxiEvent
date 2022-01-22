using Application.Features.Products.Queries.GetProductById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.CreateProduct
{
    public partial class CreateProductCommand : IRequest<GetProductViewModel>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GetProductViewModel>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreateProductCommandHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetProductViewModel> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(command);

            await _repository.Product.CreateProductAsync(productEntity);
            await _repository.SaveAsync();

            return _mapper.Map<GetProductViewModel>(productEntity);
        }
    }
}
