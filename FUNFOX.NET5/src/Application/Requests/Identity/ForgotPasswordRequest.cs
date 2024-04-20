using System.ComponentModel.DataAnnotations;

namespace FUNFOX.NET5.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}