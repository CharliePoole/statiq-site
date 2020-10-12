Title: An Engine Extension for Running Failed Tests
Lead: "Part 1: Creating the Extension"
Published: 22 Sep 2016
Tags: [NUnit,It's the Tests]
---
In a recent online discussion, one of our users talked about needing to re-run the NUnit console runner, executing just the failed tests from the previous run. This isn't a feature in NUnit but it could be useful to some people. So... can we do this by creating an Engine Extension? Let's give it a try!

The NUnit Test Engine supports extensions. In this case, we're talking about a Result Writer extension, one that will take the output of a test run from NUnit and create an output file in a particular format. In this case, we want the output to be a text file with each line holding the full name of a failed test case. Why that format? Because it's exactly the format that the console runner already recognizes for the `--testlist` option. We can use the file that is created as input to a subsequent test run.

Information about how to write an extension can be found on the [Writing Engine Extensions](https://github.com/nunit/docs/wiki/Writing-Engine-Extensions) page of the NUnit documentation. Details of creating a ResultWriter extension can be found on the [Result Writers](https://github.com/nunit/docs/wiki/Result-Writers) page.

To get started, I created a new class library project called `failed-tests-writer`. I made sure that it targeted .NET 2.0, because that allows it to be run under the widest range of runtime versions and I added a package reference to the `NUnit.Engine.Api` package. That package will be published on <a href="http://nuget.org">nuget.org</a> with the release of NUnit 3.5. Since that's not out yet, I used the latest pre-release version from the NUnit project MyGet feed by adding `https://www.myget.org/F/nunit/api/v2` to my NuGet package sources.

Next, I created a class to implement the extension. I called it `FailedTestsWriter`. I added `using` statements for `NUnit.Engine` and `NUnit.Engine.Extensibility` and implemented the `IResultWriter` interface. I gave my class `Extension` and `ExtensionProperty` attributes. Here is what it looked like when I was done.

```csharp
using System;
using System.IO;
using NUnit.Engine;
using NUnit.Engine.Extensibility

namespace EngineExtensions
{
    [Extension, ExtensionAttribute("Format", "failedtests")]
    public class FailedTestsWriter : IResultWriter
    {
        public void CheckWritability(string outputPath)
        {
            using (new StreamWriter(outputPath, false, Encoding.UTF8)) { }
        }

        public void WriteResultFile(XmlNode resultNode, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath, false, Encoding.UTF8))
            {
                WriteResultFile(resultNode, writer);
            }
        }

        public void WriteResultFile(XmlNode resultNode, TextWriter writer)
        {
            foreach (XmlNode node in resultNode.SelectNodes("//test-case[@result='Failed']")) // (3)
                writer.WriteLine(node.Attributes["fullname"].Value);
        }
    }
}
```

The `ExtensionAttribute` marks the class as an extension. In this case as in most cases, it's not necessary to add any arguments. The Engine can deduce how the extension should be used from the fact that it implements `IResultWriter`.

As explained on the [Result Writers](https://github.com/nunit/docs/wiki/Result-Writers) page, this type of extension requires use of the `ExtensionPropertyAttribute` so that NUnit knows the name of the format it implements. In this case, I chose to use "failedtests" as the format name.

The `CheckWriteability` method is required to throw an exception if the provided output path is not writeable. We do that very simply by trying to create a `StreamWriter`. The empty using statement is merely an easy way to ensure that the writer is closed.

The main point of the extension is accomplished in the second `WriteResultFile` method. A `foreach` statement selects each failing test, which is then written to the output file.

### Testing the Extension

That explains how to write the extension. In Part 2, I'll explain how to deploy it. Meanwhile, I'll tell you how I tested my extension in it's own solution, using `nunit3-console`.

First, I installed the package <code>NUnit.ConsoleRunner</code> from <a href="http://nuget.org">nuget.org</a>. I used version 3.4.1. Next, I created a fake package subdirectory in my <code>packages</code> folder, so it ended up looking like this:

```csharp
packages
    NUnit.ConsoleRunner.3.4.1
    NUnit.Engine.Api.3.5.0-dev-03211
    NUnit.Extension.FailedTestsWriter
        tools
            failed-tests-writer.dll
```

Note that the new extension "package" directory name **must** start with "NUnit.Extension." in order to trick the console-runner and engine into using it.

With this structure in place, I was able to run the console with the <code>--list-extensions</code> option to see that my extension was installed and I could use a command like

```csharp
nunit3-console mytests.dll --result:FailedTests.lst;format=failedtests
```

to actually produce the required output.

---

### Comments

---

[&#8230;] An Engine Extension for Running Failed Tests – Part 1: Creating the Extension &#8211; (NUnit Blogs) [&#8230;]
>Compelling Sunday – 11 Posts on Programming and QA, Sunday, September 25, 2016
