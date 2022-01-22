using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepositoryWrapper
    {
        //IFileRepository File { get; }

        //IAccountRepository Account { get; }
        //IAppUserRepository AppUser { get; }
        //IMailRepository Mail { get; }

        IProductRepository Product { get; }

        string Path { set; }

        Task SaveAsync();
    }
}
