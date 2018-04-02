using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankCore.Models
{
    public class User
    {
        [StringLength(50, ErrorMessage = "Le nom doit contenir {1} caractères maximum.")]
        [Required(ErrorMessageResourceName = "required")]
        public string firstName { get; set; }
        [StringLength(50, ErrorMessage = "Le nom doit contenir {1} caractères maximum.")]
        [Required(ErrorMessageResourceName = "required")]
        public string lastName { get; set; }

        [Key]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessageResourceName = "required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "Le format du mail est incorrect.")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "required")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Le mot de passe doit être composé de 8 chiffres")]
        public string password { get; set; }

    }
}
