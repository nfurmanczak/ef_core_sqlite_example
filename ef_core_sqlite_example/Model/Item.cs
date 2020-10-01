namespace ef_core_sqlite_example.Model

{

    /* Attribute der Klasse Item:
     * int Id: Primärschlüssel der Tabelle 
     * ItemState State: Zustand des Items als emum Typ.
     * string Storage Location: Speichert den Lagerort des Mediums.
     * string Comment: Kommentar. 
     * Member Borrower: Fremdschlüssel aus der Tabelle Members (Id).
     * 
     * Hinweis: Das Item repräsentiert die Anzahl der verfügbaren Exemplare. Allerdings fehlt die Verbindung zwischen Item und Mediums. 
     */


    /* Beeziehung zwischen Members und Items: 
         
               ┌──────────────────────┐                           ┌──────────────────────────┐                            
               │       Members        │                           │           Items          │
               │----------------------│  Borrower                 │--------------------------│
               │ + Id : Int (PK)      │◆──────────────────────────│ - id : int               │
               │ + FirstName : Text   │0,1        borrow >    0..*│ + id : int               │
               │ + LastName : Text    │                           │ + State : ItemState      │
               │ + Birthday : Date    │                           │ + StorageLocation : Text │
               └──────────────────────┘                           │ + Borrower : Member (FK) │
                                                                  └──────────────────────────┘
        Create Table Statement mit FK: 
        CREATE TABLE IF NOT EXISTS "Items" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_Items" PRIMARY KEY AUTOINCREMENT,
            "State" INTEGER NOT NULL,
            "StorageLocation" TEXT NULL,
            "Comment" TEXT NULL,
            "BorrowerId" INTEGER NULL,
            CONSTRAINT "FK_Items_Members_BorrowerId" FOREIGN KEY ("BorrowerId") REFERENCES "Members" ("Id") ON DELETE RESTRICT
        );
        */

    public class Item
    {
        public int Id { get; set; } 
        public ItemState State { get; set; }
        public string StorageLocation { get; set; }
        public string Comment { get; set; }
        public Member Borrower { get; set; }
    }

    /* Enumerations Datentyp:
     * Ist ein Wertetyp mit vorab definierten Konstanten.
     * Jeder Enum-Wert enthält automatisch eine Zahlenrepräsentation, beginnend mit 0 für den ersten Wert.
     * Achtung: Ein Enum kann nur außerhalb einer Funktion definiert werden. 
     * Usable   =>  0
     * Ordered  =>  1
     * Defect   =>  2
     */

    public enum ItemState
    {
        Usable, 
        Ordered,
        Defect 
    }
}
