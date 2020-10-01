namespace ef_core_sqlite_example.Model
{
    public class Category
    {
        /* Attribute der Klasse Category: 
        * int Id: Primärschlüssel für die Tabelle genutzt (Autoincrement). Die Id wird in der Tabelle Mediums als Fremdschlüssel genutzt. 
        * string Categorytype: String Attribut zum speichern des Categorytypes (z.B.: Bilderbuch, Sachbuch, Spielfilm, etc.) 
        */

        public int Id { get; set; }  
        public string Categorytype { get; set; }
    }
}
