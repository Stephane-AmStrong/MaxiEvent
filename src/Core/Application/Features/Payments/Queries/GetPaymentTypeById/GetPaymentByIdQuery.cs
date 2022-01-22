using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Payments.Queries.GetPaymentById
{
    public class GetPaymentByIdQuery : IRequest<GetPaymentViewModel>
    {
        public Guid Id { get; set; }

        public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, GetPaymentViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public GetPaymentByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetPaymentViewModel> Handle(GetPaymentByIdQuery query, CancellationToken cancellationToken)
            {
                var payment = await _repository.Payment.GetByIdAsync(query.Id);
                if (payment == null) throw new ApiException($"Payment Not Found.");
                return _mapper.Map<GetPaymentViewModel>(payment);
            }
        }
    }
}
