﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankCore.Models
{
    public class Virement : ModelBase
    {

        [Required]
        public string recipient_iban { get; set; }

        [ForeignKey("sender_id")]
        public virtual User sender { get; set; }

        [Required]
        public Guid? sender_id { get; set; }

        [Required]
        public double montant { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        public bool done { get; set; }


    }
}