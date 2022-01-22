using Application.Features.Events.Commands.CreateEvent;
using Application.Features.Events.Queries.GetEvents;
using Application.Parameters;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEventRepository
    {
        Task<PagedList<Event>> GetPagedListAsync(GetEventsQuery eventsQuery);

        Task<Event> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(Event _event);

        Task CreateAsync(Event _event);
        Task UpdateAsync(Event _event);
        Task UpdateAsync(IEnumerable<Event> events);
        Task DeleteAsync(Event _event);
    }
}
