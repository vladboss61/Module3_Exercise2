using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Module3_Exercise2;

public sealed class Contact
{
    public string Name { get; set; }

    public string PhoneNumber { get; set; }
}

public sealed class ContactCollection
{
    private readonly Dictionary<CultureInfo, SortedDictionary<string, List<Contact>>> _contactsByCulture;

    public ContactCollection()
    {
        _contactsByCulture = new Dictionary<CultureInfo, SortedDictionary<string, List<Contact>>>();
    }

    public void AddContact(Contact contact, CultureInfo cultureInfo = null)
    {
        if (cultureInfo == null)
        {
            cultureInfo = CultureInfo.GetCultureInfo("en-US"); // Default culture: English
        }

        if (!_contactsByCulture.ContainsKey(cultureInfo))
        {
            var alphabet = GetAlphabetForCulture(cultureInfo);
            _contactsByCulture[cultureInfo] = new SortedDictionary<string, List<Contact>>(alphabet);
        }

        string groupName = GetGroupName(contact, cultureInfo);

        if (!_contactsByCulture[cultureInfo].TryGetValue(groupName, out var contacts))
        {
            contacts = new List<Contact>();
            _contactsByCulture[cultureInfo][groupName] = contacts;
        }

        contacts.Add(contact);
    }

    public List<Contact> GetContacts(CultureInfo cultureInfo)
    {
        if (!_contactsByCulture.ContainsKey(cultureInfo))
        {
            return new List<Contact>();
        }

        var contacts = _contactsByCulture[cultureInfo]
            .SelectMany(kv => kv.Value)
            .ToList();

        return contacts;
    }

    private IComparer<string> GetAlphabetForCulture(CultureInfo cultureInfo)
    {
        if (cultureInfo.TwoLetterISOLanguageName == "uk")
        {
            // Ukrainian alphabet
            return StringComparer.Create(cultureInfo, true);
        }

        // Default: English alphabet
        return StringComparer.InvariantCultureIgnoreCase;
    }

    private string GetGroupName(Contact contact, CultureInfo cultureInfo)
    {
        string alphabet = contact.Name.Substring(0, 1);
        if (!string.IsNullOrEmpty(alphabet))
        {
            return char.IsDigit(alphabet[0]) ? "0-9" : alphabet.ToUpper(cultureInfo);
        }

        return char.IsDigit(alphabet[0]) ? "0-9" : alphabet.ToUpper(cultureInfo);
    }
}