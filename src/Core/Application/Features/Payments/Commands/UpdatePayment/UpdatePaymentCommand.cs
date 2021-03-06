using Application.Exceptions;
using Application.Features.Payments.Queries.GetPaymentById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommand : IRequest<GetPaymentViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusPayment { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public String AppUserId { get; set; }

        public Guid SponsorId { get; set; }


        public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, GetPaymentViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public UpdatePaymentCommandHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetPaymentViewModel> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken)
            {
                var paymentEntity = await _repository.Payment.GetByIdAsync(command.Id);

                if (paymentEntity == null)
                {
                    throw new ApiException($"Payment Not Found.");
                }

                _mapper.Map(command, paymentEntity);

                await _repository.Payment.UpdateAsync(paymentEntity);
                await _repository.SaveAsync();

                var paymentReadDto = _mapper.Map<GetPaymentViewModel>(paymentEntity);

                //if (!string.IsNullOrWhiteSpace(paymentReadDto.ImgLink)) paymentReadDto.ImgLink = $"{_baseURL}{paymentReadDto.ImgLink}";

                return paymentReadDto;


            }
        }
    }
}
