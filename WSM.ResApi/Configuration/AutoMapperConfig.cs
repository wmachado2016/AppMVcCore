using AutoMapper;
using WSM.ResApi.Models;

namespace WSM.ResApi.Configuration.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Conta, ContaViewModel>().ReverseMap();
        }        
    }
}
