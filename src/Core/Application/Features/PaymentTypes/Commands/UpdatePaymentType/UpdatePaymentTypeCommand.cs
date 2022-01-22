using Application.Exceptions;
using Application.Features.PaymentTypes.Queries.GetPaymentTypeById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PaymentTypes.Commands.UpdatePaymentType
{
    public class UpdatePaymentTypeCommand : IRequest<GetPaymentTypeViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusPaymentType { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public String AppUserId { get; set; }

        public Guid SponsorId { get; set; }


        public class UpdatePaymentTypeCommandHandler : IRequestHandler<UpdatePaymentTypeCommand, GetPaymentTypeViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public UpdatePaymentTypeCommandHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetPaymentTypeViewModel> Handle(UpdatePaymentTypeCommand command, CancellationToken cancellationToken)
            {
                var paymentTypeEntity = await _repository.PaymentType.GetByIdAsync(command.Id);

                if (paymentTypeEntity == null)
                {
                    throw new ApiException($"PaymentType Not Found.");
                }

                _mapper.Map(command, paymentTypeEntity);

                await _repository.PaymentType.UpdateAsync(paymentTypeEntity);
                await _repository.SaveAsync();

                var paymentTypeReadDto = _mapper.Map<GetPaymentTypeViewModel>(paymentTypeEntity);

                //if (!string.IsNullOrWhiteSpace(paymentTypeReadDto.ImgLink)) paymentTypeReadDto.ImgLink = $"{_baseURL}{paymentTypeReadDto.ImgLink}";

                return paymentTypeReadDto;


            }
        }
    }
}
