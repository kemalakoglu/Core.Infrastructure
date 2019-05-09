using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Infrastructure.Application.Contract.DTO
{
    [DataContract]
    public class ResponseDTO
    {
        [DataMember]
        public object Data { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public Information Information { get; set; }
        [DataMember]
        public string RC { get; set; }
    }

    [DataContract]
    public class Information
    {
        [DataMember]
        public string TrackId { get; set; }
    }
}
