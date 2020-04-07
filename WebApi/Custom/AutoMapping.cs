using Api.Entitys;
using AutoMapper;

namespace WebApi.Custom
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
                CreateMap<Product, Message>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src));
        }
    }
}
