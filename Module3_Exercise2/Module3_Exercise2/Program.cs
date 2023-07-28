using System;
using System.Collections.Generic;
using System.Globalization;

namespace Module3_Exercise2;

public class Program
{
    public static void Main()
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

        CultureInfo selectedCulture = CultureInfo.GetCultureInfo("uk-UA");
        List<Contact> selectedContacts = contacts.GetContacts(selectedCulture);

        Console.WriteLine($"Contacts in {selectedCulture.Name}:");
        foreach (var contact in selectedContacts)
        {
            Console.WriteLine($"Name: {contact.Name}, Phone: {contact.PhoneNumber}");
        }
    }
}
