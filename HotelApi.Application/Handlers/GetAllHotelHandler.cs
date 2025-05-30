using HotelApi.Application.Mappers;
using HotelApi.Application.Queries;
using HotelApi.Application.Responses;
using HotelApi.Domain.Repositories;
using MediatR;

namespace HotelApi.Application.Handlers
{
    // Handler class implementing MediatR's IRequestHandler to process GetAllHotelQuery and return a list of HotelDetailsResponse
    /// <summary>
    /// Constructor with dependency injection for the IHotelRepository
    /// </summary>
    /// <param name="hotelRepository">add hotel repository as dependency</param>
    public class GetAllHotelHandler(IHotelRepository hotelRepository) : IRequestHandler<GetAllHotelQuery, IList<HotelDetailsResponse>>
    {
        // Private readonly field to hold the hotel repository instance
        private readonly IHotelRepository _hotelRepository = hotelRepository;

        /// <summary>
        /// Handles the GetAllHotelQuery to retrieve a paginated list of hotel details.
        /// </summary>
        /// <param name="request">The query containing pagination parameters: PageNumber and PageSize.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of HotelDetailsResponse objects.</returns>
        public async Task<IList<HotelDetailsResponse>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
        {
            // Retrieve paginated hotel details from the repository
            var hotelDetail = await _hotelRepository.GetHotelsAsync(request.PageNumber, request.PageSize, request.FilterText, request.MinRating, request.SortOption!, cancellationToken);
            // Map the domain hotel models to response DTOs using AutoMapper
            var response = HotelMapper.Mapper.Map<IList<HotelDetailsResponse>>(hotelDetail);
            return response;
        }
    }
}





