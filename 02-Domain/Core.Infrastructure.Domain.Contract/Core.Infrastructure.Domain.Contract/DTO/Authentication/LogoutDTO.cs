using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Infrastructure.Domain.Contract.DTO.Authentication
{
    [DataContract]
    public class LogoutDTO
    {
        [DataMember]
        public string Email { get; set; }
        //[DataMember]
        //public string Token { get; set; }
    }
}
