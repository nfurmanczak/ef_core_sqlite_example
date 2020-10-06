// using ef_core_sqlite_example.Model wird benötigt um auf Klassen im Verzeichnis "Model/" zuzugreifen 
using ef_core_sqlite_example.Model;
using System.Collections.Generic; 
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace ef_core_sqlite_example
{
    class Program
    {
        static void Main(string[] args)
        {

            //  --> Löschen der bestehenden Datenbank. <-- 
            // Die Methode File.Delete wird aus der Klasse System.IO geladen
            // Die Variable DataBaseFile wird in der Klasse ModelContext.cs definiert 
            File.Delete(ModelContext.DataBaseFile);

            // --> Using <--
            // using-Anweisungen werden genutzt um sicherzustellen das Objekte nur innerhalb dieses
            // Codeblockes gültig sind und danach automatisch gelöscht werden. Die using-Anweisung macht das
            // disposen (löschen) von Objekten somit überflüssig.

            // Bei einer Variable vom typ "var" wählt der C# Compiler automatisch den passenden Datentyp
            // Alternativ würde auch: using (ModelContext db = new ModelContext()) funktionieren

            using (var db = new ModelContext())
            {

                db.Database.EnsureCreated();

                List<Member> members = Import.ImportMembers();
                List<Format> formats = Import.ImportFormat();
                List<Category> categories = Import.ImportCategory();
                List<Medium> mediums = Import.ImportMediums(formats, categories);
                List<Author> authors = Import.ImportAuthor();
                

                db.AddRange(members);
                db.AddRange(formats);
                db.AddRange(categories);
                db.AddRange(mediums);
                db.AddRange(authors); 
                db.SaveChanges();

                List<Medium_Author> medium_authors = Import.ImportAuthors_Medium(db.Authors.ToList(), db.Mediums.ToList());
                db.AddRange(medium_authors);
                db.SaveChanges(); 

            }

            Console.WriteLine("Fertig.");
            Console.ReadLine();
        }
    }
}
