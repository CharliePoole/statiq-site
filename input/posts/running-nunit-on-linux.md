Title: "Running NUnit on Linux"
Published: 30 Jul 2006
Tags: [NUnit,Open Source,OSCON,It's the Tests]
---
At OSCON, I spoke about NUnit for Cross-Platform development. Here are some screenshots from that talk, showing NUnit running under Mono 1.1.13 on Ubuntu Linux.

<!--more-->
<table><tr><td>
<a class="imagelink" href="http://nunit.com/blogs/wp-content/uploads/2006/07/WindowsForms-Screenshot.png" title="NUnit GUI Running on Linux"><img id="image33" src="http://nunit.com/blogs/wp-content/uploads/2006/07/WindowsForms-Screenshot.thumbnail.png" alt="NUnit GUI Running on Linux" title="NUnit GUI on Linux"/></a>
</td><td>
Here's the NUnit Gui running under Linux. There are some bugs, but it basically works. This is the NUnit 2.4 Beta, the same code we run on Windows!
</td></tr></table>

<table><tr><td>
This shot shows the NUnit add-in for MonoDevelop running some tests. Unfortunately, it's still using NUnit 2.0.
</td><td>
<a class="imagelink" href="http://nunit.com/blogs/wp-content/uploads/2006/07/MonoDevelop-Screenshot.png" title="Running NUnit tests using MonoDevelop"><img id="image34" src="http://nunit.com/blogs/wp-content/uploads/2006/07/MonoDevelop-Screenshot.thumbnail.png" alt="Running NUnit tests using MonoDevelop" /></a>
</td></tr></table>

<table><tr><td>
<a class="imagelink" href="http://nunit.com/blogs/wp-content/uploads/2006/07/NUnitSolution-Screenshot.png" title="Building NUnit with MonoDevelop"><img id="image35" src="http://nunit.com/blogs/wp-content/uploads/2006/07/NUnitSolution-Screenshot.thumbnail.png" alt="Building NUnit with MonoDevelop" /></a>
</td><td>
Here we're actually building NUnit under MonoDevelop. We can't yet build the Gui due to some resource generation bugs, but the framework and console apps build just fine.
</td></tr></table>

Since we can't yet build the gui under Linux, we got it to run there by copying it from Windows. Here's how you can do it yourself...

1. Copy the contents of your NUnit bin directory to a directory on the Linux system. Depending on your setup, you can use a network connection or some portable medium like a USB key. The standard location for most systems will be a directory under /usr/lib. Don't overwrite any existing nunit directory, since it is most likely being used by an application like MonoDevelop. On my system, I copied the files to /usr/lib/nunit-2.4.

2. Create a script on your path somewhere to execute the gui. I called mine nunit and placed it in /usr/bin, which is a location used for similar scripts for other programs that run under mono. The script should look something like this

<blockquote>```csharp#!/usr/bin/sh
exec /usr/bin/mono /usr/lib/nunit-2.4/nunit.exe "$@"
```</blockquote>

If you don't have Linux, get a copy of <a href="http://mono-live.com/">MonoLive</a> CD creted by <a href="http://www.beyondfocus.com/">Joseph Hill</a>. If you have VMWare, he also provides a ready-to-use machine image on the same site. [Note: the monolive user password is monolive. You need this to perform operations with sudo.]

---

### Comments

---

**Charlie Poole @ OSCON**

Charlie Poole has just returned from OSCON and seems to be on a bit of a roll. He writes:NUnit on LinuxRunning
>TestDriven.NET by Jamie Cansdale, Sunday, July 30, 2006

---

**Charlie Poole @ OSCON**

Charlie Poole has just returned from OSCON and seems to be on a bit of a roll. He writes:NUnit on LinuxRunning
>M2, Sunday, July 30, 2006
