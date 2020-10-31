Title: "NUnit for .Net 2.0 - Do We Really Need It?"
Published: 21 Nov 2005
Tags: [NUnit V2,TDD]
---
The NUnit 2.2.3 release contains two versions of each download: one built with .Net 1.1, one with .Net 2.0. So, what's the deal? Do we need separate versions? And which version do **you** need? I'll try to explain...

<!--more-->
Every .Net program you write works with a particular version of the CLR - the common language runtime. Of course, it's possible to write programs that work with multiple versions of the CLR (NUnit itself is one such program), but most programmers writing normal business applications are happy to stick with just one.

The point of all this for testing, is that you would like your program to be tested under the CLR version for which it was written. But NUnit does nothing to select a runtime version for your program - as it might if it were actually hosting the runtime. NUnit simply executes your tests using the same runtime, _under which it is already running_.

So who decides which version of the CLR is used for NUnit? Essentially, you do...

If you just run NUnit - let's say the Gui - without taking any other action, it will execute under the runtime version for which it will built. If you are using the standard downloaded packages - the ones without "Net-2.0" in their names - this will be version 1.1 of the framework. If you're running one of the "Net-2.0" packages, this will be version 2.0. In particular, the "Net-2.0" packages are built using the .Net 2.0.50727.

If you edit the config file - nunit-gui.exe.config, for example - to remove the comments around the &lt;startup&gt; section, then the first CLR version listed in that section will be used, if it is present. If not, then the second will be tried, then the third, and so forth.

If you set the COMPLUS_Version environment variable to a string similar to "v2.0.50727" then that version will be used if it is present. You can accomplish the same thing by running the NUnit gui with "/framework:v2.0.50727" as a command-line option. In the latter case, NUnit will spawn a new copy of itself, if necessary, in order to run under the requested framework.

So, if it's possible to run the original, built-with-net-1.1 version of NUnit under any version of the CLR, why do we need a separate .Net 2.0 version? Strictly speaking, we don't. But it's convenient for a few reasons:

1. Folks seem to have an inordinate amount of trouble getting NUnit to run under the proper framework version. Having a special version will make their lives a bit easier, not to mention mine, since I have to answer all the questions.

2. Eventually, we will need a separate version. It's inevitable that people will begin to use 2.0-only features in their tests. Imagine an Assert on a nullable value. Or a generic TestFixture class. We'll be able to deal with some of those things from an NUnit built with .Net 1.1, but many of them will either require .Net 2.0 or be much simpler to implement with .Net 2.0.

For now, the .Net 2.0 builds are identical in features to the .Net 1.1 builds. We have reflected this in **not** changing the version numbering for now. Those using one or the other framework version exclusively can download a copy of NUnit built to use that version without missing out on any features. Those using both versions have the choice of installing both versions side by side - just be careful which one you reference - or using the command-line /framework option to select the correct version on the fly.

---

### Comments

---

[...] NUnit for .Net 2.0 - Do We Really Need It? - Overview of getting NUnit to work with multiple versions of the .NET framework. [...]
>The DaxMindMapper Reloaded &raquo; NUnit and v2 (or other versions) of the .NET framework, Tuesday, March 20, 2007
