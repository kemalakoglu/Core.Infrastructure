using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Core.Infrastructure.Application.Contract.DTO.Base;

namespace Core.Infrastructure.Application.Contract.DTO.RefType
{
    [DataContract]
    public class AddRefTypeResponseDTO : BaseDTO
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public long ParentId { get; set; }
    }
}
