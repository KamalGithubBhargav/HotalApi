namespace HotelApi.Domain.Entities
{
    /// <summary>
    /// Represents the hotel details stored in the data source.
    /// </summary>
    public class HotelDetail
    {
        /// <summary>
        /// Gets or sets the unique identifier for the hotel.
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
        /// Gets or sets the rating of the hotel (e.g., from 1 to 5).
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the hotel.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of image URLs or paths for the hotel.
        /// </summary>
        public List<string> Images { get; set; } = [];
    }

}
