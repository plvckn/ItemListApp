using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemListLibrary
{
    public class Item // represents a list item: has a numeric value, name and may have another list within itself.
    {
        static Random rand = new Random(); // used for random number generation to be assigned to each item's numeric value

        public int Number { get; private set; }
        public string Name { get; private set; }
        public List<Item> SubItem = new List<Item>();

        public Item() { }
        public Item(string name) { Name = name; }
        public Item(int number, string name)
        {
            Number = number;
            Name = name;
        }


        public static void CreateList(Item item, int count)
        {
            if (count > 0)
            {
                item.SubItem.Add(new Item(generateNumber(), generateName(item)));
                count--;
                CreateList(item, count);
            }
        }

        public static Item LocateItem(Item item, string name)
        {
            if (item.Name.Equals(name))
                return item;
            foreach (var subItem in item.SubItem)
            {
                var result = LocateItem(subItem, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static int generateNumber() => (int)rand.Next(1000);

        public static string generateName(Item item)
        {
            string itemName = item.Name;
            string newSubitemName = "";

            if (itemName.StartsWith("main"))
            {
                for (char newName = 'a'; newName < 'z'; newName++) // item names after main item start with a combination of alphabetical values in increasing order (a, b, c..), every subitem inherits item name (parent name) and adds to it.
                {
                    if (LocateItem(item, newName.ToString()) is null) // if the list does not yet contain an item with item.name = newName, assign newName to a new item.
                        return newSubitemName = newName.ToString();
                }
                return null; 
            }
            else
            {
                if (!item.SubItem.Any())    // if an item does not contain any subitems, the first subitem can take parent's name (item.name) and add the first letter ('a') to have a new unique name created for it
                    return string.Concat(itemName, "a");
                char newName = 'a';
                string currName = item.SubItem.Last().Name; // if an item does have subitems, look at last subitem to compare its name in order to create new unique name
                char nameEndsWith = currName[currName.Length - 1];
                while (nameEndsWith >= newName) // we can scan through possible names to find one that is available (a name that no other subitem has).
                {
                    newName++;
                }
                newSubitemName = string.Concat(itemName, newName.ToString());
                return newSubitemName;
            }

        }

        public static string PrintList(Item item, StringBuilder sb)
        {
            if (item.Name is "main")
                sb.AppendLine(item.Name);
            else
                sb.AppendLine(item.Name + " " + item.Number);

            foreach (Item subitem in item.SubItem)
                PrintList(subitem, sb);
            return sb.ToString();
        }
        

        public static int SumItem(Item item, string name)
        {
            int result = 0;
            var itemToSum = LocateItem(item, name);
            foreach (var subitem in itemToSum.SubItem)
            {
                result += subitem.Number;
            }
            return result;
        }

        public static void SortItem(Item item, string name, bool ascending = true)
        {
            var itemToSort = LocateItem(item, name);
            if (ascending)
            {
                itemToSort.SubItem.Sort((x, y) => x.Number.CompareTo(y.Number));
            }
            else itemToSort.SubItem.Sort((y, x) => x.Number.CompareTo(y.Number));
        }

    }
}



