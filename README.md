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

### In-Memory File System

By default `dotLiquid` just reads template files from disk via `include` functionality. `InMemoryFileSystem` helps you to use `include` method to load your templates from memory.


```cs
// In-memory templates
public static class Templates
{
    public static string GetTemplate1()
    {
        // You can use single qoutes too.
        // {% include 'tmpl2' %}
        return @"Hello, {{name}} {% include ""tmpl2"" %}.";
    }

    public static string GetTemplate2()
    {
        return @"{{family}}";
    }
}

// Template engine
using DotLiquidExtended;

Template.FileSystem = new InMemoryFileSystem(new()
{
    // Key, Value
    { "tmpl1", Templates.GetTemplate1() },
    { "tmpl2", Templates.GetTemplate2() }
});
Template template = Template.Parse(Templates.GetTemplate1());
var result = template.Render(Hash.FromAnonymousObject(
    new { Name = "Hamed", Family = "Fathi" }
));
```

![pic1](https://user-images.githubusercontent.com/8418700/145047229-04dac9cb-6f48-4f61-9f36-a876dc233386.png)

### Extensions

There are bunch of useful extensions as following:

```cs
using DotLiquidExtended;

// A simple way to set an anonymous object.
// new { YourObjectName = YourObject }
string RenderAnonymousObject(this Template template, object obj, bool inclueBaseClassProperties = false, IFormatProvider formatProvider = null)

// It sets 'RootObject' for your anonymous object.
// new { RootObject = obj }
// You have access to it inside a template via 'root_object'.
RenderObject(this Template template, object obj, bool inclueBaseClassProperties = false, IFormatProvider formatProvider = null)

// Returns all AST nodes recursively.
// e.g. template.GetAllNodes().Where(node => node is Variable) returns all variables.
IEnumerable<object> GetAllNodes(this Template template)
````

### Utilities

To access below utilities you should call `DotLiquidUtility` static class.

```cs
using DotLiquidExtended;

// You can register your types into the template engine.
void RegisterSafeTypes(params Type[] types)

// Automatically registers all types and properties of entry assembly.
// You can register all types of referenced assseblies too.
void RegisterSafeTypes(bool withReferencedAssemblies = false)

// Automatically registers all types and properties of an specified assembly.
void RegisterSafeTypes(Assembly assembly)

// Automatically registers all types and properties of all introduced assembelies.
void RegisterSafeTypes(IEnumerable<Assembly> assemblies) 
```

##### Render with Variable Validation

There is a `RenderWithValidation` method inside `DotLiquidUtility` to help you detect a variable with two below conditions:
1. Variable is `null`.
2. Variable does not exist.

```cs
RenderResult RenderWithValidation(string templateText, object data, Func<string, IEnumerable<string>, bool> ignoreValidationCondition = null)
```

In fact, this method simulate a strict variable/property checking because if `dotLiquid` is not able to find a variable just ignores it and renders nothing without any warning or error.

* `templateText`: You should pass your template a text.
* `data`: is your anonymous object to fill template. `new { YourObjectName = YourObject }`
* `ignoreValidationCondition` is a functionality that help you to ignore some variables or filters for checking.

```cs
// Func<string, IEnumerable<string>, bool> ignoreValidationCondition

// Filters are related to the current variable. 
(varName, filters) => {
    if(varName.Contains('name')) return true;
    if(filters != null && filters.Contains('nullable')) return true;
    return false;
}
```

Obviously, The entire variables with the exact `name` word in their names will ignore from checking same as all variables with `nullable` filter. e.g. `myVar | nullable`

By default you can use a pre-defined `ignore_safe_var`
 filter to skip this checking.

In the end you will see a result similar to the following class.

```cs
public class RenderResult
{
    // dotLiquid template object.
    public Template Template { get; set; }
    
    // Contains whole errors.
    // It will be null if there is no error.
    public IEnumerable<string> Errors { get; set; }
    
    // The final rendered result. 
    // It will be null if you have an error.
    public string Result { get; set; }
}
```

![pic2](https://user-images.githubusercontent.com/8418700/145047276-28c57fe3-b3c6-42d3-add1-fcc2feb3fa42.png)
 
### `Liquid` related works

* [Fluid](https://github.com/sebastienros/fluid)
* [Scriban](https://github.com/scriban/scriban)
* [LiquidJS](https://liquidjs.com/)
* [ACE Editor](https://ace.c9.io/build/kitchen-sink.html) Change `Document` and  `Mode` to `Liquid`.
