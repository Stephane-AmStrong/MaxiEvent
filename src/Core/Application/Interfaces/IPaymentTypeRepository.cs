using Application.Features.PaymentTypes.Commands.CreatePaymentType;
using Application.Features.PaymentTypes.Queries.GetPaymentTypes;
using Application.Parameters;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentTypeRepository
    {
        Task<PagedList<PaymentType>> GetPagedListAsync(GetPaymentTypesQuery paymentTypesQuery);

        Task<PaymentType> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(PaymentType paymentType);

        Task CreateAsync(PaymentType paymentType);
        Task UpdateAsync(PaymentType paymentType);
        Task UpdateAsync(IEnumerable<PaymentType> paymentTypes);
        Task DeleteAsync(PaymentType paymentType);
    }
}
