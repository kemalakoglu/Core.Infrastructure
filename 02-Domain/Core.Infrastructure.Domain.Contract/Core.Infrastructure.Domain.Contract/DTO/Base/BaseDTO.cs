using System;
using System.Runtime.Serialization;

namespace Core.Infrastructure.Domain.Contract.DTO.Base
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
