Title: "About NUnit 2.2.4... Whoops... Make that 2.2.5"
Published: 25 Dec 2005
Tags: [NUnit,TDD,It's the Tests]
---
I've been occupied with other things and not blogging lately. Meanwhile - in fact this is one of the things I've been occupied with - NUnit 2.2.4 was released. People tried it and found a few bugs - NUnit 2.2.5 corrects them.

<!--more-->
NUnit 2.2.5 is the latest, stable, production release of NUnit. Putting out bug fixes on a stable release base is a new thing for the NUnit project. Up to now, we have attempted to maintain a single code line, with bug fixes, new features and experimental features all coming out at once. This is handy for those who are on the leading edge of NUnit development, but turns out to be less useful for folks who just want the bugs fixed.

The new (to us) approach will be to maintain separate production and development code lines. We'll put out critical fixes to the production releases, but won't add features. Stable (production) releases will be designated by an even minor version number, unstable (development) releases by an odd one. This approach seems to have paid off already, as we were able to release NUnit 2.2.5 only two weeks after 2.2.4.

NUnit 2.2.4 and 2.2.5 are the first production releases since NUnit 2.2 was released in August, 2004. NUnit 2.2.5 is our current recommended release. Here are a few highlights of what's new in these releases as compared to 2.2...

* NUnit now runs under .Net 2.0 and works with Visual Studio 2005. There's even a separate download that is built with .Net 2.0, although it's not strictly needed at this time - there are no differences in the code yet.
* You can now run your old tests - built against earlier versions of NUnit 2.x - without recompilation.
* A number of new Asserts have been added... AreNotEqual, AreNotSame, Greater, Less, IsNaN, IsEmpty, IsNotEmpty, IsInstanceOf, IsNotInstanceOf, IsAssignableFrom, IsNotAssignableFrom, Contains, StartsWith, EndsWith, AreEqualIgnoringCase. It's much easier now to create custom Asserts to meet your own needs.
* An extensibility mechanism allows you to define your own attributes for test fixtures and test cases and cause them to behave in non-standard ways. [This feature is still a bit experimental, and will appear in final form in the 2.4 release.]
* Documentation is substantially improved and is provided as a set of html files. The packaged documentation includes only version-specific
information, with details that may change over time, such as contacts, kept on the web site.

If you <a href="http://nunit.org/?p=download">download</a> the latest release, you'll notice other changes as well. And more is coming. See the <a href="http://nunit.org/?p=roadmap">NUnit Roadmap</a> for details.

---

### Comments

---

[...] Misc. Links and odd ends Scott is so totally wrong - it hurts to read. He thinks&nbsp; FAR&nbsp;and xplorer2 are the best explorer replacements ever, but personally I think once he tries Total Commander, he'll never go back (the UI looks like an oldie, but it has more features than both combined, methinks) &nbsp; Oh, and have I mentioned how much I like Larkware yet? I like it a lot! Unfortunately, I haven't had enough time lately to check it out, so here's a collection of links posted there in the last month or so, which I find interesting:  cl1p.net - Make up your own URL, paste in data, go to another machine, visit the URL, copy the data back. An easy way to use HTTP to transport data between computers. csUnit 2.1.1 BETA - This unit-testing tool for .NET now supports .NET 2.0 and Visual Studio 2005.  Automise - VSoft has released their general-purpose automation utility, with a GUI based on the fantastic FinalBuilder. You can download a 30-day eval copy here, or license it starting at $195 In other news  NUnit 2.2.5 ReleasedThis is an update to the recent 2.2.4 release, the first new production release of NUnit in over a year. See&nbsp;this blog entry for a summary of features and links to the release Ohad keeps making his Beat-Box Demo cooler and cooler.It's for Tech-Ed Israel and demonstrates creating a beat mixing application that uses EntLib for all it's backend stuff. &nbsp;I saw this live and this is indeed a demo for the books. In fact, he should distribute it after Tech-Ed Israel with source, because this is one cool demo! That's it. Nothing else interesting happening out there for us .NET techies.  Really. I swear.  Published Wednesday, May 03, 2006 11:04 PM by Royo [...]
>ISerializable in Israel : Misc. Links and odd ends, Wednesday, May 3, 2006
