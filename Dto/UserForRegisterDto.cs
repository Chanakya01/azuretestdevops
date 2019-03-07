using System.ComponentModel.DataAnnotations;

namespace Valuelabs.API.Dto
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username{get; set;}
        [Required]
        [StringLength(8,MinimumLength =4,ErrorMessage="Password must be between 4 to 8")]
        public string Password {get;set;}
        
    }
}