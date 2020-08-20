// using ef_core_sqlite_example.Model wird benötigt um auf die Klassen im Verzeichnis "Model/" zuzugreifen 
using ef_core_sqlite_example.Model;
using System;
using System.IO;
using System.Linq;

namespace ef_core_sqlite_example
{
    class Program
    {
        static void Main(string[] args)
        {

            // Löschen der bestehenden Datenbank. 
            // Die Methode File.Delete wird aus der Klasse System.IO geladen
            // DataBaseFile ist eine Variable aus der Klasse ModelContext 
            File.Delete(ModelContext.DataBaseFile);


            // using-Anweisungen werden genutzt um sicherzustellen das Objekte nur innerhalb dieser
            // Anweisung gültig sind und danach automatisch gelöscht werden. Die using-Anweisung macht das
            // disposen (löschen) von Objekten überflüssig

            // Bei einer Variable vom typ "var" wählt der C# Compiler automatisch den passenden Datentyp
            // Alternativ würde auch: using (ModelContext db = new ModelContext()) funktionieren

            using (var db = new ModelContext())
            {

                // EnsureCreated() ist eine Funktion aus Microsoft.EntityFrameworkCore welche prüft ob
                // die angegebene Datenbank vorhanden ist. Boolsche Rückgabewert

                // Frage: Wieso können wir nicht db.DatebaseFile.EnsureCreated() verwendet? Die Variable DatebaseFile
                // sollte uns eigentlich zur verfügung stehen. 

                db.Database.EnsureCreated();
                
                if (db.Items.Count() == 0)
                {
                    for (int i = 0; i < 13; i++)
                    {
                        db.Add(new Item());
                        // db.Add(new Item() {Id = 100})
                        db.SaveChanges();
                    }

                }

                if (db.Members.Count() == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        db.Add(new Member() { FirstName = "vorname " + i, LastName = "nachname " + i });
                        // db.Add(new Item() {Id = 100})
                        db.SaveChanges();
                    }

                }

                if (db.Items.Count() == 0)
                {
                    db.Add(new Item()); db.Add(new Item()); db.Add(new Item()); db.SaveChanges();
                }

                // show data
                foreach (var item in db.Items)
                {
                    Console.WriteLine($"Item: {item.Id}");
                }

                foreach(var member in db.Members)
                {
                    Console.WriteLine($"Vorname: {member.FirstName}");
                    Console.WriteLine($"Nachname: {member.LastName}");

                }
            }

            Console.WriteLine("\nDone.");
            Console.ReadLine();
        }
    }
}
