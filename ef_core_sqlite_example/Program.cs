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

            //  --> Löschen der bestehenden Datenbank. <-- 
            // Die Methode File.Delete wird aus der Klasse System.IO geladen
            // DataBaseFile ist eine Variable aus der Klasse ModelContext
            File.Delete(ModelContext.DataBaseFile);


            // --> Using <--
            // using-Anweisungen werden genutzt um sicherzustellen das Objekte nur innerhalb dieses
            // Codeblockes gültig sind und danach automatisch gelöscht werden. Die using-Anweisung macht das
            // disposen (löschen) von Objekten überflüssig.

            // Bei einer Variable vom typ "var" wählt der C# Compiler automatisch den passenden Datentyp
            // Alternativ würde auch: using (ModelContext db = new ModelContext()) funktionieren

            using (var db = new ModelContext())
            {

                // EnsureCreated() ist eine Funktion aus Microsoft.EntityFrameworkCore welche prüft ob
                // die angegebene Datenbank vorhanden ist. (Boolsche Rückgabewert)

                // Frage: Wieso können wir nicht db.DatebaseFile.EnsureCreated() verwendet?
                // Antwort: Das EF Framework erwartet die Verbindung zur Datenbank bzw. den Dateinamen in der Funktion
                // OnConfiguring (siehe Klasse ModelContext.cs) 

                db.Database.EnsureCreated();

                // Die Funktion db.Items.Count()  liefert als Rückgabewert die Anzahl der Datensätze (Tupel)
                // in der Tabelle Items. Sind keine Datensätze vorhanden, weil die DB z.B. vorher gelöscht wurde,
                // werden mittels einer FOR-Schleife mehrere Tupel in die Tabelle 

                if (db.Items.Count() == 0)
                {
                    for (int i = 0; i < 13; i++)
                    {
                        // Erzeugt ein neues Tupel in der Tabelle Item, da für die Spalte ID autoincrement gesetzt wurde,
                        // wählt SQLite  
                        db.Add(new Item());

                        // db.Add(new Item() {Id = 100}) --> Alternativer Insert mit einem Festenwert für die ID 

                        // Änderungen an der Tabelle oder Tupeln werden erst mit der Funktion SaveChanges() in die Datenbank übernommen. 
                        db.SaveChanges();
                    }

                }

                // Analog zu Items: Wenn keine Tupel vorhanden sind, wird mittels einer FOR-Schleif 
                if (db.Members.Count() == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        db.Add(new Member() { FirstName = "vorname " + i, LastName = "nachname " + i});
                        // db.Add(new Item() {Id = 100})
                        db.SaveChanges();
                    }

                }

                if (db.Items.Count() == 0)
                {
                    db.Add(new Item()); db.Add(new Item()); db.Add(new Item()); db.SaveChanges();
                }


                // Auslesen von Tupel mittels FOR-Schleife und Platzhalter 
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
