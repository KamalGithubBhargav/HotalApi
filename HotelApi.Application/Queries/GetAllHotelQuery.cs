using HotelApi.Application.Responses;
using MediatR;

namespace HotelApi.Application.Queries
{
    /// <summary>
    /// Represents a query to retrieve a filtered, sorted, and paginated list of hotel details.
    /// Implements MediatR's <see cref="IRequest{TResponse}"/> interface with a response type of <see cref="IList{HotelDetailsResponse}"/>.
    /// </summary>
    /// <param name="PageNumber">The 1-based page number to retrieve.</param>
    /// <param name="PageSize">The number of hotel records to return per page.</param>
    /// <param name="FilterText">Optional filter text to match against hotel name or location.</param>
    /// <param name="MinRating">Minimum rating value to filter hotels (inclusive).</param>
    /// <param name="SortOption">Sorting criteria: 'rating' for rating descending, otherwise sorts by name ascending.</param>
    public class GetAllHotelQuery(int PageNumber, int PageSize, string? FilterText, double MinRating, string? SortOption)
        : IRequest<IList<HotelDetailsResponse>>
    {
        /// <summary>
        /// Gets or sets the page number to retrieve (1-based).
        /// </summary>
        public int PageNumber { get; set; } = PageNumber;

        /// <summary>
        /// Gets or sets the number of hotels per page.
        /// </summary>
        public int PageSize { get; set; } = PageSize;

        /// <summary>
        /// Gets or sets the optional filter text to search by hotel name or location.
        /// </summary>
        public string? FilterText { get; set; } = FilterText;

        /// <summary>
        /// Gets or sets the minimum hotel rating to include in the result.
        /// </summary>
        public double MinRating { get; set; } = MinRating;

        /// <summary>
        /// Gets or sets the sort option: 'rating' for sorting by rating descending, otherwise sorts by name ascending.
        /// </summary>
        public string? SortOption { get; set; } = SortOption;
    }


}
