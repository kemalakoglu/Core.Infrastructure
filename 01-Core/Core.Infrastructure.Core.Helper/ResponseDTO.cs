﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Infrastructure.Core.Helper
{
    [DataContract]
    public class ResponseDTO<T> where T : class
    {
        [DataMember]
        public T Data { get; set; }
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
