using HotelApi.Domain.Entities;
using HotelApi.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace HotelApi.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        // Path to the JSON file containing hotel data
        private readonly string _path;
        private readonly string _imageUrl;

        // JsonSerializer options to handle camelCase and case-insensitive property names
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // Constructor reads the base path for the JSON file from configuration and combines with file name
        public HotelRepository(IConfiguration configuration)
        {
            var basePath = configuration.GetValue<string>("DatabaseSettings:FilePath")!;
           
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentException("File path is not configured properly.");
            }

            _path = Path.Combine(basePath, "Hotels.json");
            _imageUrl = configuration.GetValue<string>("DatabaseSettings:ImageUrl") ?? "assets/hotels/";
        }

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
        public async Task<List<HotelDetail>> GetHotelsAsync(int pageNumber, int pageSize, string? filterText, double minRating, string sortOption, CancellationToken cancellationToken)
        {
            // Read the JSON file
            var json = await File.ReadAllTextAsync(_path, cancellationToken);

            // Return empty list if file is empty or null
            if (string.IsNullOrWhiteSpace(json))
            {
                return [];
            }

            // Deserialize hotel list
            var hotels = JsonSerializer.Deserialize<List<HotelDetail>>(json, _jsonOptions)!;

            // Filter by name or location (case-insensitive)
            if (!string.IsNullOrWhiteSpace(filterText))
            {
                var lowerText = filterText.ToLower();
                hotels = hotels.Where(h =>
                    h.Name.ToLower().Contains(lowerText) ||
                    h.Location.ToLower().Contains(lowerText))
                    .ToList();
            }

            // Filter by minimum rating
            hotels = hotels
                .Where(h => h.Rating >= minRating)
                .ToList();

            // Sort
            hotels = sortOption?.ToLower() switch
            {
                "rating" => hotels.OrderByDescending(h => h.Rating).ToList(),
                _ => hotels.OrderBy(h => h.Name).ToList() // Default: sort by name
            };

            // Apply pagination AFTER filtering and sorting
            var pagedHotels = hotels
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Update image paths with full URL prefix
            foreach (var hotel in pagedHotels)
            {
                hotel.Images = hotel.Images
                    .Select(img => $"{_imageUrl}{img}")
                    .ToList();
            }

            return pagedHotels;
        }

    }

}
