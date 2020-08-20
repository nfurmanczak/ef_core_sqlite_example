using System;
namespace ef_core_sqlite_example.Model

{
    // ITEMS.CS
    // Die Klasse beschreibt die Attribute der SQLite Datenbank 
    // Konstruktur der Klasse darf keine Parameter haben. Parameterlose Konstruktoren werden aufgerufen, wenn ein Objekt
    // durch Verwendung des Operators new instanziiert wird und keine Argumente für new bereitgestellt werden.

    // Klassen ohne Konstruktoren erhalten vom C#-Compiler einen öffentlichen parameterlosen Konstruktor, um die Instanziierung
    // der Klasse zuzulassen, außer die Klasse ist static. 

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
