namespace ef_core_sqlite_example.Model
{

    // Medium.cs 
    // Die Klasse beschreibt die Attribute der SQLite Tabelle, jede Klasse im Ordner Model bildet eine Tabelle ab.
    // Der Konstruktur der Klasse darf keine Parameter haben. Es sollte also der C# Standardkonstruktor genutzt werden
    // oder ein parameterloser Konstruktur verwendet werden.

    public class Medium
    {        
        public string Identifier { get; set; } // Primärschlüssel 
        public string Title { get; set; }
        public string Category { get; set; }
        public string Kind { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Autor { get; set; }
    }
}
