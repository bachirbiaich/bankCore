using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankCore.Models
{
    public class Compte : ModelBase
    {
        [ForeignKey("owner_id")]
        public virtual User owner { get; set; }

        [Required]
        public Guid? owner_id { get; set; }

        [Required]
        public double solde { get; set; }

        [Required]
        public DateTime creation_date { get; set; }

        [Required]
        public string iban { get; set; }
    }
}
