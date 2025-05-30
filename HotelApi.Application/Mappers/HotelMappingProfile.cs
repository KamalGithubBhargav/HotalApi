using AutoMapper;
using HotelApi.Application.Responses;
using HotelApi.Domain.Entities;

namespace HotelApi.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile that defines mapping rules between hotel domain entities and response DTOs.
    /// </summary>
    public class HotelMappingProfile : Profile
    {
        /// <summary>
        /// Configures mappings between HotelDetail and HotelDetailsResponse objects.
        /// Enables two-way mapping with ReverseMap().
        /// </summary>
        public HotelMappingProfile()
        {
            CreateMap<HotelDetail, HotelDetailsResponse>().ReverseMap();
        }
    }

}
