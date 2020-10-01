
namespace ef_core_sqlite_example.Model
{
    /* Attribute der Klasse Author:
    * int Id: Primärschlüssel für die Tabelle (Autoincrement).
    * string FirstName: Speichert den Vornamen des Autors.  
    * string LastName: Sppeichert den Nachnamen und zweiten Vornamen (falls Vorhanden). 
    * Medium MediumId: Der Primärschlüssel aus der Tabelle Medium (Id) wird hier als Fremdschlüssel verwendet 
    */

    public class Author
    {
        public int Id { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Medium MediumId { get; set; } 
    }
}
