Title: "How NUnit Finds Config Files"
Published: 30 Sep 2005
Tags: [NUnit, It's the Tests]
---
Lots of people write to ask about the use of configuration files with NUnit. Usually, they have put some setting in a config file - somewhere - and are wondering why NUnit doesn't seem able to find it. In this post, I'll try to summarize the main issues around NUnit's use of configuration files. It really isn't all that complicated, once you understand a few things...

<!--more-->
**1. NUnit does not read config files!**

It's actually the .NET framework itself that reads a properly named config file when an application is loaded. By default, it looks for a config file named program.exe.config. Of course, when you are running your tests, you are running the NUnit application and the config file used is either nunit-console.exe.config or nunit-gui.exe.config.

Usually, you won't need to change either of these config files, but it could be necessary if you are trying to run NUnit under a different version of the .NET framework. In particular, you won't put any test settings in these configs, because...

**2. Your tests cannot "see" nunit-gui or nunit-console config files.**

When NUnit runs your tests, it creates a separate AppDomain in order to give them a different set of directories for finding assemblies and a different configuration file from NUnit itself. NUnit tells the .NET framework the name of the configuration file to use. The name of the file varies depending on how you load your tests...

* If you load a test assembly directly, such as mytests.dll, the config file must be named mytests.dll.config.

* If you load an NUnit project, such as mytests.nunit, the config file must be named mytests.config.

* If you use NUnit's Visual Studio support to load a Visual Studio project or solution, such as mytests.csproj or mytests.sln, the config file must be named mytests.config.

In all of these cases, the config file must be in the same directory as the assembly or project that you are loading.

**You might wonder** why NUnit works this way. Placing the test settings in a file separate from the NUnit config files is necessary so you may have a separate config for each project and to minimize the chance of errors in the NUnit config. We use two separate naming conventions for assemblies and projects to allow different settings for these different kinds of test runs. However, the naming issue has ended up causing a lot of confusion and will probably be phased out in a future release.

**If you aren't sure** whether your config file is being used, a good trick is to put a special setting in it and write a test to see if that setting can be retrieved. For example,

```csharp
[Test]
public void CheckConfigFile()
{
    Assert.AreEqual( "1234565", ConfigurationSettings.AppSettings["configCheck"] );
}
```

---

### Comments

---

Superb, it works a treat.

On and off I've been battling this for some time, and had no success, this is just what was needed.

Thanks :-)
>anonymous, Thursday, October 20, 2005

---

Awesome, I've been trying to test my db connection with little success. Great Posting
>Anonymous 2, Monday, November 28, 2005

---

This is just what was needed , especially  the trick .Clear and simple. Thanks
>.Net Adventures, Sunday, December 25, 2005

---

Thanks for the tip.  Worked like a charm!
>.Net Rookie, Tuesday, January 10, 2006

---

I've had a lot of problems with NUnit picking up the right config file. Basically by examining AppDomain.CurrentDomain.SetupInformation.ConfigurationFile in Debug mode it appears NUnit will always expect the config file name to follow the NUnit project file name. This is regardless of your C# project name being used in the solution or the Configuration setting you have put into NUnit itself e.g. If your NUnit project file in called NUnitTests then the config file MUST be called NUnitTests.config otherwise it is ignored.
>John, Tuesday, April 11, 2006

---

For Andre: In the latest development code - to be released as NUnit 2.4 - nunit-console will run each assembly in a separate AppDomain and use separate config files.
>Charlie, Wednesday, May 3, 2006

---

Thanks Charlie, that will be great.
>andre, Thursday, May 4, 2006

---

Thanks. It has rescued me from pain! :)
>turgay, Tuesday, May 2, 2006

---

Thanks for this post and for all the great work on NUnit.
An unexpected (by me) behavior is that if more than one assembly is specified on the command line (for example: nunit-console.exe assem1test.dll assem2test.dll )
Nunit will generate a temporary project for the collection of assemblies and will look for a configuration file named after the generated project file (for example: Project1.config)
I was naively calling nunit in an msbuild task for all the unit test assemblies in a combined directory (all the dlls end up in a common directory) and could not figure out why the config files were not being read.
>andre, Wednesday, May 3, 2006
