using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ef_core_sqlite_example.Model
{
    public class Menu
    {

        public static void MenuLoop<T>(List<T> menu)
        {
            using (var db = new ModelContext())
            {
                bool loopcheck;

                do
                {
                    Menu.PrintMenu(menu);
                    byte userInput = Menu.GetUserInput(menu);
                    loopcheck = (userInput != menu.Count + 1);
                    Menu.ExecuteMenupoint(userInput);

                } while (loopcheck);
            }
        }

        public static void PrintMenu<T>(List<T> menu)
        {
            foreach (var mitem in menu.Select((value, index) => new { value, index }))
            {
                Console.WriteLine("{0}. {1}", mitem.index + 1, mitem.value);
                if (mitem.index + 1 == menu.Count) Console.WriteLine("{0}. Beenden", menu.Count + 1);
            }
        }

        public static byte GetUserInput<T>(List<T> menu)
        {
            string userInputString;
            do
            {
                Console.Write("Eingabe: ");
                userInputString = Console.ReadLine();

                if (!byte.TryParse(userInputString, out _))
                {
                    Console.WriteLine("Ungültige Eingabe!");
                    PrintMenu(menu);
                }
                else if (byte.TryParse(userInputString, out _))
                {
                    if (Convert.ToByte(userInputString) > menu.Count + 1)
                    {
                        
                        Console.WriteLine("Unbekannter Menüpunkt!");
                        PrintMenu(menu);
                        continue;
                    }
                }

            } while (!byte.TryParse(userInputString, out _));

            return Convert.ToByte(userInputString);

        }

        public static void ExecuteMenupoint(byte userInput)
        {
            using (var db = new ModelContext())
            {
                if (userInput == 1)
                    Service.MediumsInventory(db.Mediums.Include(nameof(Medium.Format)).Include(nameof(Medium.Category)).ToList());

                else if (userInput == 2)
                {
                    List<string> fList = new List<string>();
                    foreach (var item in db.Formats.ToList()) fList.Add(item.Formattype);
                    bool loopcheck;
                   
                    do
                    {
                        Menu.PrintMenu(fList);
                        byte userI = Menu.GetUserInput(fList);
                        loopcheck = (userI != fList.Count + 1);
                        if (userI < fList.Count + 1)
                            Service.MediumsInventory(db.Mediums.Include(nameof(Medium.Format)).Where(p => p.Format.Formattype.Contains(fList[userI-1])).ToList());

                    } while (loopcheck);
      
                }

                else if (userInput == 3)
                {
                    Console.WriteLine("ID: ");
                    //Service.MediumsInventory(db.Mediums.Include(nameof(Medium.Format)).Include(nameof(Medium.Category)).Where(p => p.Id == (1)).ToList());
                    Service.UpdateMedium(Convert.ToInt32(Console.ReadLine()));
                }

                else if (userInput == 4)
                    Console.WriteLine("// Medium zurückgeben!");

                else if (userInput == 5)
                {
                    Service.MediumsInventory(db.Mediums.Include(nameof(Medium.Format)).Include(nameof(Medium.Category)).Where(p => p.Borrow == 1).ToList());
                }
            }
        }

    }
}
