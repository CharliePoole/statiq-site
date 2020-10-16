Title: "The Death of NUnit Revisited: Will it be Fire?"
Published: 30 Dec 2005
Tags: [Open Source, NUnit, TDD, It's the Tests]
---
In my earlier post,
[The Death Of NUnit - Will it be Ice?](/posts/the-death-of-nunit-will-it-be-ice.html),
I adopted the notion - promoted in some mailing list posts - that NUnit might be dead or dying.
I identified two big worries: death by ice and death by fire. Ice stood for frozen inactivity on
the part of the project itself - failure to move ahead. Fire symbolized competitive forces, which
might make NUnit obsolete.

<!--more-->
In that first article, I tried to show that NUnit is not frozen but has been moving ahead,
although we were remiss in not releasing early and often. I promised we get some new releases
out, and indeed we have. NUnit has had three times since then and NUnit 2.2.5 is our new
recommended release. You can see it's features in the
[NUnit Release Notes](http://nunit.org/index.php?p=releaseNotes&r=2.2.5").
Meanwhile, work is underway on the NUnit 2.4 release, as described in our
[project roadmap](http://nunit.org/index.php?p=roadmap).

So now, in this post, I want to address the question of death by fire. In particular, some folks
have expressed the opinion that the unit-testing features in Visual Studio 2005 Team System
(VSTS) will be the end of NUnit, so lets look at some of the main issues in comparing the two
"products".

**Test-Driven Development**

NUnit is tailored for use in Test-Driven Development. If you've used other xUnit test frameworks, NUnit looks and works pretty much the same as they do.

VSTS unit tests can be used for TDD, but it requires a fair amount of effort. Essentially, you're
fighting the way the environment wants you to work when you try to work test-first under VSTS.

Of course, Microsoft has some pretty smart people and many of them do TDD - often using NUnit.
So my guess is that some future release will correct this problem. But for now, NUnit is the
clear winner.

**Portability**

Not everyone wants to develop with Visual Studio. Even  if they do use it on Windows,  many
folks want to be able to build and test their applications on other platforms as well.
Obviously, Visual Studio's unit-testing support is only useful when you use Visual Studio.

NUnit , on the other hand, can be used without buying any version of Visual Studio. You can
simply use the .NET SDK directly or use open-source IDEs such as SharpDevelop, MonoDevelop or
Eclipse. Using Mono, NUnit works on Linux and Unix-based systems - in fact, Mono itself is tested
with NUnit. This makes NUnit ideal for use in developing multi-platform applications, or those
that might eventually be ported to other platforms.

**Cost**

As described on the Microsoft Visual Studio site, Visual Studio 2005 is distributed in the
following editions:

* Visual Studio Express Editions
* Visual Studio Standard Edition
* Visual Studio 2005 Professional Edition
* Visual Studio 2005 Team System

Of the above, only the Team System includes unit-testing support. Specifically, the entry-level
for developers wanting unit-testing support is Visual Studio 2005 Team Edition for Software
Developers. The Microsoft [How to Buy](http://msdn.microsoft.com/vstudio/howtobuy/Default.aspx)
page gives comparative prices for the various editions.

NUnit, on the other hand works with all editions of Visual Studio .Net, Visual Studio .Net 2003 and Visual Studio 2005. That includes the free express editions.

**The Future**

For most of its user base, the fact that NUnit is open-source is a plus. That won't change.
Of course, there are corporations that prefer to buy proprietary commercial software. That's
not new. Those folks are not our current users and we're not particularly interested in them.

NUnit will continue to evolve, as described in our [Vision Statement](http://nunit.org/?p=vision)
and [Roadmap](http://nunit.org/index.php?p=roadmap). Some of our developers will get busy with
other things. Some of our users will take their place. Eventually, the NUnit project may come
to an end, but even then, NUnit won't be obsolete because its users will be able to update it
themselves.

Finally, if we're lucky, the ideas that empower NUnit will spread and vendors like Microsoft will
need to pay attention to them, leading to even tougher "competition."

So much the better.

---

### Comments

---

Though NUnit has made a niche for itself, i find that people like me who do much of their business logic in the UI (even though that is bad practise) , do not see the same level of advances in NUnitForms or NUnitAsp. I doubt if Microsofts Unit test suite also will cater much to unit tests for UI ?
>Chakravarthy, Friday, December 30, 2005

---

Don't forget the sheer size of the NUnit user base either. Another test framework would need to have some pretty compelling advantages over NUnit for people to start moving over IMHO. I suspect many people have invested an awful lot of time in a framework they now know, trust and understand. They won't make a switch lightly.
>Rob Levine, Saturday, December 31, 2005

---

[...] In a recent post Charlie discusses the future of NUnit in light of the built in testing capabilities in the Visual Studio Team System platform. [...]
>idp &raquo; Blog Archive &raquo; Not yet dead&#8230;, Saturday, December 31, 2005

---

We want to use CruiseControl.NET and to run the tests we specifically don't want VS.NET on the integration server. This would involve another license (at extreme cost just for building!) and also would create an environment that does not mirror a production server. So, in that respect NUnit does the job a whole lot better. Given the integration with CC.NET and NAnt, the VS.NET 2005 does not cut any ice with me whatsoever.

Charlie and folks, keep up the good work!
>Dirc Khan-Evans, Tuesday, January 3, 2006

---

I like NUnit and would not like to depend on VS.NET just to be able to do unit testing. 

However, one nice thing that is included with VS.NETs unit test is the ability to set a break-point in the test code and use the debugger when running the tests. This feature was touted as "the case against" NUnit on a Team Server seminar on which i participated. They actually said that the VS.NETs version of unit testing was much like NUnit, only "they made it work properly."

If you could make debugging available when running NUnit tests it would not only be useful it would also pull some of the teeth from MS's case against NUnit.

Thanks for a very nice test framework.
>Thomas Koch, Friday, January 6, 2006

---

It is possible to debug using NUnit.

For your solution set the startup project to be the one containing your unit tests. For that project from the Debug page of the project properties set Start Action to start external program and specify the NUnit runner you wish to use. Set the command line arguments to be the project directory and set the working directory to be the directory containing the project file. Set your breakpoints and run the solution. This will lauch the NUnit runner you indicated earlier. When NUnit runs the tests your breakpoint will be hit and you can debug away.
>Mark Roberts, Friday, January 6, 2006

---

Is the Microsoft 'case' against NUnit published somewhere? I'd love to be able to reference it.

Obviously, since they have a product that is both a Visual Studio Addin and a test framework, they can provide integration that a non-Addin cannot provide. You can do that currently with NUnit coupled with several AddIns.

As Mark indicates, you can debug fairly easily with NUnit, but it does take more than a single click. I'd like to add the single click capability, but I'm not sure how to implement it in a non-VS-dependent way. That is, it would be cool if NUnit could run the tests under whatever debugger is installed on your system!

I'll have an addin for NUnit myself "one of these days" - separate from the NUnit download of course.
>Charlie, Friday, January 6, 2006

---

[...] De nemcsak ennyi a történet, vannak más előnyei is az NUnitnak a Microsoft megoldásával szemben, és nem csupán a hordozhatóságra gondolok (más platformok, más környezetek). Érdemes elolvasni a szoftver fejlesztőjének összehasonlítását: Charlie Pool: The Death of NUnit Revisited - Will it be Fire?: [&#8230;] In particular, some folks have expressed the opinion that the unit-testing features in Visual Studio 2005 Team System (VSTS) will be the end of NUnit, so lets look at some of the main issues in comparing the two “products”. [&#8230;] [...]
>gaba: NUnit: versenyben a Microsofttal, Thursday, February 9, 2006

---

Also, how about TestDriven.NET (formerly NUnitAddIn)? It gives the ability to debug &amp; launch NUnit right from VS IDE... And MS recommends it, in fact, at
http://msdn.microsoft.com/msdnmag/issues/05/12/VisualStudioAddins/default.aspx
>Patrick E., Monday, January 9, 2006

---

There is Testrunner (http://www.mailframe.net/Products/TestRunner/default.aspx) which does pretty much everything we need for VS.NET integration; i.e. also single click test running and debugging.  It costs $49 -- not *that* expensive compared to VSTS.  :-)
>Felix Wiemann, Monday, January 9, 2006

---

Re: However, one nice thing that is included with VS.NETs unit test is the ability to set a break-point in the test code and use the debugger when running the tests. This feature was touted as “the case against” NUnit on a Team Server seminar on which i participated. They actually said that the VS.NETs version of unit testing was much like NUnit, only “they made it work properly.”

This is pretty funny if you consider the following entry in Microsoft's Product Feedback Center:

Suggestion Details: Add Debugging Support for Unit Tests
http://lab.msdn.microsoft.com/productfeedback/viewfeedback.aspx?feedbackid=b5da5362-3256-4526-ba2e-399f38faa9bb

In particular look at the workaround:

FDBK15335#2: Try the testdriven.net add-in
http://lab.msdn.microsoft.com/productfeedback/ViewWorkaround.aspx?FeedbackID=FDBK15335#2

There's nothing like a good bit of revisionist history. ;o)
>Jamie Cansdale, Wednesday, January 25, 2006

---

I'd propose MSBuild, NUnit, NUnitForms, TestDriven.NET and CruiseControl.NET.
MSBuild from the .NET SDK can load the sln files of VS 2005 which I prefer to using NAnt. What do you think?

An excellent article, CHARLIE!
>Michael K., Monday, January 30, 2006

---

**NAnt, NUnit and CruiseControl.NET**


>Bite my bytes, Thursday, January 26, 2006

---

Visual Studio Team Edition is priced out of reach of most mid-sized organizations that cannot afford the hefty price tag. I believe we'll find more small- and medium-sized business migrating to Visual Studio Professional and continuing to use open-source tools that are at least as good as (if not better than) Visual Studio Team Edition's bundled add-ins. As it applies to this discussion, I see nothing in Visual Studio's unit-testing framework that urges me to switch away from NUnit and TestDriven.NET. In fact, I would argue that I have a vested interest in continuing to use NUnit because I do not want to rewrite all of my unit tests without a significant advantage in doing so.

Code coverage analysis isn't really a compelling reason either because I already have sufficient code coverage analysis in Visual Studio 2003 and Visual Studio 2005 with NCover and NCoverExplorer. I would even argue that code coverage analysis is easier with TestDriven.NET because I choose whether to perform code coverage analysis from the context menu whereas in Visual Studio 2005 Team Edition I must manually edit a configuration setting each time I want to toggle code coverage.

We have already migrated to Subversion for version control and Mantis for bug tracking and have the two nicely integrated to allow us to track our code changes. I experimented with Team Services and found it requires excessive hardware, is significantly more complex to implement, and is likely more expensive to maintain. We also prefer the branch/merge/tag approach of Subversion since it allows us to track changes in each release and to perform parallel development of disparate code bases.

The only compelling feature in Visual Studio 2005 Team Edition that I can't obtain (or haven't found) through open source is code analysis, but code analysis alone does not justify the enormous expense for us to purchase Visual Studio 2005 Team Edition. Once the products are finalized, I highly suspect we will migrate to Visual Studio 2005 Professional and continue to use existing open-source tools. As I mentioned, the primary reasons are that Visual Studio 2005 Professional is significantly cheaper and Visual Studio 2005 Team Edition does not offer significant compelling features that justify its higher cost.
>Todd Jones, Thursday, February 16, 2006

---

Hey Todd,

check out FxCop. It performs code analysis with more than a 100 tests on IL level (I think that means it won't check that nicely put copyrights in all your source files). And the best: It's FREE!
http://www.gotdotnet.com/team/fxcop/

Christoph
>Christoph Wienands, Sunday, February 19, 2006

---

Hi, 
I want to try use NUnit with Visual Web Developer 2005 Express, but without success (I don't know how to run tests =( ). 
Is there any way how to test asp.net 2 application with free edition of VS 2005 ?

Thank you
>Petr Šnobelt, Wednesday, March 1, 2006

---

MSBuild is very inferior to NAnt in most respects -- I won't be making that switch any time soon, even with both Microsoft and SharpDevelop using it for their build systems.

Their testing framework seems inferior to NUnit as well, in virtually every conceivable way.  However, the ultimate arguement against any Microsoft testing tool is simple.  When is the last time you heard anyone use 'well tested' in reference to any Microsoft product, at all?  Wow, you know Windows XP was well tested, Word was well tested...  You don't hear people saying that, usually you hear things like "wow, there's that stupid extra Visual Studio window on the other monitor again."

For the folks talking about CruiseControl, you might also want to take a look at Draco.net -- in my experience, Draco is a lot easier to maintain than CC.
>Dave Bacher, Tuesday, March 7, 2006

---

Regarding the discussion on debugging on 1/6/2006 and the "ability to set a break-point in the test code and use the debugger when running the tests", here's another way to debug NUnit test code, without using another product or changing project settings:

1. Run NUnit-Gui and open the test file
2. In VS (2003 or 2005), go to Debug/Attach to Process and attach to nunit-gui.exe
3. Set a breakpoint in the test code
4. Switch back to NUnit-Gui and click Run.  The test should break at your breakpoint.

More than one click, but easy enough for occasional debugging.
>Darin H., Wednesday, March 15, 2006

---

A good open source solution for testing web applications on the web: Selenium
http://www.openqa.org/selenium/
Still in beta but it looks promising.
>LosManos, Tuesday, March 21, 2006

---

The price for the MS's Team Edition is too high.  My company (400 developers) decided to go with VS Pro and NUnit instead.
>UncleFestis, Tuesday, April 4, 2006

---

"That won’t change. Of course, there are corporations that prefer to buy proprietary commercial software. That’s not new. Those folks are not our current users and we’re not particularly interested in them."

Amen.  NUnit rocks!
>Mark Doliner, Tuesday, April 4, 2006

---

NUnit will not die becuase is multiplatform and not tied to a specific development environemnt.
>Giacomo Stelluti Scala, Tuesday, April 11, 2006

---

Mmm, this is not an advert!
For running NUnit tests under the debugger www.testdriven.net (VS add-in) gets my vote too.  Provided you remember to set the Add-In startup options under Tools-&gt;AddIn Manager.  No NUnit and Visual Studio developer/tester shoudl be without it.  Within the IDE running all tests, single tests and single tests under the debugger are a point, right-click and left-click away.  The .NET 2 version is beta right now, but seems to work as well as the 1.1 version did.
I think the high price of entry of MS's unit testing effort, the industries experience and investment in NUNit and the fact that you seem to have to fiddle about to get MS's effort to work right (http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnvs05/html/vstsunittesting.asp) makes it a non-starter... for now.  The auto-test generation option you get is not enough to pursuede me.  In future it might be a different story.
>Adrian Hilder, Tuesday, April 4, 2006

---

I am facing a problem when using VSTS with NUnit 2.2.6.
I am testing a website. After opening the browser it closes immidiately and
I am getting the following error.

!!!SCENARIO FAILURE!!!
Info: Failed to execute test cases due to run time error: ‘The HWnd is no longer valid’
Image: file:///C:\Program%20Files\NUnit-Net-2.0%202.2.6\bin\MAUI_Chrome%20MastHead%20section-Scen10.jpg

Is the problem with NUnit 2.2.6? Let me know.
>Deevish, Monday, April 17, 2006

---

It's unlikely that NUnit is causing this, but it's hard to tell without seeing your actual code. Please post with a sample of the code that is failing to either the help forum on sourceforge or to the nunit-users mailing list.
>Charlie, Wednesday, April 19, 2006

---

NUnit might as well be on its way out, the whole efficiency that an automated unit test case creation tool brings in would gradually see the end of NUnit. Though i do agree that the first 1 or 2 releases from MS would not be able to wipe out NUnit, but the end seems near.
>Naresh, Tuesday, April 11, 2006

---

What a great blog.  I thought the emphasis on the competition aspect was particually important.
>Warren, Friday, April 28, 2006

---

For Uche: Cool, but you should have read the original post... http://nunit.com/blogs/?p=12 :-)
>Charlie, Thursday, May 11, 2006

---

Some say the world will end in fire,
Some say in ice.
From what I've tasted of desire
I hold with those who favor fire.

But if it had to perish twice,
I think I know enough of hate
To say that for destruction ice
Is also great
And would suffice. 

"Fire and Ice" by Robert Frost.  Sorry--someone had to post it!
>Uche Akotaobi, Wednesday, May 10, 2006

---

charlie can you send me the installation files for NUnitAsp and Nunit.i have .netframework2.0 in my machine.
i am waiting for your response
>vikrant, Thursday, June 8, 2006

---

vikrant: You may download NUnit from the download page on this site. You may download NUnitAsp from the NUnitAsp site. NUnitAsp is not part of NUnit. 

Kindly use email or one of our mailing lists for help requests.
>Charlie, Thursday, June 8, 2006

---

Regarding pricing VSTS, yes it is overpriced, but it is not a question
Recently we have migrated our project to .NET 2.0 (~200K lines of code).
All our unit tests written with NUnit, now I think costs for reimplementing all test code will costs MUCH MORE, then price of VSTS. And this price must be payed only for nice interface (for me it is only nice interface). Answer no , and once again no. I believe my company not only one who fall in such situation. 

P.S.1. Never post or mail NUnit team , then want to say thank you guys your work absolutely Brilliant 
P.S.2. Please excuse my English it is far from perfect :)
>NUnit, Saturday, July 29, 2006

---

I tried VSTS for Developer and found little use for me except the nice gui interface inside IDE. However, there is excellent VS add-in that does both serious refactoring and functioning as gui interface for NUnit, ReSharper 2.0 from JetBrains. It gives you seamless experience as in NUnit-Gui and debugging test code feature as well as famous refactoring. I also found that unit test generation feature in VSTS is useful for me so, I created similar add-in called, NUnitGenAddIn. Check it out at http://developer.novell.com/wiki/index.php/NUnitGenAddIn.
>Terry, Tuesday, August 8, 2006

---

I am using NUnit-Net-2.0 2.2.8 and Visual Studio Team Edition For Software Architects.
I have created a C# Console Application and in Program.cs class I have code which I want to test. I have another TestClass which I use for testing the code which I have in Program.cs class. Iin TestClass I have referenced NUint.Framework and and have used  [TestFixture] , [SetUp] and [Test] attributes at class level, and at setup and test level. I have in project properties page at Debug tab specied path of NUint Gui exe and also working Dircetory as Console Application path. When I run program in debug mode with breakpoints in TestClass I see Nuit Gui exe come up but it does not show tree structure of Tests and also it does not stop at break point. I also tried to attache Nuit Gui exe to attache and run but effiect is same as earlier desceibed. Can any one please help me as how where I may be going wrong? I am ready to send code snippest or zip file of the said program.
Thank you.

Bharat Gadhia.
>Bharat, Tuesday, August 1, 2006

---

Hello Bharat

Most likely, you have not set up the nunit-gui command line to point to the correct copy of the test assembly. However, the blog is not convenient for extended discussion, so if you have further problems, please use the mailing list or forums on sourceforge.

Charlie
>Charlie, Tuesday, August 1, 2006

---

Hi Charlie,
Thank you for the reply. Can you please tell me what you mean by "nunit-gui command line to point to the correct copy"?

I will henceforth use sourceforge for the discussion. 

Thank you.
Bharat.
>Bharat, Tuesday, August 1, 2006

---

The argument to nunit-gui should indicate the copy of your test assembly located in bin/debug or bin/release. In particular, use of VS2005 macros to specify the argument will usually get you to the obj directory, which will not work.
>Charlie, Tuesday, August 1, 2006

---

[...] Traditionally, it&#8217;s been relatively difficult to evaulate any given project&#8217;s potential for death, be it by &#8216;ice&#8216; or by &#8216;fire&#8216;, as Charlie Poole puts it. OpenSource users typically rely on metrics such as SourceForge&#8217;s activity percentile, mailing-list noise-to-signal ratios and even commits-per-period, to determine how likely a particular project is to survive. [...]
>Oh Oh OSS&#8230; | idp :: shmarya.net, Monday, July 17, 2006

---

Smart developers span platforms, NUnit is more like JUnit and thus it makes the developer more portable.  I dont' doubt that some corporate environments may switch but the cost, loss of platform chameleon will make it less attractive for some time. Microsoft has their own Source control as well but everyone has long gone to vault, subversion or cvs with tortoisex fronts.

NUnit and things like test driven are too fun currently to go away.
>Ryan Christensen, Saturday, June 24, 2006

---

[...] Visual Studio Team Suite (VSTS) built in test framework.&nbsp; See what Charlie Poole, a member of NUnit team has to say about this. [...]
>The long way round&#8230; &raquo; Setting up NUnit, Tuesday, September 19, 2006

---

Additionally Microsoft's unit testing framework does not support inheritance as described here:
http://vaultofthoughts.net/TestMethodInTheAbstractClass.aspx
>Michal Talaga, Monday, September 25, 2006

---

NUnit has been the most widely used unit test framework for .NET for a number of years. To my direct knowledge, it's used by
teams at Microsoft, Siemens, Intel and HP. There's no way to get a complete list, since NUnit is freely downloadable.

There are a number of things that your company may mean by "Enterprise Level"

1) They want a single product that does unit testing as well as other things - a sort of IDE. That's not NUnit.
2) They want specific testing features not found in NUnit. I'd definitely like to hear what those are.
3) They want support. Contact me for rates.
4) They want a different license and/or they want to pay for NUnit. Contact me.

If you find out what they mean, please let me know as it would be useful to the project.

Charlie
>Charlie, Monday, October 9, 2006

---

Is there a list of projects (preferably commercial) that use NUnit?  I'm trying to sell NUnit at work.  The initial response was positive, but the push back I'm getting is that some feel that NUnit doesn't have "enterprise" level features.  Not that I really know what that means.  Hopefully, I can point to its use at Microsoft and other "enterprises."
>TheSlate, Friday, October 6, 2006

---

Is is possible to add the NUnit test class in LoadTest of VSTS? (like the Unit Test of VSTS)
>Anony, Thursday, November 9, 2006

---

VSTS doesn't know how to load NUnit tests. NUnit does have an experimental addin - not yet released - that loads VSTS tests into NUnit.
>Charlie, Thursday, November 9, 2006
