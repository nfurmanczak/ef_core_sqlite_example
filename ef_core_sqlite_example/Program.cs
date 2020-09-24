// using ef_core_sqlite_example.Model wird benötigt um auf die Klassen im Verzeichnis "Model/" zuzugreifen 
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

                // EnsureCreated() ist eine Funktion aus Microsoft.EntityFrameworkCore welche prüft, ob
                // die angegebene Datenbank vorhanden ist. (Boolscher Rückgabewert)

                db.Database.EnsureCreated();

                // Importieren von Testdaten                  
                db.AddRange(Import.ImportMembers());
                db.AddRange(Import.ImportFormat());
                db.AddRange(Import.ImportCategory());
                db.SaveChanges();
                db.AddRange(Import.ImportMediums(db.Formats.ToList(), db.Categorys.ToList()));
                db.SaveChanges();
    
            }

            Console.WriteLine("\n=*=*=* ENDE =*=*=*");
            Console.ReadLine();
        }
    }
}
