Title: "Running NUnit on Linux"
Published: 30 Jul 2006
Tags: [NUnit V2,Open Source,OSCON]
---
At OSCON, I spoke about NUnit for Cross-Platform development. Here are some screenshots from that talk, showing NUnit running under Mono 1.1.13 on Ubuntu Linux. (Click to enlarge)

<!--more-->
<div class="pic odd">
  <a href="/images/nunit-gui-on-ubuntu.png">
    <img src="/images/nunit-gui-on-ubuntu.png" alt="NUnit GUI Running on Linux" style="max-width: 400px;"/>
  </a>
  <p>Here's the NUnit Gui running under Linux. There are some bugs, but it basically works. This is the NUnit 2.4 Beta, the same code we run on Windows!</p>
</div>

<div class="pic even">
  <a href="/images/running-nunit-under-monodevelop.png">
    <img src="/images/running-nunit-under-monodevelop.png" alt="Running NUnit tests using MonoDevelop" style="max-width: 400px;"/>
  </a>
  <p>This shot shows the NUnit add-in for MonoDevelop running some tests. Unfortunately, it's  still using NUnit 2.0.</p>
</div>

<div class="pic odd">
  <a href="/images/building-nunit-under-monodevelop.png">
    <img src="/images/building-nunit-under-monodevelop.png" alt="Building NUnit with MonoDevelop" style="max-width: 400px;"/>
  </a>
  <p>Here we're actually building NUnit under MonoDevelop. We can't yet build the Gui due to some resource generation bugs, but the framework and console apps build just fine.</p>
</div>

Since we can't yet build the gui under Linux, we got it to run there by copying it from Windows. Here's how you can do it yourself...

1. Copy the contents of your NUnit bin directory to a directory on the Linux system. Depending on your setup, you can use a network connection or some portable medium like a USB key. The standard location for most systems will be a directory under /usr/lib. Don't overwrite any existing nunit directory, since it is most likely being used by an application like MonoDevelop. On my system, I copied the files to /usr/lib/nunit-2.4.

2. Create a script on your path somewhere to execute the gui. I called mine nunit and placed it in /usr/bin, which is a location used for similar scripts for other programs that run under mono. The script should look something like this

```bash
#!/usr/bin/sh
exec /usr/bin/mono /usr/lib/nunit-2.4/nunit.exe "$@"
```

If you don't have Linux, get a copy of the MonoLive CD created by Joseph Hill. If you have VMWare, he also provides a ready-to-use machine image on the same site. [Note: the monolive user password is monolive. You need this to perform operations with sudo.]

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
