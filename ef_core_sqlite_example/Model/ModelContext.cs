using System;
using Microsoft.EntityFrameworkCore;

namespace ef_core_sqlite_example.Model
{

    // Unsere eigene Klasse ModelContext leitet von Microsoft.EntityFrameworkCore.DbContext ab und erbt somit
    // alle Eigenschaftenund Funktionen von Microsoft.EntityFrameworkCore.DbContext
    public class ModelContext : DbContext
    {
        // Name der SQLite Datenbank, als abwechslung mal eine Variable die mit const deklariert wurde und somit
        // während der Laufzeit des Programmes nicht verändert werden kann. 
        // Ohne Angabe eines Pfads wird die Datenbank direkt im Projektordner gespeichert
        // Beispiel: /Users/nikolai/Projects/ef_core_sqlite_example/ef_core_sqlite_example/bin/Debug/netcoreapp3.1/CityLibrary.Sqlite

        public const string DataBaseFile = "CityLibrary.Sqlite";

        // Einbinden der Klassen damit ModelContext die 
        public DbSet<Item> Items { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Medium> Mediums { get; set; }

        // Überschreiben der Funktion OnConfiguring aus der Klasse Microsoft.EntityFrameworkCore.DbContext
        // In dieser Funktion werden die Zugriffsinformationen zur Datenbank gespeichert. Je nach genutzem
        // DBMS müssen andere Informationen (Treiber, Port, Socket, IP, etc.) angegeben werden. 

        protected override void OnConfiguring(DbContextOptionsBuilder dcob)
        {
            dcob.UseSqlite("Data Source=" + DataBaseFile);
        }

        // Überschreiben der Funktion OnModelCreating aus der Klasse Microsoft.EntityFrameworkCore.DbContext
        // In der Funktion wird u.a. definiert welche Primärschlüssel die jeweiligen Tabellen besitzen. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //  
            //modelBuilder.Entity<Member>().HasNoKey();

            // Definieren eines PK in der Tabelle Member 
            modelBuilder.Entity<Medium>().HasKey("Identifier");

            // Zusammengesetzter PK in der Tabelle 
            modelBuilder.Entity<Member>().HasKey("LastName", "FirstName");
            //modelBuilder.Entity<Member>().HasAlternateKey("LastName", "FirstName"); 
        }
    }
}
