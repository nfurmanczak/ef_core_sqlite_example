namespace ef_core_sqlite_example.Model

{
    // ITEM.CS
    // Die Klasse Items.cs beschreibt die Attribute der SQLite Tabelle, jede Klasse im Ordner Model bildet eine Tabelle ab.
    // Der Konstruktur der Klasse darf keine Parameter haben. Es sollte also der C# Standardkonstruktor genutzt werden
    // oder ein parameterloser Konstruktur verwendet werden. 

    public class Item
    {
        // Privater Constructor, hat allerdings in dieser Konfiguration keine andere Funktion als der Standardskonstruktur von C# 
        public Item() { }

        public int Id { get; set; } // Primärschlüssel 
        public ItemState State { get; set; }
        public string StorageLocation { get; set; }
        public string Comment { get; set; }
        public Member Borrower { get; set; }

        /* Beschreibung der Beziehung zwischen den Tabellen Item und Member 
         * Jede Tabelle besitzt nur einen PK, als FK kann nur ein Attribut dienen das in einer anderen Tabelle PK ist. 
         * In der Tabelle Items wird das Attribut Borrower hinzugefügt. Da Borrower vom Typm Member ist, wird automatisch 
         * der PK von Members (Id : Int) als FK in Items eingetragen. 
         
               ┌──────────────────────┐                           ┌──────────────────────────┐                            
               │       Members        │                           │           Items          │
               │----------------------│  Borrower                 │--------------------------│
               │ + Id : Int (PK)      │◆──────────────────────────│ - id : int               │
               │ + FirstName : Text   │0,1        borrow >    0..*│ + id : int               │
               │ + LastName : Text    │                           │ + State : ItemState      │
               │ + Birthday : Date    │                           │ + StorageLocation : Text │
               └──────────────────────┘                           │ + Borrower : Member (FK) │
                                                                  └──────────────────────────┘
        * Create Table Statement mit FK: 
        CREATE TABLE IF NOT EXISTS "Items" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_Items" PRIMARY KEY AUTOINCREMENT,
            "State" INTEGER NOT NULL,
            "StorageLocation" TEXT NULL,
            "Comment" TEXT NULL,
            "BorrowerId" INTEGER NULL,
            CONSTRAINT "FK_Items_Members_BorrowerId" FOREIGN KEY ("BorrowerId") REFERENCES "Members" ("Id") ON DELETE RESTRICT
        );
        */
    }

    // Enumerations Datentyp: Ist ein Wertetyp mit vorab definierten Konstanten.
    // Jeder Enum-Wert enthält automatisch eine Zahlenrepräsentation, beginnend mit 0 für den ersten Wert.
    // Achtung: Ein Enum kann nur außerhalb einer Funktion definiert werden. 
    public enum ItemState
    {
        Usable, // 0
        Ordered, // 1
        Defect // 2
    }
}
