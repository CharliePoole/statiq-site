Title: "Ten Reasons to Try NUnit 2,5"
Published: 29 Apr 2009
Tags: [NUnit V2]
---
NUnit 2.5 has so many new features (see the [release notes](http://docs.nunit.org/2.5/releaseNotes)) that I thought I'd try to come up with my top-ten favorites. It was hard to get down to ten, but here's what I came up with...

**Reason 1: Data-Driven Tests**

Users of mbUnit and xUnit.net have enjoyed the flexibility that data-driven (aka parameteried) tests provide for some time. NUnit implements this paradigm in its own way, with it's own set of attributes. Test methods may have arguments and the data for them may be supplied in a number of ways: inline, from a separate method or class or randomly. This feature gives you a succinct way to express a set of examples to be used in running individual test cases.

**Reason 2: Theories**

As used in NUnit, a Theory is a generalized statement of how a program should operate, like "For any positive number, the square root is defined as the positive or negative number, which, when multiplied by itself, gives the original number." Traditional, example-based, testing allows you to select one or more sets of values to use in testing such a program. A **Theory**, on the other hand, allows you to express the generalization itself, writing a test that will pass for whatever values are passed to it, provided they meet the stated constraints. David Saff has written a number of papers about the use of Theories in testing and has implemented this construct as a part of JUnit. Now you can use the same construct in any .NET language.

**Reason 3: Inline Expected Exception Tests**

Testing that an expected exception is thrown correctly has always been an issue in NUnit. The **ExpectedExceptionAttribute** has been available since early releases but has a number of problems. It tests that the exception was thrown somewhere in the test, without specifying the exact place in the code, and it is subject to the syntactic limitations that apply to use of an attribute. With the introduction of the **Assert.Throws** assertion method and the even more powerful constraint expressions **Throws.Exception**, **Throws.InstanceOf** and **Throws.TypeOf**, exception testing logic can now be moved right into the test along with any other necessary assertions.

**Reason 4: Generic Support**

NUnit 2.5 provides separate framework assemblies for .NET 1.x and 2.0+. Using .NET 2.0 or higher, a number Up to 2.4, NUnit avoided any use of Generics, in order to maintain backward compatibility. In 2.5, the framework assembly used under .NET 2.0 provides a number of generic Asserts and Constraint expressions for convenience. More significantly, your test methods and classes may now be generic, and NUnit will specialize them using the types you provide.

**Reason 5: Lambda Support**

If you write your tests using C# 3.0, you may use Lambda expressions in a number of places where NUnit expects a delegate. This is particularly useful in providing a custom definition of equality without explicitly defining an IComparer&lt;T&gt; and can even be used to apply an arbitrary predicate to the members of a collection.

**Reason 6: Out-of-Process Execution and Runtime Selection**

NUnit 2.4 ran all tests within the same process, using one or more AppDomains for isolation. This works fine for many purposes, but has some limitations. NUnit 2.5 extends this concept to running tests under one or more separate processes. Aside from the isolation it provides, this allows running the tests under a different version of the .NET runtime from the one NUnit is currently using.

**Reason 7: PNUnit**

PNUnit stands for "parallel NUnit" and is an extension developed by Pablo Santos Luaces and his team at Codice Software and contributed to NUnit. It's a new way to test applications composed of distributed, communicating components. Tests of each component run in parallel and use memory barriers to synchronize their operation. Currently, pNUnit uses a special executable to launch its tests. In the future, you will be able to run pNUnit tests from the standard NUnit console or gui runner. See [their documentation](https://www.plasticscm.com/documentation/technical-articles/pnunit-parallel-nunit) for details.

**Reason 8: Source Code Display**

The new stack trace display in the Errors and Failures tab of the Gui is able to display the source code at the location where a problem occured, provided the source is available and the program was compiled with debug information. Currently, syntax coloring for C# is provided and other languages are treated as simple text, but additional syntax definitions will be available in the future.

**Reason 9: Timeout and Delayed Constraints**

These are two separate features, but they are related. Besides, I'm working hard to keep this down to only ten points! It's now possible to set a timeout, which will pre-emptively fail a test to fail if it is exceeded. This may be done on a method, fixture, assembly or as a global default. On the other hand, if you need to wait for an action to take place after a delay, you can use the After syntax to delay the application of the constraint. NUnit will subdivide a long delay and apply your test repeatedly until the constraint succeeds or the specified amount of time is up!

**Reason 10: Threading Attributes**

In past releases, if any test needed to run in the STA, the entire test run had to use the STA. With 2.5, any method, fixture or assembly may be given an attribute that causes it to run on a separate thread in the STA. Other attributes allow requiring an MTA or simply running on a separate thread for isolation. This can eliminate a lot of boilerplate code now required to create a separate thread, launch it and capture the results for NUnit.

This is my own list, of course. Yours may vary. [Download the release](https://sourceforge.net/projects/nunit/files/NUnit%20Version%202/V2.5/)</a>, try it out and let me know what your own favorites are.

---

### Comments

---

[...] 2.5 erschienen und steht zum Download bereit.Â Die Top 10 Neurerungen beschreibt der Enwickler in seinem Blog. Details zu allen Ã„nderungen finden sich in den Release Notes.   Share and [...]
>NUnit 2.5 erschienen | LieberLieber Software TeamBlog, Wednesday, May 6, 2009

---

**NUnit Framework for .NET Compact Edition and&nbsp;Silverlight...**

Unofficial NUnit Framework port for .NET Compact Edition and Silverlight. Only nunit.framework project. Now Add NUnit-2.5.3.9345 for .NET CF 3.5 and Silverlight 3.0. Please checkout and build via Visual Studio 2008 yourself. Visit the project site here...
>Iron9light's Tech Weblog, Friday, February 26, 2010

---

[...] UPDATE: Charlie Poole posted an explanation of each point on his blog. [...]
>NUnit 2.5 Release Candidate now available &laquo; SÃ©bastien Lorion, Friday, May 1, 2009

---

[...] has awesome new features such as data driven tests! See http://nunit.com/blogs/?p=66 for more information. That means I can now do the things only possible before in other frameworks [...]
>Did you know NUnit 2.5 is out? &laquo; Arrange Act Asssert, Wednesday, July 1, 2009

---

[...] has awesome new features such as data driven tests! See http://nunit.com/blogs/?p=66 for more information. That means I can now do the things only possible before in other frameworks [...]
>Did you know NUnit 2.5 is out? &laquo; Arrange Act Assert, Tuesday, June 23, 2009
