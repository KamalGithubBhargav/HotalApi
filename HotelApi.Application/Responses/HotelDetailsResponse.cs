namespace HotelApi.Application.Responses
{
    /// <summary>
    /// Data Transfer Object (DTO) representing the details of a hotel in API responses.
    /// </summary>
    public class HotelDetailsResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the hotel.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location of the hotel.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the rating of the hotel, typically ranging from 1 to 5.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the hotel.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of image URLs or paths associated with the hotel.
        /// </summary>
        public List<string> Images { get; set; } = [];
    }

}
