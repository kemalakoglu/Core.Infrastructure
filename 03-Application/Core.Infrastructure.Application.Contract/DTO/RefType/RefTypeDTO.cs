using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.DTO.Base;
using MediatR;

namespace Core.Infrastructure.Application.Contract.DTO.RefType
{
    [DataContract]
    public class RefTypeDTO : BaseDTO, IRequest<RefTypeDTO>
    {
        public RefTypeDTO() { }
        [DataMember] public string Name { get; set; }
        [DataMember] public RefTypeDTO Parent { get; set; }
    }
}
