using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Module3_Exercise2;



public class Animal
{

}


public class Dog : Animal
{

}


public interface ISpecificCollection<out T> // out - Ковариантний / in контрвариантний
{
    public T GetItem();
}

public class SpecificCollection<T> : ISpecificCollection<T>
{
    public T GetItem()
    {
        throw new NotImplementedException();
    }
}

public interface ISpecificCollectionContravariant<in T> // in контрвариантний
{
    public void SetItem(T item);
}

public class SpecificCollectionContravariant<T> : ISpecificCollectionContravariant<T>
{
    public void SetItem(T item)
    {
        throw new NotImplementedException();
    }
}


class Point
{
    public int X { get; set; }

    public int Y { get; set; }
}

public class Program
{
    public static void Main()
    {
        int[][] selectManyExample = new int[3][]
        {
            new int[] { 1, 2},
            new int[] { 3, 4},
            new int[] { 8, 9}
        };
        // 1 2 3 4 8 9

        int[] aligned = selectManyExample.SelectMany(x => x).ToArray();

        ExampleContacts();

        CultureInfo currentCulture = CultureInfo.CurrentCulture;
        CultureInfo japanCulture = new CultureInfo("ja-JP");
        CultureInfo plCulture = new CultureInfo("pl-PL");
        var towLetter = japanCulture.TwoLetterISOLanguageName;

        DateTime now = DateTime.Now;

        Console.WriteLine(now.ToString("D", currentCulture));
        var result1 = now.ToString("D", japanCulture);
        var result2 = now.ToString("D", plCulture);

        int[] numbers = { 1, 2, 3, 4, 5 };

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] *= numbers[i];
        }

        List<int> newNumbers = new List<int>();
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] < 10)
            {
                newNumbers.Add(numbers[i]);
            }
        }

        int[] newArray = newNumbers.ToArray();

        int[] numbersInSquare = numbers // 1 2 3 4 5
            .Select(x => x * x) // 1 4 9 16 25
            .Where(x => x < 10) // 1 4 9
            .Take(2) // 1 4
            .ToArray();

        int[] numbersInSquare1 = numbers // 1 2 3 4 5
            .Skip(2) // 3 4 5
            .Take(2) // 3 4
            .Select(x => x * x) // 9 16
            .Where(x => x < 10) // 9
            .ToArray();

        int[] numbersInSquare2 = numbers // 1 2 3 4 5
            .Take(2) // 1 2
            .Select(x => x * x) // 1 4
            .Where(x => x > 3) // 4
            .ToArray();

        int[] numbersInSquare3 = numbers // 1 2 3 4 5
            .Select(x => x * x) // 1 4 9 16 25
            .Where(x => x > 3) // 4 9 16 25
            .Take(2) // 4 9
            .ToArray();

        List<int> ints =  new List<int>() { 4, 5, 6, 7 };
        Dictionary<int, string> dictionary = new Dictionary<int, string>();

        var result = ints.Select(x => x + x);

        Point[] points = numbers.Select(x => new Point { X = x, Y = x * x }).ToArray();
    }

    public static void ExampleContacts()
    {
        ContactCollection contacts = new ContactCollection();

        Contact contact1 = new Contact { Name = "Alice 1", PhoneNumber = "123-456-7890" };
        Contact contact11 = new Contact { Name = "Alice 2", PhoneNumber = "123-456-7899" };

        Contact contact2 = new Contact { Name = "Василь 1", PhoneNumber = "987-654-3210" };
        Contact contact22 = new Contact { Name = "Василь 2", PhoneNumber = "987-654-3210" };

        contacts.AddContact(contact1, CultureInfo.GetCultureInfo("en-US"));
        contacts.AddContact(contact11, CultureInfo.GetCultureInfo("en-US"));
        contacts.AddContact(contact2, CultureInfo.GetCultureInfo("uk-UA"));
        contacts.AddContact(contact22, CultureInfo.GetCultureInfo("uk-UA"));

        CultureInfo selectedCulture = CultureInfo.GetCultureInfo("en-US");
        List<Contact> selectedContacts = contacts.GetContacts(selectedCulture);

        Console.WriteLine($"Contacts in {selectedCulture.Name}:");
        foreach (var contact in selectedContacts)
        {
            Console.WriteLine($"Name: {contact.Name}, Phone: {contact.PhoneNumber}");
        }
    }
}
