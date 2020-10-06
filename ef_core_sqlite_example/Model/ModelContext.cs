using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ef_core_sqlite_example.Model
{


    /* ModelContext
     * Die selbst definierte Klasse "ModelContext" erbt alle Methonden und Eigenschaften der bereits bestehenden Klasse "DbContext" vom EFCore Framework. 
     * In der Klasse "ModelContext" werden verschiedene Parameter und Einstellungen gesetzt die Verbindung zur Datenbank aufzubauen. Einige bestehende Funkltionen 
     * aus "DbContext" müssen noch überschrieben werden, diese Methoden sind jeweils mit "override" definiert.
     *
     * Wichtige Variablen und Methonden: 
     * - public const string DataBaseFile = Enthält den Namen und ggf. Pfad zur Datenbank. Ist kein Pfad angegeben wird die Datei im Debug Verzeichnis angelegt.
     * - Die benötigten Tabellen müssen als Eigenschaft (DbSet<Klasse>) angelegt werden.  
     * - Die Methode "OnConfiguring" stellt die eigentliche Verbindung zur Datenbank her. 
    */


    /* Loggen von Querys
     * EFCore bietet die Möglichkeit Querys zur Kontrolle auf der Console oder in einer Datei zu speichern. Zum aktivieren und deaktivieren wird die Variable 
     * "loggerFactory" bom Typ ILoggerFactory gesetzt. In der Methode "OnConfiguring" wird über eine if-Abfrage geprüft ob das Logging aktiviert werden soll. 
     * Über loggerFactory = LoggerFactory.Create() können noch weitere Parameter wie das Loglevel (LogLevel.Information oder .Error ... etc.) definiert werden. Über AddFile() kann die Ausgabe in eine Datei
     * uzmgeleitet werden. 
     * 
    */


    public class ModelContext : DbContext
    {
        public const string DataBaseFile = "CityLibrary.Sqlite";

        public DbSet<Item> Items { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Medium> Mediums { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Medium_Author> Medium_Authors { get; set; }

        private ILoggerFactory loggerFactory = null; 

        protected override void OnConfiguring(DbContextOptionsBuilder dcob)
        {
            dcob.UseSqlite("Data Source=" + DataBaseFile);

            // Logging DEAKTIVIERT: loggerFactory != null
            // Logging AKTIVIERT: loggerFactory = null
            if (loggerFactory != null)
            {
                loggerFactory = LoggerFactory.Create(lbldr => lbldr
                    .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information)
                    .AddConsole());
            }
            dcob = dcob.UseLoggerFactory(loggerFactory);
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definieren eines PK in der Tabelle Member 
            // modelBuilder.Entity<Medium>().HasKey("Id");
            

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
                .HasOne(i => i.Category)
                .WithMany().
                OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medium>()
                .HasOne(i => i.Format)
                .WithMany().
                OnDelete(DeleteBehavior.Cascade);

       

            /* Beispiel: 
             * Die Beschreibung von Beziehungen kann sowohl aus der Sicht der Tabelle Items als auch Members erfolgen.
             * Für Members ergibt sich eine erwas andere Sichtweise: Während ein Item nur einem Member zugeordnet wird, 
             * kann ein Member mehrere Items ausleihen. 
             *
             *  modelBuilder.Entity<Member>()
             *      .HasMany<Item>()
             *      .WithOne(i => i.Borrower)
             *      .OnDelete(DeleteBehavior.Cascade);
             */
        }
    }
}
