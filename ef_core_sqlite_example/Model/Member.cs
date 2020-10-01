using System;
using System.Collections.Generic;

namespace ef_core_sqlite_example.Model
{
    /* Attribute der Klasse Member.cs:
    * int Id: Primärschlüssel für die Tabelle (Autoincrement)
    * string FirstName: Speichert den Vornamen des Angestellten/Kunden 
    * string LastName: Speichert den Nachnamen des Angestellten/Kunden 
    * DateTime Birthday: Speichert den Geburtstag im Date Format (YYYY-MM-DD HH:MM:SS)
    * virtual Member Vorgesetzter: Fremdschlüssel welche auf Id in der Tabelle Members zeigt. Das Attribut speichert die Id des Vorgesetzten.
    * virtual ICollection<Member> Staff: Generische Liste welche alle Ids von Membern/Mitarbeitern speichert welche einem Vorgesetzten unterstellt sind (Wird nicht in der Tabelle gespeichert).
    */

    public class Member
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public virtual Member Vorgesetzter { get; set; }
        public virtual ICollection<Member> Staff { get; set; } = new List<Member>(); 
    }
}