using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System; 

namespace ef_core_sqlite_example.Model
{
    /* Attribute der Klasse Medium.cs: 
    * int Id: Primärschlüssel für die Tabelle. 
    * string Title: Enthält den Titel des Mediums (Kann auch Whitespaces oder Sonderzeichen enthalten. UTF8 Codierung empfohlen).
    * string Publisher: Enthält den Publischer des Mediums. 
    * string Ean: Enhält eine Zeichenkette welche als String gespeichert wird.
    * string Date: Enthält das Datum welches aus Monat (Ausgeschrieben) und Jahr (YYYY) besteht.
    * Category Category: Fremdschlüssel aus der Tabelle Category (Attribut Id, INT).
    * Format Format: Fremdschlüssel aus der Tabelle Format (Attribut Id, INT).
    * string Author: Durch den [NOTMAPPED] Status wird in der Tabelle Mediums kein Attribut mit dem Namenmn Author angelegt.

    * Mögliche Verbesserungen?
    * => Ean könnte als Primärschlüssel genutzt werden
    * => Ean könnte als INT gespeichert werden  
    * => Price sollte als REAL (sqlite 8-byte IEEE floating number Datentyp) gespeichert werden um berechnungen auszuführen.
    * => Date sollte auch als Dateformart gespeichert (Leider kein gültiges Datum in der Importdatei vorhanden.)
    */

    public class Medium
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Price { get; set; }
        public string Ean { get; set; }
        public string Date { get; set; }
        public Category Category { get; set; }
        public Format Format { get; set; }
        public string Author { get; set; }
        public int Borrow { get; set; }
        // [NotMapped]
        // public string Author { get; set; }
        // public virtual ICollection<String> Author { get; set; } = new List<String>();
    }
}