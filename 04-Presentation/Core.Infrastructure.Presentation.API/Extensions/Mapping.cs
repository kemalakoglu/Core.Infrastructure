using AutoMapper;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using Core.Infrastructure.Domain.Contract.DTO.RefType;

namespace Core.Infrastructure.Presentation.API.Extensions
{
    public class Mapping : Profile
    {
        //public static void ConfigureMapping()
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.AllowNullCollections = true;
        //        cfg.CreateMap<RefType, RefTypeDTO>();
        //        cfg.CreateMap<AddRefTypeResponseDTO, AddRefTypeRequestDTO>();
        //    });
        //}

        public Mapping()
        {
            CreateMap<RefType, RefTypeDTO>();
            CreateMap<AddRefTypeResponseDTO, AddRefTypeRequestDTO>();
        }
    }
}