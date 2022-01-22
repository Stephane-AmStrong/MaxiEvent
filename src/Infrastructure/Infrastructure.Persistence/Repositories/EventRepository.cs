using Application.Features.Events.Commands.CreateEvent;
using Application.Features.Events.Queries.GetEvents;
using Application.Interfaces;
using Application.Parameters;
using Application.Wrappers;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        private ISortHelper<Event> _sortHelper;

        public EventRepository
        (
            RepositoryContext repositoryContext,
            ISortHelper<Event> sortHelper
        ) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<Event>> GetPagedListAsync(GetEventsQuery eventsQuery)
        {
            var events = Enumerable.Empty<Event>().AsQueryable();

            ApplyFilters(ref events, eventsQuery);

            PerformSearch(ref events, eventsQuery.SearchTerm);

            var sortedEvents = _sortHelper.ApplySort(events, eventsQuery.OrderBy);

            return await Task.Run(() =>
                PagedList<Event>.ToPagedList
                (
                    sortedEvents,
                    eventsQuery.PageNumber,
                    eventsQuery.PageSize)
                );
        }


        public async Task<Event> GetByIdAsync(Guid id)
        {
            return await BaseFindByCondition(_event => _event.Id.Equals(id))
                .FirstOrDefaultAsync();
        }


        public async Task<bool> ExistAsync(Event _event)
        {
            return await BaseFindByCondition(x => x.Name == _event.Name && x.Description == _event.Description)
                .AnyAsync();
        }

        public async Task CreateAsync(Event _event)
        {
            await BaseCreateAsync(_event);
        }

        public async Task UpdateAsync(Event _event)
        {
            await BaseUpdateAsync(_event);
        }

        public async Task UpdateAsync(IEnumerable<Event> events)
        {
            await BaseUpdateAsync(events);
        }

        public async Task DeleteAsync(Event _event)
        {
            await BaseDeleteAsync(_event);
        }

        private void ApplyFilters(ref IQueryable<Event> events, GetEventsQuery eventsQuery)
        {
            events = BaseFindAll();

            /*
            if (eventsQuery.MinCreateAt != null)
            {
                events = events.Where(x => x.CreateAt >= eventsQuery.MinCreateAt);
            }

            if (eventsQuery.MaxCreateAt != null)
            {
                events = events.Where(x => x.CreateAt < eventsQuery.MaxCreateAt);
            }
            */
        }

        private void PerformSearch(ref IQueryable<Event> events, string searchTerm)
        {
            if (!events.Any() || string.IsNullOrWhiteSpace(searchTerm)) return;

            events = events.Where(x => x.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
        }


    }
}
