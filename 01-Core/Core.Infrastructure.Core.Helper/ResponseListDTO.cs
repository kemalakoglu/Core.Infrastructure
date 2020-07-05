using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Core.Infrastructure.Core.Helper
{
    [DataContract]
    public class ResponseListDTO<T> where T : class
    {
        [DataMember]
        public IEnumerable<T> Data { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public Information Information { get; set; }
        [DataMember]
        public string RC { get; set; }
    }
}
