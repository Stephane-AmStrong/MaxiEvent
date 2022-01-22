using Application.Features.Places.Commands.CreatePlace;
using Application.Features.Places.Queries.GetPlaces;
using Application.Parameters;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlaceRepository
    {
        Task<PagedList<Place>> GetPagedListAsync(GetPlacesQuery placesQuery);

        Task<Place> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(Place place);

        Task CreateAsync(Place place);
        Task UpdateAsync(Place place);
        Task UpdateAsync(IEnumerable<Place> places);
        Task DeleteAsync(Place place);
    }
}
