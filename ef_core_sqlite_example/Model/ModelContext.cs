using System;
using Microsoft.EntityFrameworkCore;

namespace ef_core_sqlite_example.Model
{

    // Unsere eigene Klasse ModelContext leitet von Microsoft.EntityFrameworkCore.DbContext ab und erbt somit
    // alle Eigenschaften (?) und Funktionen von Microsoft.EntityFrameworkCore.DbContext
    public class ModelContext : DbContext
    {
        // Name der SQLite Datenbank, als abwechslung mal eine Variable die mit const deklariert wurde und somit
        // während der Laufzeit des Programmes nicht verändert werden kann. 
        // Ohne Angabe eines Pfads wird die Datenbank im Projektordner gespeichert
        // /Users/nfurmanc/Projects/ef_core_sqlite_example/ef_core_sqlite_example/bin/Debug/netcoreapp3.1/CityLibrary.Sqlite

        public const string DataBaseFile = "CityLibrary.Sqlite"; // Collection for all persistent Item objects

        // set und get 
        public DbSet<Item> Items { get; set; }
        public DbSet<Member> Members { get; set; }

        // Connection String zur SQLite Datenbank
        // OOP Polymorphie 
        // Wir überschreiben die Methode OnConfiguring aus der Basisklasse DbContext um unsere Variable DataBaseFile
        // als Pfad zur SQLite DB zu nutzen 
        protected override void OnConfiguring(DbContextOptionsBuilder dcob)
        {
            // inject Sqlite usage
            dcob.UseSqlite("Data Source=" + DataBaseFile);
        }
    }
}
