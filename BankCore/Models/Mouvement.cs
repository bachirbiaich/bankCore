using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankCore.Models
{
    public class Mouvement : ModelBase
    {
        [ForeignKey("compte_id")]
        public virtual Compte compte { get; set; }

        [Required]
        public Guid? compte_id { get; set; }

        [Required]
        public double montant { get; set; }

        [Required]
        public string libelle { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}
