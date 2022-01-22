using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PaymentTypes.Queries.GetPaymentTypeById
{
    public class GetPaymentTypeByIdQuery : IRequest<GetPaymentTypeViewModel>
    {
        public Guid Id { get; set; }

        public class GetPaymentTypeByIdQueryHandler : IRequestHandler<GetPaymentTypeByIdQuery, GetPaymentTypeViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public GetPaymentTypeByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetPaymentTypeViewModel> Handle(GetPaymentTypeByIdQuery query, CancellationToken cancellationToken)
            {
                var paymentType = await _repository.PaymentType.GetByIdAsync(query.Id);
                if (paymentType == null) throw new ApiException($"PaymentType Not Found.");
                return _mapper.Map<GetPaymentTypeViewModel>(paymentType);
            }
        }
    }
}
