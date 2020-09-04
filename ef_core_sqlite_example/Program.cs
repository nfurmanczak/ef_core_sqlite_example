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

                // EnsureCreated() ist eine Funktion aus Microsoft.EntityFrameworkCore welche prüft, ob
                // die angegebene Datenbank vorhanden ist. (Boolscher Rückgabewert)

                // Frage: Wieso können wir nicht db.DatebaseFile.EnsureCreated() verwendet?
                // Antwort: Das EF Framework erwartet die Verbindung zur Datenbank bzw. den Dateinamen in der Funktion
                // OnConfiguring (siehe Klasse ModelContext.cs) 

                db.Database.EnsureCreated();

                // Die Funktion db.Items.Count() liefert als Rückgabewert die Anzahl der Datensätze (Tupel)
                // in der Tabelle Items. Sind keine Datensätze vorhanden, weil die DB z.B. vorher gelöscht wurde,
                // werden mittels einer FOR-Schleife mehrere Tupel in die Tabelle geschrieben.

                // ACHTUNG: Jede Transaktion in der Datenbank wird erst mit db.SaveChanges() in die Datenbank geschrieben

                if (db.Items.Count() == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        // Erzeugt ein neues Tupel in der Tabelle Item
                        // db.Add(new Item());
                        // db.Add(new Item() {Id = 100}) --> Alternativer Insert mit einem Festenwert für die ID

                        db.Add(new Item() { State = ItemState.Usable, StorageLocation = "Regal " + i });
                        db.SaveChanges();
                    }
                }

                // var x1 = db.Members.FirstOrDefault(); Auslesen des ersten Datensatzes 

                // Hinzufügen des ersten Member/Angestellten 
                var boss = new Member() { FirstName = "Hugo", LastName = "Boss"  }; 

                // Hinzufügen von zusätzen Membern/Angestellten welche als Vorgesetzten "Hugo Boss" haben
                db.Add(new Member() { FirstName = "Peter", LastName = "Postmann", Vorgesetzter = boss });
                db.Add(new Member() { FirstName = "Lisa", LastName = "Lustig", Vorgesetzter = boss });
                db.SaveChanges();


                // FirstOrDefault() ist ein LINQ-Konvertierungsoperator, wie alle LINQ-Abfragen wird diese 
                var m1 = db.Members.FirstOrDefault();
                db.Add(new Item() { State = ItemState.Usable, StorageLocation = "Regal 9", Borrower = m1 });
                db.SaveChanges();
                Console.WriteLine("Ausgabe m1: " + m1.FirstName + " " + m1.LastName);
     
                // foreach (var item in db.Items) Console.WriteLine($"ITEM : {item.Id}->{item.Borrower?.Id}");

                // Auslesen von Tupel mittels FOR-Schleife und Platzhalter in Console.WriteLine() 
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

            Console.WriteLine("\n=*=*=* ENDE =*=*=*");
            Console.ReadLine();
        }
    }
}
