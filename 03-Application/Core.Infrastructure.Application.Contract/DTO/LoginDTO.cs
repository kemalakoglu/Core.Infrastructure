using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Core.Infrastructure.Application.Contract.DTO
{
    [DataContract]
    public class LoginDTO
    {
        [DataMember] [Required] public string Email { get; set; }

        [DataMember] [Required] public string Password { get; set; }
    }
}