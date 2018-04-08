using BankCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankCore.Models
{
    public class User : ModelBase
    {
        [StringLength(50, ErrorMessage = "Le nom doit contenir {1} caractères maximum.")]
        [Required]
        public string firstname { get; set; }

        [StringLength(50, ErrorMessage = "Le nom doit contenir {1} caractères maximum.")]
        [Required]
        public string lastname { get; set; }
    
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "Le format du mail est incorrect.")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Le mot de passe doit être composé de 8 chiffres")]
        public string password { get; set; }

        public bool canVir { get; set; }
    }
}
