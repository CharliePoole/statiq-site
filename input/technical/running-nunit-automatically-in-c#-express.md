Title: "Running NUnit Automatically in C# Express"
Published: 27 May 2006
Tags: [NUnit V2]
---
A post on the **TDD** list asks about running NUnit tests using the Start button in Visual C# Express. In Visual Studio 2003 and 2005, you can set up a project's Debug properties so that an arbitrary external program is executed. By selecting NUnit as that program and providing the correct command-line options, you can cause your tests to be run.

<!--more-->
But in Visual C# 2005 Express, the only choice you have is to execute one of your own programs, with the result that this approach to running tests is unavailable. Of course, you could always run NUnit separately, as many of us do. Or you could set it up as an external tool using the **Tools | External Tools... menu item**, which works the same in the Express version as in the others.

But if you're **really attached** to the approach using the Start button, all is not lost. You merely need a project of your own that runs the tests under NUnit. This is much easier than you might imagine. Just create an Windows executable and add a reference the nunit-gui-runner assembly. Replace the contents of **Main()** with a call to **AppEntry.Main()** with the same arguments you were passed. Here's what it looks like...

```csharp
using System;
using System.Windows.Forms;
namespace WindowsApplication2
{
    static class Program
    {
        ///
        /// The main entry point for the application.
        ///
        [STAThread]
        static void Main(string[] args)
        {
            NUnit.Gui.AppEntry.Main(args);
        }
    }
}
```

Make this project your startup project and set the command-line to point to the output of the project containing your tests. Use the /run option and any other options you like. When executed, this program will launch the NUnit GUI and run your tests.


---

### Comments

---

Right after I posted this article, I saw a reply on the TDD list, posted by Errol Thompson. He explains that C# Express will actually run an external program, if you can get the proper entries into the .user file for the project. You can read his post here: http://thread.gmane.org/gmane.comp.programming.test-driven-development/15039
>Charlie, Saturday, May 27, 2006

---

[...] What we have to do is explained on Charlie Poole&#8217;s blog&nbsp;on nunit.com entitled Running NUnit Automatically in C# Express.&nbsp; You basically fire up the GUI from inside a project.&nbsp; Hence the reason for our Kenthall.TestHelper assembly, and that&#8217;s all it does for now. [...]
>Debugging NUnit test within C# Express &laquo; Kenthall Development Team, Saturday, September 9, 2006

---

[...] Zdroje: Charlie Poole, Stewart Robertson [...]
>Jirka bianco Vágner &raquo; Debug unit testu pomoci Visual C# Express, Sunday, October 8, 2006

---

**Efficiently testing with NUnit and log4net**

Hello!
I have used Nunit for testing .NET based software (libraries) for quite some time now  but I have not been entirely happy due to three reasons:

The only way to execute the tests was through nunit-gui or nunit-console. Both of these programs rea...
>EmPowerTec blog, Monday, April 16, 2007

---

[...] C# Express&nbsp;NUnit  C# Express does not give you the ability to run through your nunits in debug. To mitigate your test-driven developments hassles, I came across this little workaround gem&#8230;. http://nunit.com/blogs/?p=28 . You&#8217;ll have to mess around with the arguments after that but you get the point. [...]
>C# Express NUnit &laquo; Life and times of the Mighty Anger, Sunday, January 21, 2007
