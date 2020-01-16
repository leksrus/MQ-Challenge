using Api.Entitys;
using AutoMapper;

namespace WebApi.Custom
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
                CreateMap<Product, Message>()
                .ForMember(dest => dest.Product.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Product.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
