using System;
namespace ef_core_sqlite_example.Model
{

    // Member.CS
    // Die Klasse beschreibt die Attribute der SQLite Tabelle, jede Klasse im Ordner Model bildet eine Tabelle ab.
    // Der Konstruktur der Klasse darf keine Parameter haben. Es sollte also der C# Standardkonstruktor genutzt werden
    // oder ein parameterloser Konstruktur verwendet werden. 

    public class Member
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; } 
    }
}
