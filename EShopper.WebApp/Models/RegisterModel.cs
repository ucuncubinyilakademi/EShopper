using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EShopper.WebApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="FullName boş geçilemez.")]
        public string FullName { get; set; }
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
