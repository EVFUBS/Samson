using AutoMapper;

namespace SamsonLocal
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<SamsonServerClient.Stream, Stream>();
            CreateMap<FileStream, SamsonServerClient.Stream>();
        }
    }
}
