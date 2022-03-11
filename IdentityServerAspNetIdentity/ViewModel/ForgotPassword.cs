using System.ComponentModel.DataAnnotations;

namespace IdentityServerAspNetIdentity.ViewModel
{
    public class ForgotPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public string ReturnUrl { get; set; }
        
    }
}