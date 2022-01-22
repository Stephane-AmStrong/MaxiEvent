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

namespace Application.Features.PaymentTypes.Queries.GetPaymentTypes
{
    public class GetPaymentTypesQuery : QueryStringParameters, IRequest<PagedList<GetPaymentTypesViewModel>>
    {
        public GetPaymentTypesQuery()
        {
            OrderBy = "name";
        }

        public string WithTheName { get; set; }
    }

    public class GetAllPaymentTypesQueryHandler : IRequestHandler<GetPaymentTypesQuery, PagedList<GetPaymentTypesViewModel>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetAllPaymentTypesQueryHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<PagedList<GetPaymentTypesViewModel>> Handle(GetPaymentTypesQuery query, CancellationToken cancellationToken)
        {
            var paymentTypes = await _repository.PaymentType.GetPagedListAsync(query);
            return  _mapper.Map<PagedList<GetPaymentTypesViewModel>>(paymentTypes);
        }
    }
}
