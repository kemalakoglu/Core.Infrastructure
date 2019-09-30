using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using AutoMapper;

namespace Core.Infrastructure.Presentation.GraphQL.Extensions
{
    public static class MappingExtensions
    {
        public static void ConfigureMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.CreateMap<RefType, RefTypeDTO>();
                cfg.CreateMap<AddRefTypeResponseDTO, AddRefTypeRequestDTO>();
            });
        }
    }
}

