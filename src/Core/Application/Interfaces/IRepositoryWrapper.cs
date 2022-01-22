using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepositoryWrapper
    {
        IFileService FileService { get; }

        IAccountService AccountService { get; }
        IEmailService EmailService { get; }

        ICategoryRepository Category { get; }
        IEventRepository Event { get; }
        IOrderRepository Order { get; }
        IPaymentRepository Payment { get; }
        IPaymentTypeRepository PaymentType { get; }
        IPlaceRepository Place { get; }
        IWorkstationRepository Workstation { get; }

        string Path { set; }

        Task SaveAsync();
    }
}
