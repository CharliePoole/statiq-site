Title: "NUnit Extensibility - A High Level View"
Published: 10 May 2006
Tags: [NUnit V2,TDD]
---
NUnit originally identified tests in the time-honored way that is still used by most xUnit frameworks. Test classes inherited from the framework's TestCase class. Individual test case methods were identified by their naming pattern.

<!--more-->
With NUnit 2.0, we introduced the use of attributes to identify both fixtures and test cases. This was heralded by many as an advance - and indeed it was. Use of attributes for identifying test cases is a natural outcome of their presence in .NET.

However, one thing was lost in the transition. Test frameworks based on inheritance - like the original JUnit  - have a simple, readily available mechanism for extending test cases and fixtures: inheritance itself. Because each user fixture **is_a** TestCase, it's quite easy to modify test behavior by extending TestCase.

On the other hand, NUnit's TestCase, TestSuite and other classes are purely internal, and are not designed to be inherited from by user fixtures. They construct and manage the user fixture objects, but are not visible to the user tests. Of course, it is possible to inherit from these internal classes and introduce new behavior. But, without changing NUnit itself, there has been no way to induce the test runner to make use of the new functionality you have created.

Beginning with NUnit 2.2.1, we introduced an experimental extensibility mechanism that would attempt to make up for the loss of inheritance-based extensibility in NUnit. Users have tried it and we have made substantial changes as a result of their feedback and our own experiments.

With NUnit 2.4, the notion of extensibility becomes a mainstream NUnit feature. In this blog entry, I'll attempt to give a high-level view of what we mean by extensibility. I'll also identify those parts of extensibility that we expect to release with NUnit 2.4 and those that are more futuristic.

**What is Extensibility**

People use the terms extension and extensibility in various ways. For example, some folks take a copy of the NUnit source and modify it, referring to this as "extending" NUnit. While there's nothing intrinsically wrong with this usage, we have something else in mind when we use the term.

An NUnit Extension - as we use the term - is a piece of code that works with NUnit to produce new functionality in the creation, selection and running of tests without changing the code of NUnit itself. An Extension does not have to be packaged with NUnit, but can be installed separately.Writers of extensions do not need to rebuild NUnit or even have the source code - although it might help. They work with the a binary release, which helps guarantee that their extension will be useful to other folks using the same release.

**Three Types of Extensions**
We plan to support three different kinds of Extensions:

* Framework Extensions
* Core Extensions
* Client Extensions

**Framework Extensions** are new facilities that are referenced and used by the tests. Examples of such extensions are new kinds of Asserts, domain-specific test libraries, helper objects for accessing private methods, etc. Such extensions - like the framework itself - know nothing of NUnit internals and are only called from user code. NUnit does nothing special to activate them, since their use in the test code causes them to be loaded without any special action.

**Core Extensions** are new facilities provided to the NUnit core to help it in loading and running tests. These are often referred to as **add-ins** because they must be located by NUnit and added to a list of active core extensions in order for them to be activated. The 2.4 alpha release will support three sorts of core extensions, with a fourth likely to be added in the future:

* Test Case Builders
* Test Suite Builders
* Test Decorators
* Test Event Handlers (NYI)

These extensions are generally used to provide new types of test fixtures and test cases. Often they will make use of new attributes intended for use in the tests themselves.
**Core Extensions** are installed by copying the extension assembly into the NUnit **bin\addins** directory. No other action is needed. It's possible that we will want to complement this simple approach with use of an XML file that lists extensions, deactivates them, etc. However, we won't do so until we actually see the need - <a href="http://www.xprogramming.com/Practices/PracNotNeed.html">YAGNI</a> after all.

While **Core Extensions** do their work in the test AppDomain, **Client Extensions** are loaded into the client domain - that of the nunit-gui process, for example. They work by intercepting events that occur during the loading and running of tests and taking various actions. In general, we expect these extensions to be used for reporting purposes, for controlling where and how tests are run and for other high-level tasks.

**Availability**
The alpha release of NUnit 2.4, which is planned for  mid-May, will support **Framework Extensions** and the first three types of **Core Extensions.** Examples of each type of extension are already working and several samples will be released as part of NUnit. Additional add-ins will be released separately immediately after the NUnit release. We expect to maintain a central registry of extensions as well as a distribution site for those who choose to use it.

No actual work has been done on **Client Extensions** at this point. We'll wait to see how our **Core Extensions** are received before putting further effort on the client side. I've listed this type here to call attention to the fact that many enhancements are better handled on the client side rather than the test side. This distinction will become increasingly imortant when NUnit is able to run tests across a network.

I'll be blogging separately on how the various types of extensions work.

---

### Comments

---

What's this all about?
http://www.codeplex.com/Wiki/View.aspx?ProjectName=NUnitV3
>cs, Sunday, May 14, 2006

---

redirects to the homepage
>js, Monday, May 15, 2006

---

For cs and js: I wrote a blog about codeplex so you could comment there if you like. This one is about NUnit Extensibility.
>Charlie, Monday, May 15, 2006

---

Nick: With current NUnit, you can only do this through various workarounds - like saving all your error messages until the end of the test. The notion that an Assert failure terminates the containing test is pretty fundamental to NUnit, so you're correct in wanting each of your data lines to correspond to an NUnit-generated test.

The extensibility mechanisms could easily do this. Whether you wanted to use a TestSuiteBuilder, TestCaseBuilder, TestDecorator or some combination would depend on the exact syntax you selected to express this.

In your blog example, however, there is a bit of syntax which will not work with NUnit's architecture. Tests are started and interpreted by the NUnit core which is not supposed to be referenced by user tests. [There's one current exception to this in the creation of test suites, but it will be modified eventually to no longer depend on the core.] The NUnit Framework and Core components are completely isolated from one another, which is what allows us to make use of tests written against older framework versions. Any extensions have to enforce a similar separation... the parts that reference and are loaded by the NUnit Core unless you are willing to give up this backward compatibility.

The hypothetical StartTest method would need to reach into the core, either directly or indirectly, in order to have any affect. So that's approach doesn't seem workable. What NUnit can do pretty well is recognize attributes on a test and run it multiple times. There's an example in the samples directory (2.4 Alpha) of a RepeatAttribute, which runs the same test multiple times. It's written to stop on the first failure, but it could be changed to keep running and report each failure separately.
>Charlie, Monday, May 29, 2006

---

Is it possible to dynamically create tests?

See here for an example. 

http://coding.leaton.net/index.html

Nick
>Nick, Monday, May 29, 2006

---

In relation to my blog entry http://mattadamson.blogspot.com/2006/08/unit-tests-executing-over-multiple.html I would like to plug in to the nunit framework so I can conditionally execute some tests based on a custom attribute applied to the test method to specify the version number of the application the test method can execute against.

Would these extensions support this?
>Matt Adamson, Thursday, August 3, 2006

---

Matt: Since your email hit me first, I commented in detail on your blog. It seems to me like a worthwhile effort to figure out whether and how our plans for extensibility would let you do this. As I mentioned in my comments, we would probably not ourselves write an extension that is specific to database testing, but we might provide the necessary hooks for somebody else to do it.
>Charlie, Thursday, August 3, 2006

---

Charlie,

Thanks for your explanation and suggestions. It gives me the info I needed to start on the right track. I’ll give it a try in the coming weeks and I will keep you posted.

Regards,

Jeroen
>Jeroen van Menen, Monday, August 28, 2006

---

Hi Charlie,

I'm the main developer of WatiN (a C# framework to test web apps through .Net). Automating Internet Explorer requires that the code automating the DOM runs on a single threaded apartment. So writing tests with NUnit requires specifieing the STA apartment state in the test assembly config file. NUnit uses this setting to set the apartmentstate of the Thread that runs the tests. I have just recently discoverd this possibility and I was wondering about another way to implement this behaviour. I was thinking of an extra (optional) parameter on the TestFixture attribute to explicitly state the apartmentstate to use when running tests in a TestFixture (like MBUnit does). The syntax would be something like:

[TestFixture(ApartmentState.STA)]
public class STATests
{
  [Test]
  public void STATestGoesHere()
  {
    ....
  }
}

Would it be possible to do this with the new extension support in 2.4 or does this require changes to nunit.core. Do you have some pointers for me, I really want to make this work so users of WatiN (and others who require STA for their tests) can more easely create their test in NUnit.

Regards, 

Jeroen
>Jeroen van Menen, Sunday, August 27, 2006

---

Jeroen,

First, let me deal the easy part: the proposed Syntax. TestFixtureAttribute is part of NUnit so you clearly can't change its constructor or add a property to it without changing NUnit. You would have to define a new attribute - e.g. ApartmentStateAttribute - to do this as an add-in. Even inside NUnit, it's generally cleaner to add new attributes than it is to modify existing ones.

Now for the semantics...

NUnit - as you have discovered - runs tests in the GUI on a separate thread. Optionally, it can use a separate thread when you use the console runner, by use of the /thread argument on the command line. That thread can have its apartment state initialized when it is created, using a value in the test configuration file.

Of course, once you have a thread, and have set the apartment state, you can't change it again, so any tests that need to be run using a different setting will require a separate thread. There's overhead involved here, but that's not a problem if the feature is only used where needed.

Internally, NUnit runs tests like this...

    TestRunner --> TestRunner --> ... --> TestRunner --> TestSuite --> TestSuite --> ... TestFixture --> TestCase

That's oversimplified, but hopefully clear: The chain of test runners add multiple capabilities to how/where tests are run and the TestSuites, fixtures and cases provide the organization and content for the tests. In a typical, "vanilla" execution under the gui, the runner chain might look like this...

   TestLoader --> TestDomain --> RemoteTestRunner --> ThreadedTestRunner -->SimpleTestRunner --> ....tests...

As the name suggests, ThreadedTestRunner is the one that creates the test execution thread.

Note that test runners do not iterate over the tests and execute them. Rather, the tests execute themselves. The intent here is that a test should be ignorant of where or how or by whom it is being run.

If we want to run different tests in different threads, we'll have to give some responsibility to the test itself to (1) create that thread (2) assign the apartment state appropriately and (3) transmit any exceptions back to the caller, since exceptions not handled on a thread are otherwise lost. That logic could be either in the NUnit definition of how a test operates or in a derived class of test that is created by an extension. Note that this could NOT be done as an extension as currently defined if it required introduction of a new TestRunner.

There are lots of variations as to how this might be done... We could have an attribute that calls for a separate thread per fixture - or even per test case. We could have an attribute that calls for a separate STA thread, ONLY if we are not already on an STA thread. You can probably think of others.

This is the situation that extensibility is intended for. We can easily have multiple extensions that work in different, even contradictory ways. Those that have the most general use can be "promoted" to be part of the extension assemblies that are shipped with NUnit, or even moved into the core itself. 

I'll assume that you want a separate STA thread to be created unless you are already running on one, since it's easy to take out that logic if you always want one. Here's how I'd approach the extension...

I'd make it a TestDecorator, an extension that takes an already created test and adds some new behavior to it. The test decorator in this case would look at it's input test, see if it's a test fixture and if it has the defined attribute. If not, it would just return the original test. Otherwise, it would create a new TestFixture- or TestSuite-derived object, aggregating the original test. I'll call it STAFixture here for clarity

The new STAFixture class would need to examine the current thread apartment state to decide whether to create a thread. Note that this cannot be done in the decorator itself, since it runs at a different point in time and the thread is needed at test execution time. In fact, it's possible that the loading of the tests will be on a different thread from the test execution. If a new STA thread is not needed, run can just return the result of running the aggregated fixture.

If a new thread is needed, you will want to create it, set the apartment state and run the aggregated fixture in that thread. Any exception will need to be passed back to the original thread for re-throwing. NUnit includes a class, TestRunnerThread, that may be helpful for this. It's probably not set up correctly for reuse, but we could change that or you could simply copy it and use it.

A final pointer... IF you define your new attribute in the same assembly as the TestDecorator and STAFixture, you will create an indirect reference to the NUnit core in your tests. That's bad and can lead to problems where two copies of the core are loaded in your test domain and types that you expect to be equivalent are no longer equivalent. An easy way to avoid this is to put the attribute in a separate assembly, at least until we figure out a better approach. You may also manage to avoid the double loading if you use reflection - as NUnit does - to find it by name rather than type. This is a problem that has been reported and which I'm still researching so all I can suggest for now is that you watch for the possiblility.


Charlie
>Charlie, Sunday, August 27, 2006

---

Charlie,

A few years back a developer (Andy Davis) modified a pre-2.2 final release of NUnit to include some "system" testing capabilities.  Basically, he added some new attributes to indicate system level tests.  He also modified the test execution for the new system test types.

Now (2 years and few versions later)...we would like to still use the system capabilities Andy added but also take advantage of some of the latest and greatest stuff added into NUnit.  After reading about the new extensibility capability with NUnit, instead of modifying NUnit source, I'd rather write an add-on using the Core extensibility options.   I did notice that with 2.4 Beta 2 you mentioned that the extensibility stuff could still be subject to change before the final release.

Basically, I was curious if you had any plans for add-ons that might provide system test capabilities or know of anyone who is working on womething similar?  Also, outside of bug fixes, do you think there may actually be major changes to the core extensibility interfaces prior to the final 2.4 release?

I've pulled down the 2.4 Beta 2 (source and all) and I've read several NUnit blogs, forums, groups.  I plan on looking at the extensiblity example you included in 2.4 today, with plans to outline a design for our add-on.  Any additional suggested reading or examples would be more than welcome.

Thanks!
Sara Lee
>Sara McLindon, Tuesday, October 24, 2006

---

Sara,

That's super! If nobody tries out the extensibility features, then we won't find out what needs to change. I'd be glad to exchange ideas with you offline on the design for the addin. I was planning to do a few addins myself, but since I developed the interfaces, I think it's better right now if somebody else exercises them.

If I remember correctly, the "system" testing capability had to do with ordering tests - is that right? I suggest naming addins after what they do, not for the purpose you think somebody will use them for. That way, they can find there own natural audience without being labeled as "for" a particular kind of test. So, if it's an ordered test fixture, I might call it an OrderedTestFixture - you get the idea, I'm sure.

I hope you're able to put this together. It will be a great addition to NUnit's capabilities.

Charlie
>Charlie, Wednesday, October 25, 2006

---

[...] NUnit 2.4 has a new extension feature called NUnit Add Ins. With these Add-Ins it is possible to extend NUnits capabilities without hacking the NUnit code base. This is a great step forward, although&nbsp;it is a&nbsp;feat that MBUnit&nbsp;has been doing for quite some time now. [...]
>Eli Lopian&#8217;s Blog (TypeMock) &raquo; Blog Archive &raquo; Problems with NUnit 2.4 Add-In Extensions, Sunday, April 29, 2007

---

@pingmustard...

Sorry, your post languished in the approval queue for a long time. If it's still a problem, try our mailing list, nunit-discuss on google.

A quick answer... you shouldn't rely on the current directory. The result file is created at the end of the run in the same directory
as the project file.
>Charlie, Sunday, December 5, 2010

---

We're trying to capture and perform post processing on TestResult.xml (eg: email and track when run on our production servers);   Using System.IO.Directory.GetCurrentDirectory returns 

"C:\Documents and Settings\myName\Local Settings\Temp\nunit20\ShadowCopyCache\4380_634188705643873321\Tests_437250125\assembly\dl3\9ecc3f44\59eea810_6849cb01"

However, the physical location of the TestResult.xml is in something like c:\TestConsole\TestResult.xml (where myTest.dll also resides) 

Is there a correct way to be able to dynamically reference TestResult.xml ?   I noticed the internals references a NUnitProject, but didn't see how I can access it...   Or maybe am I doing something wrong to cause current directory context to not switch?? 

Any help greatly appreciated.   Been pulling my hair out for the past few hours over this...
>pingmustard, Tuesday, August 31, 2010
