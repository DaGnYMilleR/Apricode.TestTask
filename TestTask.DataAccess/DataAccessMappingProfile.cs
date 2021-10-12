using AutoMapper;
using TestTask.Domain;

namespace TestTask.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<DataBaseVideoGame, VideoGame>()
                .ReverseMap();
        }
    }
}