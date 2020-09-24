namespace ef_core_sqlite_example.Model
{

    // Medium.cs 
    // Die Klasse beschreibt die Attribute der SQLite Tabelle, jede Klasse im Ordner Model bildet eine Tabelle ab.
    // Der Konstruktur der Klasse darf keine Parameter haben. Es sollte also der C# Standardkonstruktor genutzt werden
    // oder ein parameterloser Konstruktur verwendet werden.

    public class Medium
    {        
        public int Id { get; set; } // Primärschlüssel
        public string Title { get; set; }
        public string Publisher { get; set; }
        public virtual string Autor { get; set; }
        public string Price { get; set; }
        public string Ean { get; set; }
        public string Date { get; set; }
        public Category Categorytype { get; set; } // Fremdschlüssel zu Tabelle Categorys.Id
        public Format Formattype { get; set; } // Fremdschlüssel zur Tabelle Formats.Id
    }
}
