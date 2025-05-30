using AutoMapper;

namespace HotelApi.Application.Mappers
{
    /// <summary>
    /// Provides a singleton instance of AutoMapper's IMapper configured with hotel mappings.
    /// </summary>
    public static class HotelMapper
    {
        // Lazy initialization to create the mapper instance only when first accessed
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<HotelMappingProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        /// <summary>
        /// The singleton IMapper instance used for mapping hotel domain models to DTOs and vice versa.
        /// </summary>
        public static IMapper Mapper = Lazy.Value;
    }

}
