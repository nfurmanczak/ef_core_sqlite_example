using System;
using System.Collections.Generic;

namespace ef_core_sqlite_example.Model
{

    // Member.CS
    // Die Klasse beschreibt die Attribute der SQLite Tabelle, jede Klasse im Ordner Model bildet eine Tabelle ab.
    // Der Konstruktur der Klasse darf keine Parameter haben. Es sollte also der C# Standardkonstruktor genutzt werden
    // oder ein parameterloser Konstruktur verwendet werden. 

    public class Member
    {
        public int Id { get; set; } // Primärschlüssel 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        // FK der als PK die Id aus der Tabelle Members nutzt 
        public virtual Member Vorgesetzter { get; set; }

        // Generische Liste welche alle Ids von Membern/Mitarbeitern speichert welche einem Vorgesetzten unterstellt sind
        // Die Liste speichert nicht nur die Ids sondern enthält zusätzlich die Verknüpfung zum kompletten Objekt welche
        // alle Attribute speichert 
        public virtual ICollection<Member> Staff { get; set; } = new List<Member>(); 
    }
}