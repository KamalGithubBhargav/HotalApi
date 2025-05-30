using HotelApi.Domain.Entities;

namespace HotelApi.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for hotel data retrieval.
    /// </summary>
    public interface IHotelRepository
    {

        /// <summary>
        /// Retrieves a filtered, sorted, and paginated list of hotels from the JSON data source.
        /// Applies optional filtering by name or location, minimum rating, and sorting by rating or name.
        /// </summary>
        /// <param name="pageNumber">The current page number (1-based index).</param>
        /// <param name="pageSize">The number of hotels to return per page.</param>
        /// <param name="filterText">Optional text to filter hotels by name or location (case-insensitive).</param>
        /// <param name="minRating">The minimum rating threshold to include hotels.</param>
        /// <param name="sortOption">The sorting option: "rating" for descending rating or any other value for ascending name.</param>
        /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
        /// <returns>A paginated list of <see cref="HotelDetail"/> objects after applying filter and sort operations.</returns>
        Task<List<HotelDetail>> GetHotelsAsync(int pageNumber, int pageSize, string? filterText, double minRating, string sortOption, CancellationToken cancellationToken);
    }

}
