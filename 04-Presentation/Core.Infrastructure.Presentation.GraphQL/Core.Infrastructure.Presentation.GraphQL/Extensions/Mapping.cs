using AutoMapper;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using Core.Infrastructure.Domain.Contract.DTO.RefType;

namespace Core.Infrastructure.Presentation.GraphQL.Extensions
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<RefType, RefTypeDTO>();
        }
    }
}

