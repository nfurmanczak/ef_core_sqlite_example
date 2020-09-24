using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ef_core_sqlite_example.Model
{

    // Unsere eigene Klasse ModelContext leitet von Microsoft.EntityFrameworkCore.DbContext ab und erbt somit
    // alle Eigenschaften und Funktionen von dieser Klase. 
    public class ModelContext : DbContext
    {
        // Name der SQLite Datenbank, als abwechslung mal eine Variable die mit const deklariert wurde und somit
        // während der Laufzeit des Programmes nicht verändert werden kann. 
        // Ohne Angabe eines Pfads wird die Datenbank direkt im Projektordner gespeichert
        // Beispiel: /Users/nikolai/Projects/ef_core_sqlite_example/ef_core_sqlite_example/bin/Debug/netcoreapp3.1/CityLibrary.Sqlite
        public const string DataBaseFile = "CityLibrary.Sqlite";

        // Einbinden der Klassen damit ModelContext die Tabllen anlegt
        public DbSet<Item> Items { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Medium> Mediums { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Format> Formats { get; set; }

        // Erstelle eine Variable vom Typ ILoggerFactory mit null
        // Die Variable kann als Schalter genutzt werden um die Logfunktion im Programm zu aktivieren oder deaktivieren. 
        private ILoggerFactory loggerFactory = null; 

        // Überschreiben der Funktion OnConfiguring aus der Klasse Microsoft.EntityFrameworkCore.DbContext.
        // In dieser Funktion werden u.a. die Zugriffsinformationen und Einstellungen der Datenbank gespeichert.
        // Je nach genutztem DBMS müssen zusätzliche Informationen (Treiber, Port, Socket, IP, etc.) angegeben werden. 
        protected override void OnConfiguring(DbContextOptionsBuilder dcob)
        {
            dcob.UseSqlite("Data Source=" + DataBaseFile);

            // Abfrage des Logging-Schalter ob aktiv oder inaktiv
            // Logging DEAKTIVIERT: loggerFactory != null
            // Logging AKTIVIERT: loggerFactory = null
            if (loggerFactory != null)
            {
                // Definition der Logging Parameter 
                //loggerFactory = LoggerFactory.Create(lbldr => lbldr.AddConsole());
                loggerFactory = LoggerFactory.Create(lbldr => lbldr
                    .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information)
                    .AddConsole());
                
                // LogLevel.{Information, Debug, Error, Critical, Trace, None, Warning}
                // AddConsole() leitet die Ausgaben auf die Console um, alternativ kann man mittels AddFile()
                // die Loginformationen in eine Datei umleiten. 

            }
            dcob = dcob.UseLoggerFactory(loggerFactory);
        }

        // Überschreiben der Funktion OnModelCreating aus der Klasse Microsoft.EntityFrameworkCore.DbContext
        // In der Funktion wird u.a. definiert welche Primärschlüssel die jeweiligen Tabellen besitzen. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definieren eines PK in der Tabelle Member 
            modelBuilder.Entity<Medium>().HasKey("Id");
            

            // Beispiel für einen Zusammengesetzten PK in der Tabelle Members für die Attribute LastName und FirstName
            //modelBuilder.Entity<Member>().HasKey("LastName", "FirstName");
            //modelBuilder.Entity<Member>().HasAlternateKey("LastName", "FirstName");

            // Beschreibung einer 1:n Beziehung zwischen Items und Members
            // Ein Item kann maximal nur von einem Members. 
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Borrower)
                .WithMany().
                OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medium>()
               .HasOne(i => i.Categorytype)
               .WithMany().
               OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medium>()
               .HasOne(i => i.Formattype)
               .WithMany().
               OnDelete(DeleteBehavior.Cascade);


            /* Die Beschreibung von Beziehungen kann sowohl aus der Sicht der Tabelle Items als auch Members erfolgen.
             * Für Members ergibt sich eine erwas andere Sichtweise: Während ein Item nur einem Member zugeordnet wird, 
             * kann ein Member mehrere Items ausleihen. 
             
            modelBuilder.Entity<Member>()
                   .HasMany<Item>()
                   .WithOne(i => i.Borrower)
                   .OnDelete(DeleteBehavior.Cascade);
            */
        }
    }
}
