using BankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankCore.Models
{
    public class Virement : ModelBase
    {

        public string recipient_iban { get; set; }

        public string sender_id { get; set; }

        public int montant { get; set; }

        public DateTime? date { get; set; }

        public Boolean done { get; set; }


    }
}