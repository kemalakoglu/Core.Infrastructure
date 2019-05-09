using System.Runtime.Serialization;

namespace Core.Infrastructure.Application.Contract.DTO
{
    [DataContract]
    public class ErrorDTO
    {
        [DataMember] public string Message { get; set; }
        [DataMember] public string RC { get; set; }
    }
}