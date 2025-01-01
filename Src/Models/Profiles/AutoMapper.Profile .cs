using AutoMapper;
using BaseCodeAPI.Src.Models.Entity;

namespace BaseCodeAPI.Src.Models.Profiles
{
   public class AutoMapperProfile : Profile
   {
      public AutoMapperProfile()
      {
         CreateMap<TokenUserModelDto, UserModel>();
         CreateMap<UserModelDto,      UserModel>();
         CreateMap<PersonModelDTO,    PersonModel>();
         CreateMap<AddressModelDto,   AddressModel>();
         CreateMap<ClientModelDto,    ClientModel>();

         CreateMap<UserModel,    UserModelDto>();
         CreateMap<PersonModel,  PersonModelDTO>();
         CreateMap<AddressModel, AddressModelDto>();
         CreateMap<ClientModel,  ClientModelDto>();

         CreateMap<PersonModel, PersonModel>().ForMember(dest => dest.Id, opt => opt.Ignore())
         .ForMember(dest => dest.Document, opt => opt.Ignore());

         CreateMap<AddressModel, AddressModel>().ForMember(dest => dest.Id, opt => opt.Ignore());
      }
   }
}
