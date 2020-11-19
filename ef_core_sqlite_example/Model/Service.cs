using System;
using System.Linq; //ToList()
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography.X509Certificates;

namespace ef_core_sqlite_example.Model
{
    


    public delegate bool Matcher(Medium m1);

    public class Service
    {
      
        public static void MediumsInventory(List<Medium> mediumList)
        {
            Console.WriteLine("************************************************************");
            foreach (var medium in mediumList)
            {   
                Console.WriteLine("{0}, {1}, {2}", medium.Id,  medium.Title, medium.Format.Formattype);   
            }
            Console.WriteLine("************************************************************");
        }

        public static void FormatInventory(List<Medium> mediumList)
        {
            Console.WriteLine("************************************************************");
            foreach (var medium in mediumList)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", medium.Id, medium.Title, medium.Format.Formattype, medium.Category.Categorytype);

            }
            Console.WriteLine("************************************************************");
        }

        public static void UpdateMedium(int id)
        {
            using (ModelContext db = new ModelContext())
            {
                var f = db.Mediums.SingleOrDefault(x => x.Id == id);
                f.Borrow = 1;
                db.SaveChanges(); 
            }
        }

        public static void ListInventory(ModelContext db, Matcher matches = null)
        {
            //var mediumList = db.Mediums.ToList(); 
            var mediumList = db.Mediums.Include(nameof(Medium.Format)).ToList();


            foreach (var item in mediumList)
            {
                if (matches == null || matches(item))
                {
                    Console.WriteLine("{0}, {1}, {2}", item.Id, item.Title, item.Format.Formattype);
                }
            }
        }

        public static void BorrowItem(ModelContext db)
        {
            
        }

        public static List<Format> SelectFormats()
        {
            List<string> fList = new List<string>(); 

            using var db = new ModelContext();
            var f1 = db.Formats.Select(p => new { p.Formattype });

            foreach (var i in db.Formats.Select(p => new { p.Formattype })) fList.Add(i.Formattype);
   
            return db.Formats.ToList();
             
        }

    }
}
