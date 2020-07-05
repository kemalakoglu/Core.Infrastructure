using System.Runtime.Serialization;

namespace Core.Infrastructure.Domain.Contract.DTO.Error
{
    [DataContract]
    public class ErrorDTO
    {
        [DataMember] public string Message { get; set; }
        [DataMember] public string RC { get; set; }
    }
}