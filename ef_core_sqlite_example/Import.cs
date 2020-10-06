using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ef_core_sqlite_example.Model;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Internal;

namespace ef_core_sqlite_example
{
    public class Import
    {

        /* Importieren von Personen (Members):
        * Via Streamreader wird einme CSV Datei (fake-persons.txt) eingelesen welche mehrere Personendaten enthält. Die erste Zeile enthält nur Metadaten welche 
        * übersprungen werden müssen. In der CSV Datei werden die einzelnen Daten mittels Semikolon (;) oder Komma (,) getrennt. Mittels Trim() wird die Zeile in 
        * in die Benötigten einzelnen Teile zerlegt und an ein Objekt vom Typ Member übergeben. Das Objekt wird dann in einert Liste (fakeMembers) gespeichert und am 
        * Ende der Methode zurückgegeben. 
        * 
        */

        public static List<Member> ImportMembers()
        {
            using (var sr = new StreamReader(@"@../../../fake-persons.txt", true))
            {
                List<Member> fakeMembers = new List<Member>();

                if (!sr.EndOfStream)
                {
                    sr.ReadLine();
                }

                while (!sr.EndOfStream)
                {
                    var l = sr.ReadLine();
                    var a = l.Split(';', ',');

                    var p = new Member()
                    {
                        FirstName = a[2].Trim(),
                        LastName = a[1].Trim(),
                        Birthday = DateTime.Parse(a[9].Trim(), CultureInfo.InvariantCulture)
                    };

                    fakeMembers.Add(p);
                }

                return fakeMembers;
            }
        }

        /* Importieren von Formatdaten:
         * Jedem Medium wird jeweils ein Formattyp (Buch, CD, Hardcover, etc.) zugeordnet. Die Formate beginnen in der Textdatei mit "FORMAT:".   
         * Die gefundenen Formate werden in einer Liste (List<Format> fakeFormat) gespeichert. Vor dem Speichern wird mittels einer Methode (CheckDuplicates) geprüft ob das Format schon
         * in der Liste gespeichert wurde. Die Methode gibt im Anschluss eine Liste mit Objekten vim Typ Format zurück. 
         */

        public static List<Format> ImportFormat()
        {

            Boolean CheckDuplicates(string line, List<Format> Liste)
            {
                bool check = false;

                foreach (var item in Liste)
                {
                    if (item.Formattype == line)
                        check = true; 
                }

                if (check)
                    return true;
                else
                    return false; 
            }

            using (var sr = new StreamReader(@"@../../../spiegel-bestseller.txt", true))
            {
                List<Format> fakeFormat = new List<Format>();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line.Length == 0) continue;
                    if (line.StartsWith("#")) continue;

                    if (line.StartsWith("FORMAT:"))
                    {
                        var a = line.Split(':');

                        if (!CheckDuplicates(a[1], fakeFormat))
                        {
                            Format f = new Format() { Formattype = a[1] };
                            fakeFormat.Add(f); 
                        }
                    }
                }

                return fakeFormat; 
            }

        }

        /* Importieren von Kategorien 
         * Das Importieren der Kategorien funktioniert wie bei den Formaten. Die Kategorien beginnen in der Textdatei mit "CATEGORY:". Die gefudenen Kategorioen werden 
         * in deiner Liste (List<Category> fakeCategory) gespeichert. Bei der speicherung wird mittels einer Methode darauf gedachtet das keine doppelten Kategorien 
         * gespeichert werden. Die Funktion gibt eine Liste mit mehreren Category Objekten zurück. 
         */

        public static List<Category> ImportCategory()
        {

            Boolean CheckDuplicates(string line, List<Category> Liste)
            {
                bool check = false;

                foreach (var item in Liste)
                {
                    if (item.Categorytype == line)
                    {
                        check = true;
                    }
                }

                if (check)
                    return true;
                else
                    return false;
            }

            using (var sr = new StreamReader(@"@../../../spiegel-bestseller.txt", true))
            {
                List<Category> fakeCategory = new List<Category>();

                while (!sr.EndOfStream)
                {

                    var line = sr.ReadLine();
                    if (line.Length == 0) continue;
                    if (line.StartsWith("#")) continue;

                    if (line.StartsWith("CATEGORY"))
                    {
                        var a = line.Split(':');

                        if (!CheckDuplicates(a[1], fakeCategory))
                        {
                            Category c = new Category() { Categorytype = a[1] };
                            fakeCategory.Add(c);
                        }
                    }
                }

                return fakeCategory;
            }

        }

        public static List<Author> ImportAuthor()
        {
            string ReplaceIfEmpty(string input)
            {
                if (input.Length == 0)
                    return "unbekannt";
                else
                    return input; 
            }

            using (var sr = new StreamReader(@"@../../../spiegel-bestseller.txt", true))
            {
                List<Author> fakeAuthor = new List<Author>();

                while (!sr.EndOfStream)
                {

                    var line = sr.ReadLine();
                    if (line.Length == 0 || line.StartsWith("#")) continue;

                    if (line.StartsWith("AUTHOR:"))
                    {
                        var a = line.Split(':');
                        var b = a[1].Split(';');
                        
                        foreach (var item in b)
                        {
                            
                            if(
                                    !fakeAuthor.Any(a => a.FirstName == item.Split(',').First()) &&
                                    !fakeAuthor.Any(b => b.LastName == item.Split(',').First())
                                   && a[1].Length != 0
                              )
                            {

                                fakeAuthor.Add(new Author
                                {
                                    FirstName = ReplaceIfEmpty(item.Split(',').Last().Trim()),
                                    LastName = ReplaceIfEmpty(item.Split(',').First().Trim())
                                });
                            }
                        }
                    }
                }
                return fakeAuthor;
            }
        }

        /* Importieren von Medien 
         * Die Daten für die Medien bestehen aus jeweils 6 Zeilen welche nicht alle Daten enthalten. Ein Medium beginnt immer mit "AUTHOR:" und endet mit "PRICE:". Die einzelnen 
         * Zeilen werden jeweils mittels Split(:) getrennt und in ein 1 Dimensionales String Array gespeichert. Wurde er Preis als letzter Wert eingelesen wird ein neues 
         * Objekt vom Typ Medium erzeugt und anschließend in eine List (List<Medium> fakeMediums) hinzugefügt. Der Rückgabewert kann dann in die Datenbank geschieben werden.
         * 
         * Beispiel Daten: 
         * 
         * AUTHOR:Owens, Delia
         * TITLE:Der Gesang der Flusskrebse
         * EAN:9783446264199
         * PUBLISHER:hanserblau
         * DATE:Juli 2019
         * PRICE:22,00€
         */

        public static List<Medium> ImportMediums(List<Format> formats, List<Category> category)
        {

            string Colonsplitt(string line)
            {
                if (line.Split(':')[1].Length == 0)            
                    return "unbekannt";
                else
                    return line.Split(':')[1]; 
            }

            using (var sr = new StreamReader(@"@../../../spiegel-bestseller.txt", true))
            {
                List<Medium> fakeMediums = new List<Medium>();

                string[] tmpmedium = new string[8];
 

                while (!sr.EndOfStream)
                {
                 
                    var line = sr.ReadLine();
                    if (line.Length == 0) continue;
                    if (line.StartsWith("#")) continue;

                    if (line.StartsWith("CATEGORY:"))
                    {
                        tmpmedium[7] = Colonsplitt(line);
                    }
                    
                    if (line.StartsWith("FORMAT:"))
                    {
                        tmpmedium[6] = Colonsplitt(line);
                    }

                    if (line.StartsWith("AUTHOR:"))
                    {
                        tmpmedium[0] = Colonsplitt(line);
                    }

                    if (line.StartsWith("TITLE:"))
                    {
                        tmpmedium[1] = Colonsplitt(line);
                    }

                    if (line.StartsWith("EAN:"))
                    {
                        tmpmedium[2] = Colonsplitt(line);
                    }

                    if (line.StartsWith("PUBLISHER:"))
                    {
                        tmpmedium[3] = Colonsplitt(line);
                    }

                    if (line.StartsWith("DATE:"))
                    {
                        tmpmedium[4] = Colonsplitt(line);
                    }

                    if (line.StartsWith("PRICE:"))
                    {
                        tmpmedium[5] = Colonsplitt(line);

                        Medium medium = new Medium()
                        {
                            //codebase.Methods.Where(x => (x.Body.Scopes.Count > 5) && (x.Foo == "test"));
                            Category = category.Where(c => c.Categorytype == tmpmedium[7]).First(),
                            Format = formats.Where(f => f.Formattype == tmpmedium[6]).First(),
                            Author = tmpmedium[0],
                            Title = tmpmedium[1],
                            Publisher = tmpmedium[3],
                            Date = tmpmedium[4],
                            Ean = tmpmedium[2],
                            Price = tmpmedium[5]
                        };

                        fakeMediums.Add(medium);
                    }
                }

                return fakeMediums; 
            }
        }

        public static List<Medium_Author> ImportAuthors_Medium(List<Author> authoren, List<Medium> mediums)
        {
            List <Medium_Author> medium_authors = new List<Medium_Author>();


            foreach (var item in mediums)
            {
                if (item.Author == "unbekannt") continue;

                foreach (var author in item.Author.Split(";"))
                {
                    // Enthält den einzelnen Namen des Authors
                
                    foreach (var x in authoren)
                    {
                        var name = x.LastName + " " + x.FirstName;
                        
                        if (name == author.Trim().Replace(",", ""))
                        {
                            var c = new Medium_Author() { MediumId = item, AuthorId = x };
                            medium_authors.Add(c);   
                        }
                    }
                }
            }
            return medium_authors; 
        }
    }
}
