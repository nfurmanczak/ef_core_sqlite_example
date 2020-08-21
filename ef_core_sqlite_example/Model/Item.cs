using System;
namespace ef_core_sqlite_example.Model

{
    // ITEMS.CS
    // Die Klasse beschreibt die Attribute der SQLite Tabelle, jede Klasse im Ordner Model bildet eine Tabelle ab.
    // Der Konstruktur der Klasse darf keine Parameter haben. Es sollte also der C# Standardkonstruktor genutzt werden
    // oder ein parameterloser Konstruktur verwendet werden. 

    public class Item
    {
        // Privater Constructor, hat allerdings so keine andere Funktion als der Standardskonstruktur von C# 
        public Item() { }

        public int Id { get; set; }
        public ItemState State { get; set; }
        public string StorageLocation { get; set; }
        public string Comment { get; set; }
    }

    public enum ItemState { Usable, Ordered, Defect }
}
