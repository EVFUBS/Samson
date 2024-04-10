using AutoMapper;

namespace SamsonConsoleApp.Helpers
{
    public static class ToServerStream
    {
        public static SamsonServerClient.Stream ToStream(this FileStream data)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<System.IO.Stream, SamsonServerClient.Stream>());
            var mapper = new Mapper(config);
            SamsonServerClient.Stream convertedData = mapper.Map<SamsonServerClient.Stream>(data);
            return convertedData;
        }
    }
}
