using Application.Features.PaymentTypes.Queries.GetPaymentTypeById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PaymentTypes.Commands.CreatePaymentType
{
    public partial class CreatePaymentTypeCommand : IRequest<GetPaymentTypeViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusPaymentType { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SponsorId { get; set; }
    }

    public class CreatePaymentTypeCommandHandler : IRequestHandler<CreatePaymentTypeCommand, GetPaymentTypeViewModel>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreatePaymentTypeCommandHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetPaymentTypeViewModel> Handle(CreatePaymentTypeCommand command, CancellationToken cancellationToken)
        {
            var paymentTypeEntity = _mapper.Map<PaymentType>(command);

            await _repository.PaymentType.CreateAsync(paymentTypeEntity);
            await _repository.SaveAsync();

            return _mapper.Map<GetPaymentTypeViewModel>(paymentTypeEntity);
        }
    }
}
