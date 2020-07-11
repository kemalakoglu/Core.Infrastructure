using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Infrastructure.Domain.Contract.DTO.Authentication
{
    [DataContract]
    public class LoginResponseDTO
    {
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string Username { get; set; }
    }
}
