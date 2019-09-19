using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Core.Infrastructure.Application.Contract.DTO.Base;

namespace Core.Infrastructure.Application.Contract.DTO.RefType
{
    [DataContract]
    public class AddRefTypeRequestDTO : BaseDTO
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public RefTypeDTO Parent { get; set; }
    }
}
