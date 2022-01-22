using Application.Exceptions;
using Application.Features.Products.Queries.GetProductById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<GetProductViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GetProductViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public UpdateProductCommandHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetProductViewModel> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var productEntity = await _repository.Product.GetProductByIdAsync(command.Id);

                if (productEntity == null)
                {
                    throw new ApiException($"Product Not Found.");
                }

                _mapper.Map(command, productEntity);

                await _repository.Product.UpdateProductAsync(productEntity);
                await _repository.SaveAsync();

                var productReadDto = _mapper.Map<GetProductViewModel>(productEntity);

                //if (!string.IsNullOrWhiteSpace(productReadDto.ImgLink)) productReadDto.ImgLink = $"{_baseURL}{productReadDto.ImgLink}";

                return productReadDto;


            }
        }
    }
}
