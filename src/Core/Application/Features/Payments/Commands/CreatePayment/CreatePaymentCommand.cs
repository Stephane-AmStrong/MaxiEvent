using Application.Features.Payments.Queries.GetPaymentById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.CreatePayment
{
    public partial class CreatePaymentCommand : IRequest<GetPaymentViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusPayment { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SponsorId { get; set; }
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, GetPaymentViewModel>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreatePaymentCommandHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetPaymentViewModel> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var paymentEntity = _mapper.Map<Payment>(command);

            await _repository.Payment.CreateAsync(paymentEntity);
            await _repository.SaveAsync();

            return _mapper.Map<GetPaymentViewModel>(paymentEntity);
        }
    }
}
