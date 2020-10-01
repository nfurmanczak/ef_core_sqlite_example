namespace ef_core_sqlite_example.Model
{
    public class Format
    {
        /* Attribute der Tabelle Format:
        * int Id: Primärschlüssel für die Tabelle genutzt (Autoincrement). Die Id wird in der Tabelle Mediums als Fremdschlüssel genutzt. 
        * string Formattype: String Attribut zum speichern des Formattyps (z.B.: CD, DVD, Buch, Taschenbuch, etc.) 
        */

        public int Id { get; set; }
        public string Formattype { get; set; }

       
    }
}
