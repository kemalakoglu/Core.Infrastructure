using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MediatR;

namespace Core.Infrastructure.Domain.Contract.DTO.RefType
{
    [DataContract]
    public class GetRefTypesResponseDTO : IRequest<GetRefTypesResponseDTO>
    {
        [DataMember]
        public IEnumerable<RefTypeDTO> RefTypes { get; set; }
    }
}
