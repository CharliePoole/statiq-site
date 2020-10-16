Title: "NUnit 2.2.3 Released"
Published: 15 Nov 2005
Tags: [NUnit,It's the Tests]
---
NUnit 2.2.3 is now out. You can get it at our <a href="http://nunit.sf.net">SourceForge site</a>. The release notes are available <a href="http://nunit.org/?p=releaseNotes&r=2.2.3">here</a>.

So far, only the .msi file is available. It's built with VS2003 but, as always, will run on .Net 2.0 - including the RTM. The zipped binaries and source will follow in the next few days, depending on how much connectivity I have while I'm travelling.

A version built against .NET 2.0 is in the cards, but we're still looking at alternative ways to organize the releases.

---

### Comments

---

For GertVE... 

If you have a BadImageFormatException, you are running NUnit under .Net 1.1. Look at the about box to see for sure.

To run under 2.0, you must either change the config file to uncomment the startup section OR use the /framework argument on the command line. Check the docs for more info.
>Charlie, Thursday, November 17, 2005

---

Loading a 2005 dll including NUnit test classes and methods does give a System.BadImageFormatException. 
Then config file does include "  " which is indeed our current .NET version.
Fyi, we are using 2005 Prof Edition so whe started from a class library project, this since MS Test projects only available with 2005 Team Editions. I would say this can not be the cause of the problem.
Shouldn't we build NUnit against .NET v2 first?
>GertVE, Thursday, November 17, 2005

---

[...] Charlie Poole has announced a new version of NUnit that support the recently released .NET 2.0 framework. [...]
>Roundtrip Solutions Blog &raquo; Blog Archive &raquo; NUnit Now Support .NET 2.0, Thursday, November 17, 2005

---

[...] I just noticed that Charlie Pool has anounced the release of NUnit 2.2.3. [...]
>idp &raquo; Blog Archive &raquo; NUnit 2.2.3 Released, Friday, November 18, 2005

---

[...] 1.8.5223.1 Based on theme design by Bryan Bell |&nbsp;&nbsp;        &nbsp;Sunday, 20 November 2005 Just Out: NUnit 2.2.3 NUnit team member Charlie Poole recently announced the release of NUnit 2.2.3 unit-testingframework.&nbsp; With this release, NUnit can now be installed and run under .NET 2.0 and used in conjunction with Visual Studio 2005. Read the release notes at:http://nunit.com/testweb/index.php?p=releaseNotes&amp;r=2.2.3 Download&nbsp;NUnit from:http://nunit.sf.net/&nbsp; (SourceForge)  &nbsp;&nbsp;&nbsp;&nbsp;Comments [0]&nbsp;&nbsp; [...]
>ASPNETWorld.com Blog - Just Out: NUnit 2.2.3, Sunday, November 20, 2005

---

NUnit 2.2.3 Released: Will run on .Net 2.0
>Steve Bargelt, Saturday, November 19, 2005

---

I've installed version 2.2.3 and 2.2.4 but both seem to only work with one source file in a dll. If I have two sourcefiles in one dll then only the tests in one source file can be seen (in nunit-gui for example). What do I do?
>Chris, Wednesday, December 21, 2005

---

NUnit has no info on your source files. Tests often involve hundreds of source files.

So if you're seeing a difference, it has to be due to what is or isn't in those source files. Can you create a small example?

Charlie
>Charlie, Wednesday, December 21, 2005
