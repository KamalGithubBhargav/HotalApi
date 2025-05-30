using HotelApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // IMediator is injected directly into the controller via the constructor
    public class HotelController(IMediator mediator) : ControllerBase
    {
        // MediatR is used to decouple the controller from business logic
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves a filtered, sorted, and paginated list of hotels.
        /// </summary>
        /// <param name="pageNumber">The current page number to retrieve (1-based index). Default is 1.</param>
        /// <param name="pageSize">The number of hotels to return per page. Default is 9.</param>
        /// <param name="filterText">Optional text to filter hotels by name or location (case-insensitive).</param>
        /// <param name="minRating">Optional minimum rating to filter hotels (default is 0).</param>
        /// <param name="sortOption">Sort option: "rating" for descending rating, or any other value for name ascending.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the filtered, sorted, and paginated list of hotels.</returns>
        [HttpGet("getHotels")]
        public async Task<IActionResult> GetHotels(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 9,
            [FromQuery] string? filterText = null,
            [FromQuery] double minRating = 0,
            [FromQuery] string sortOption = "name")
        {
            var result = await _mediator.Send(new GetAllHotelQuery(pageNumber, pageSize, filterText, minRating, sortOption));
            return Ok(result);
        }

    }
}
