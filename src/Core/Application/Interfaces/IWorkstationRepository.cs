using Application.Features.Workstations.Commands.CreateWorkstation;
using Application.Features.Workstations.Queries.GetWorkstations;
using Application.Parameters;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWorkstationRepository
    {
        Task<PagedList<Workstation>> GetPagedListAsync(GetWorkstationsQuery workstationsQuery);

        Task<Workstation> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(Workstation workstation);

        Task CreateAsync(Workstation workstation);
        Task UpdateAsync(Workstation workstation);
        Task UpdateAsync(IEnumerable<Workstation> workstations);
        Task DeleteAsync(Workstation workstation);
    }
}
