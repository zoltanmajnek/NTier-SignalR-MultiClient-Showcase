using CABTEX_HFT_2023241.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CABTEX_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string itemName)
        {
            {
                Console.Write("Enter " + itemName + " Name/Label: ");
                string name = Console.ReadLine();
                try
                {
                    switch (itemName)
                    {
                        case "Friend": rest.Post(new Friend() { Name = name }, itemName); break;
                        case "Software": rest.Post(new Software() { Name = name }, itemName); break;
                        case "MyDisk": rest.Post(new MyDisk() { Label = name }, itemName); break;
                        case "DiskType": rest.Post(new DiskType() { TypeName = name, Capacity = 1200 }, itemName); break;
                    }
                }
                catch (Exception) { Console.Write("You need a longer name probably. Press Enter."); Console.ReadLine(); }
            }
        }

        static void List<T>(string itemName)
        {

            List<T> items = rest.Get<T>(itemName);
            foreach (var item in items)
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                        Console.Write(prop.GetValue(item) + " ");
                }
                Console.WriteLine();
            }

            Console.Write("Press Enter to continue.");
            Console.ReadLine();
        }

        static void Update<T>(string itemName)
        {
            Console.Write("Enter " + itemName + "'s id to update: ");
            int id = int.Parse(Console.ReadLine());
            T old = rest.Get<T>(id, itemName);
            if (old != null)
            {
                foreach (var prop in old.GetType().GetProperties())
                    if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.PropertyType == typeof(string))
                    {
                        Console.Write($"New data [old: {prop.GetValue(old)}]: ");
                        string data = Console.ReadLine();
                        prop.SetValue(old, data);
                    }
                rest.Put(old, itemName);
            }

            Console.WriteLine();
        }

        static void Delete(string itemName)
        {
            Console.Write("Enter " + itemName + "'s id to delete: ");
            int id = int.Parse(Console.ReadLine());
            try { rest.Delete(id, itemName); }
            catch (Exception) { Console.WriteLine("Unsuccessfull"); }
        }

        static void ShowNonCrudData(string what)
        {
            List<string> items = rest.Get<string>("ALotOfNonCrudLogic/" + what);
            foreach (var item in items) { Console.Write(" " + item); }
        }

        static string NonCrudSingleData(string what) => rest.GetSingle<string>("ALotOfNonCrudLogic/" + what);

        static void Main(string[] args)
        {
            Console.Write("Press Enter soon : )"); Console.ReadLine();
            rest = new RestService("http://localhost:53910/", "Software");            
            Console.WriteLine("Some nonCrud query: ");
            Console.Write("Who lives in Szabadság utca?: "); ShowNonCrudData("WhoLiveInMyStreet/szab"); Console.WriteLine();
            Console.WriteLine("Largest Disk VendorName: " + NonCrudSingleData("LargestDiskVendorName"));
            Console.WriteLine("Free Space on disk 10: " + NonCrudSingleData("FreeSpace/10"));
            Console.WriteLine("Average size of the RPG genre: " + NonCrudSingleData("AvgSizeInaGenre/rpg"));
            Console.Write("Who has Last Ninja?: "); List<Friend>("ALotOfNonCrudLogic/WhoHasThisSoftware/last%20ninja");
            //Console.Write("Etc :). Press Enter to continue to the CRUD menu!"); Console.ReadLine();

            var FriendSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List<Friend>("Friend"))
                .Add("Create", () => Create("Friend"))
                .Add("Delete", () => Delete("Friend"))
                .Add("Update", () => Update<Friend>("Friend"))
                .Add("Exit Friends", ConsoleMenu.Close);

            var DiskTypeSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List<DiskType>("DiskType"))
                .Add("Create", () => Create("DiskType"))
                .Add("Delete", () => Delete("DiskType"))
                .Add("Update", () => Update<DiskType>("DiskType"))
                .Add("Exit Types", ConsoleMenu.Close);

            var MyDiskSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List<MyDisk>("MyDisk"))
                .Add("Create", () => Create("MyDisk"))
                .Add("Delete", () => Delete("MyDisk"))
                .Add("Update", () => Update<MyDisk>("MyDisk"))
                .Add("Exit MyDisks", ConsoleMenu.Close);

            var SoftwareSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List<Software>("Software"))
                .Add("Create", () => Create("Software"))
                .Add("Delete", () => Delete("Software"))
                .Add("Update", () => Update<Software>("Software"))
                .Add("Exit Softwares", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Softwares", () => SoftwareSubMenu.Show())
                .Add("Friends", () => FriendSubMenu.Show())
                .Add("DiskTypes", () => DiskTypeSubMenu.Show())
                .Add("MyDisks", () => MyDiskSubMenu.Show())
                .Add("Exit Client", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
