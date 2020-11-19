// using ef_core_sqlite_example.Model wird benötigt um auf Klassen im Verzeichnis "Model/" zuzugreifen 
using ef_core_sqlite_example.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ef_core_sqlite_example
{
    class Program
    {
        static void Main(string[] args)
        {
            File.Delete(ModelContext.DataBaseFile);

            /*
             * USING
             * using-Anweisungen werden genutzt um sicherzustellen das Objekte nur innerhalb dieses
             * Codeblockes gültig sind und danach automatisch gelöscht werden. Die using-Anweisung macht das
             * disposen (löschen) von Objekten somit überflüssig.
            /*


             /* Importieren von Beispieldaten */
            using (ModelContext db = new ModelContext())
            {
                db.Database.EnsureCreated();

                List<Member> members = Import.ImportMembers();
                List<Format> formats = Import.ImportFormat();
                List<Category> categories = Import.ImportCategory();
                List<Medium> mediums = Import.ImportMediums(formats, categories);
                List<Author> authors = Import.ImportAuthor();

                //db.AddRange(members, formats, categories, ... ) => Nicht Möglich. 
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

            /* Benutzerinterface */
            List<string> menu = new List<string>() { "Aktuellen Bestand abfragen", "Filter nach Format (Buch, Taschenbuch, etc.)", "Medium ausleihen", "Medium zurückgeben", "Ausgeliehende Medien anzeigen" };
            Menu.MenuLoop(menu); 

        }
    }
}
