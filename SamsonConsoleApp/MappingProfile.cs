using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<SamsonServerClient.Stream, Stream>();
            CreateMap<FileStream, SamsonServerClient.Stream>();
        }
    }
}
