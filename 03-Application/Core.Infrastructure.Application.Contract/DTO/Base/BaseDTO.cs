using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Infrastructure.Application.Contract.DTO.Base
{
    [DataContract]
    public class BaseDTO
    {
        [DataMember] public long Id { get; set; }

        [DataMember] public bool Status { get; set; }

        [DataMember] public DateTime? InsertDate { get; set; }

        [DataMember] public DateTime? UpdateDate { get; set; }

        [DataMember] public bool IsActive { get; set; }
    }
}
