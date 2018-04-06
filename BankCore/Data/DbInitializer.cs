using BankCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankCore.Data
{
    //Création DB Forced selon les données a chaque fois qu'on run l'appli
    //public class DbInitializer
    //{
    //    public static void Initialize(BankCoreContext context)
    //    {
    //        context.Database.EnsureCreated();

    //        //recherche s'il y a enregistrement Categories dans la table---------------------------------------------------------
    //        if (context.Virement.Any())
    //        {
    //            return;
    //        }
    //        //declaration des donnes qui seront enregistrer dans la BD Local
    //        var virements = new Virement[]
    //        {
    //            //new Virement{recipient_iban="IBAN: FR076SDFKSJHFDKJSHFKJSFD", sender_id="1", montant=333, date="21/12/2018 AM 09:00", done=true}
    //            //new Virement{_id = '1A3B944E-3632-467B-A53A-206305310BAE', UpdatedAt= "", recipient_iban="IBAN: FR076SDFKSJHFDKJSHFKJSFD", sender_id="1", montant=333, date="21/12/2018", done=true}
    //        };
    //        //on tourne la boucle pour enregistrer 2 données
    //        foreach (Virement vir in virements)
    //        {
    //            context.Virement.Add(vir);
    //        }
    //        context.SaveChanges();


    //    }
    //}
}
