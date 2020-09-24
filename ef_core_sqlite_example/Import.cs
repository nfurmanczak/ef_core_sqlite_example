using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using ef_core_sqlite_example.Model;
using System.Globalization;
using System.Threading;
using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.CompilerServices;

namespace ef_core_sqlite_example
{
    public class Import
    {

        public static List<Member> ImportMembers ()
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

        public static List<Medium> ImportMediums(List<Format> formats, List<Category> category )
        {

            string colonsplitt(string line)
            {
                var tmp = line.Split(':');
                if (tmp[1].Length == 0)            
                    return "unbekannt";
                else
                    return tmp[1]; 
            }

            using (var sr = new StreamReader(@"@../../../spiegel-bestseller.txt", true))
            {
                List<Medium> fakeMediums = new List<Medium>();

                string[] tmpmedium = new string[8];
 

                while (!sr.EndOfStream)
                {
                    List<Category> importCategory = new List<Category>();


                    var line = sr.ReadLine();
                    if (line.Length == 0) continue;
                    if (line.StartsWith("#")) continue;

                    if (line.StartsWith("CATEGORY:"))
                    {
                        tmpmedium[7] = colonsplitt(line);
                    }
                    
                    if (line.StartsWith("FORMAT:"))
                    {
                        tmpmedium[6] = colonsplitt(line);
                    }

                    if (line.StartsWith("AUTHOR:"))
                    {
                        tmpmedium[0] = colonsplitt(line);
                    }

                    if (line.StartsWith("TITLE:"))
                    {
                        tmpmedium[1] = colonsplitt(line);
                    }

                    if (line.StartsWith("EAN:"))
                    {
                        tmpmedium[2] = colonsplitt(line);
                    }

                    if (line.StartsWith("PUBLISHER:"))
                    {
                        tmpmedium[3] = colonsplitt(line);
                    }

                    if (line.StartsWith("DATE:"))
                    {
                        tmpmedium[4] = colonsplitt(line);
                    }

                    if (line.StartsWith("PRICE:"))
                    {
                        tmpmedium[5] = colonsplitt(line);


                        Medium bla = new Medium()
                        {
                            Autor = tmpmedium[0],
                            Categorytype = category.Where(c => c.Categorytype == tmpmedium[7]).First(),
                            Formattype = formats.Where(f => f.Formattype == tmpmedium[6]).First(), 
                            Title = tmpmedium[1],
                            Publisher = tmpmedium[3],
                            Date = tmpmedium[4],
                            Ean = tmpmedium[2],
                            Price = tmpmedium[5]
                        };

                        fakeMediums.Add(bla); 
                        /* TBD 
                        // Mehrere Autoren sind am Bich beteiligt
                        if (tmpmedium[0].Contains(";"))
                        {
                             
                            int count = tmpmedium[0].Count(f => f == ';');
                            var tmp = line.Split(';');
                            for (int i = 0; i < count; i++)
                            {

                            }

                            Console.WriteLine("Anzahl Autoren: " + count);
                            // Zwei Autoren vorhanden
                            Console.WriteLine("Autoren: " + tmpmedium[0]);
                            Medium bla = new Medium() { Autor = tmpmedium[0], Categorytype = c, Formattype = f, Title = tmpmedium[1], Publisher = tmpmedium[3], Date = tmpmedium[4], Ean = tmpmedium[2], Price = tmpmedium[5] };
                            fakeMediums.Add(bla);
                        }
                        else
                        {
                            // Nur ein Autor vorhanden
                            Medium bla = new Medium() { Autor = tmpmedium[0], Categorytype = c, Formattype = f, Title = tmpmedium[1], Publisher = tmpmedium[3], Date = tmpmedium[4], Ean = tmpmedium[2], Price = tmpmedium[5] };
                            fakeMediums.Add(bla);
                        }
                        */ 
                    }
                }

                return fakeMediums; 
            }
        }

    }
}
