![logo_big](https://user-images.githubusercontent.com/8418700/140829550-9b90ffc0-d13e-48c3-b2bc-ff84827b0adf.png)

> DotLiquid is a templating system ported to the .net framework from Ruby’s [Liquid Markup](https://shopify.github.io/liquid/).

### [Nuget](https://www.nuget.org/packages/DotLiquidExtended)

[![Open Source Love](https://badges.frapsoft.com/os/mit/mit.svg?v=102)](https://opensource.org/licenses/MIT)
![Nuget](https://img.shields.io/nuget/v/DotLiquidExtended)
![Nuget](https://img.shields.io/nuget/dt/DotLiquidExtended)


```
Install-Package DotLiquidExtended

dotnet add package DotLiquidExtended
```

<hr/>


<hr/>

**Models**

```cs
// Person.cs
public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FamilyName { get; set; }
    public float Age { get; set; }
    public DateTimeOffset BithDate { get; set; }
    public IEnumerable<Phone> Phones { get; set; }
    public IEnumerable<Address> Addresses { get; set; }
    public Person()
    {
        Phones = new List<Phone>();
        Addresses = new List<Address>();
    }
}

// Address.cs
public class Address
{
    public string Country { get; set; }
    public string City { get; set; }
    public string MainStreet { get; set; }
    public string Info { get; set; }
    public string No { get; set; }
}

// Phone.cs
public class Phone
{
    public string Code { get; set; }
    public string Number { get; set; }
}
```

**Fake Data Generator**

```cs
// PeopleDataGenerator.cs
using Bogus;

public static class PeopleDataGenerator
{
    public static IEnumerable<Person> GetPeople(int count = 200)
    {
        var testPhone = new Faker<Phone>()
                .StrictMode(true)
                .RuleFor(p => p.Code, f => f.Address.CountryCode())
                .RuleFor(p => p.Number, f => f.Phone.PhoneNumber())
                ;
        var testAddress = new Faker<Address>()
                .StrictMode(true)
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.No, f => f.Address.BuildingNumber())
                .RuleFor(a => a.Info, f => f.Address.FullAddress())
                .RuleFor(a => a.MainStreet, f => f.Address.StreetAddress())
                ;
        var testPerson = new Faker<Person>()
                .StrictMode(true)
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Name.FirstName())
                .RuleFor(p => p.FamilyName, f => f.Name.LastName())
                .RuleFor(p => p.Age, f => f.Random.Float(1, 120))
                .RuleFor(p => p.BithDate, f => f.Person.DateOfBirth)
                .RuleFor(p => p.Phones, f => testPhone.Generate(15))
                .RuleFor(p => p.Addresses, f => testAddress.Generate(10))
                ;
        return testPerson.Generate(count);
    }
}
```

### `Liquid` related works

* [Fluid](https://github.com/sebastienros/fluid)
* [Scriban](https://github.com/scriban/scriban)
* [LiquidJS](https://liquidjs.com/)
* [ACE Editor](https://ace.c9.io/build/kitchen-sink.html) Change `Mode` to `Liquid`.