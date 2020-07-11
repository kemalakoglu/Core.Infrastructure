using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Core.Infrastructure.Domain.Contract.DTO.Authentication
{
    [DataContract]
    public class LoginDTO
    {
        [EmailAddress] [DataMember] [Required] public string Email { get; set; }
        [DataMember] [Required] public string Password { get; set; }
        [DataMember] [Required] public bool IsPersistance { get; set; }
    }
}