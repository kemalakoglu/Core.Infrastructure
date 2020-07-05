using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Core.Infrastructure.Domain.Contract.DTO.Base;

namespace Core.Infrastructure.Domain.Contract.DTO.RefType
{
    [DataContract]
    public class AddRefTypeResponseDTO : BaseDTO
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public long ParentId { get; set; }
    }
}
