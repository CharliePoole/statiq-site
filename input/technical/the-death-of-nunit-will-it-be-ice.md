Title: "The Death of NUnit - Will it be Ice?"
Published: 06 Nov 2005
Tags: [Open Source, NUnit V2]
---
A recent post on the NUnit Open Discussion Forum on Sourceforge asked “Is NUnit Dead?” I have to admit: the question irked me a bit. But reflecting on it has led me to a few conclusions that I’ll share here.

<!--more-->
The original poster felt that NUnit might be dead because it hadn’t released for quite a while. Other posters seem to feel that the unit-testing capabilities built into Visual Studio 2005 will kill us off. So there are two ways to go...

<blockquote style="margin: 20px 10%">
Some say the world will end in fire;<br>
Some say in ice.<br>
From what I've tasted of desire<br>
I hold with those who favor fire.<br>
But if I had to perish twice,<br>
I think I know enough of hate<br>
To know that for destruction ice<br>
Is also great<br>
And would suffice.<br>
<div style="padding-left: 10em">_Robert Frost_</div>
</blockquote>

Although I'm pretty sure that Frost didn't have software development in mind, I'm adopting the metaphor anyway. I’ll talk here about the possibility of death by ice: frozen inactivity on our own part. I’ll follow up with my thoughts on the fire of competition in another blog entry.

NUnit 2.2 was released in August, 2004, 11 months after NUnit 2.1 and 22 months after NUnit 2.0. Based on that timing, we’re a bit overdue for another major release.

But back in 2004, we decided that more frequent releases were needed. We set out to issue minor releases – we call them “integration releases” - every few months and to try for a new major release every six months.

At first, this seemed to work well. The 2.2.1 release was issued in October, 2004 and 2.2.2 came out in December. Since then, there have been no binary releases, although – as I’ll show below - things have definitely been happening in the source code.

While coaching teams in Extreme Programming, I’ve often had to tell them that code, which only works for them and on their machines, is not of much use to the rest of the team – much less to the Customer. In the open source community, the same bit of wisdom is encapsulated in Eric Raymond's aphorism “Release Early, Release Often.”

So far, we haven't succeeded in doing that. Of course the source code - which has progressed - is available to all comers. But that's not good enough. Binary downloads, preferably with automatic installations, are needed to serve most people. This is particularly true for a project like NUnit, which has a large body of users of varying skills.

The reasons for our not managing to live up to (our own) expectations are varied and include poor communication among the developers, lack of platform availability for testing releases, logistical problems both on sourceforge and our external web site and simple lack of time - the developers all have day jobs, after all.

But the biggest issue seemed to be - at least to me - the lack of a common direction for where NUnit is going. That's why I wrote, circulated and published a Vision and Roadmap for NUnit. Both documents are found on the <a href="http://nunit.org">nunit.org</a> web site.

_Many thanks to Miguel de Icaza, who gave me lots of advice about open source project communicatons and who pushed me toward taking a broader view of the project's direction._

Aside from having a map, NUnit has actually covered a lot of territory in the past months.

We've supported each new beta of Visual Studio 2005 as it came along. Some people wish we would support them faster, but we have to wait until we actually receive, install and test the new releases. NUnit is not on the Microsoft Christmas card list and we get the releases at the same time as the rest of you.

We resolved issues with errors caused by use of context-bound objects and other changes to the thread context while running tests. This is a big issue for many people, particularly those running more complex tests under NUnit.

We introduced a new model for use in creating custom Asserts and an Addin structure that enables anyone to distribute new functionality for NUnit without rebuilding it. A new site has been created on SourceForge for contributed addins.

We now provide emulation for csUnit tests under NUnit, without change or recompilation. Visual Studio unit test emulation is being tested but won't be released until Visual Studio 2005 ships.

It would be nice if we had accomplished more, but the list is non-trivial. Our mistake seems to be keeping it in the source and forgetting the importance of frequent releases. We're working on that and I hope to get a new iteration release out in the next week or so, if only to keep from having to deal with bug reports on problems that are already fixed!

So yes, death by ice is a possibility for NUnit, just as it is for any open source project. But we aren't quite as frozen as we look and we expect to put out releases again fairly quickly.

Now for the fire... well that's a different story.

-----

### Comments

---

[...] From The Death of NUnit - Will it be Ice?, written by Charlie Poole, one of the NUnit developers: Our mistake seems to be keeping it in the source and forgetting the importance of frequent releases. We’re working on that and I hope to get a new iteration release out in the next week or so, if only to keep from having to deal with bug reports on problems that are already fixed! [...]
>Internet Alchemy Will NUnit Die?, Sunday, November 6, 2005

---

I am happy to learn that frequent releases are coming back.
>Christophe Broult, Monday, November 7, 2005

---

NUnit is dead... long live NUnit!

NUnit is popular with good reason. It introduced the idea of identifying tests by class attributes rather than by restrictive naming conventions. Imitation is the sincerest form of flattery and the test framework provided with Visual Studio 2005 definitely flatters NUnit by strongly resembling it. So, even if NUnit's prevalence declines somewhat since MS has "embraced and extended" it, keep it alive; it's a good tool to be able to pull out of the toolbox.
>Paul Hamill, Monday, November 7, 2005

---

Charlie, what do you think of the idea of NUnit being used by higher level tools? Personally, I never use NUnit GUI anymore - no offense, just that I use TestDriven.NET for 95% of my unit testing, Zanebug for 2%, and NAnt for the remaining 3%. Of course all of these use NUnit as a library. As a consumer, this is where I want to see NUnit going. From NunitRoadmap, I see only one GUI feature, and most of the features listed are plumbing features. Do you keep in touch with the TestDriven.NET folks and Adapdev? How about MbUnit folks? What if all the MbUnit features were available to NUnit as plug-ins? It would be cool to see more interplay amongst these tools.

I think the reason people think NUnit is freezing is that it just works, is widely deployed, and does a few things extremely well - not a recipe for a dynamic and fast changing open source project, but excellent and solid bedrock for test driving development.
>Chris Bilson, Tuesday, November 8, 2005

---

Here's an item from our NUnit Vision Statement...

<blockquote>
We are usable by other programs. We'd like to make it easy for other software to run tests programatically using NUnit. To that end, we will provide interfaces that make it easy for people to write their own clients. We can't guarantee that those interfaces will never change, but we'll make an effort to keep them reasonably stable.
</blockquote>

Seems like I forgot this when I put together the NunitRoadMap,  so I just added an item to provide a standard interface for other programs running tests. Right now, we have several ways you can run tests from other programs and it's up to the programmer to guess which ones we plan to hold relatively constant. Publishing a standard interface should help.

We do keep in touch with some developers of programs that work with NUnit. Lately, I've focused on RUnit, TestDriven, NAnt, NUnitAsp, NUnitForms and TestRunner. ZaneDebug only recently came to my attention. I'll take a look when I get a chance. As for the "competition" - Microsoft, csUnit, mbUnit - I generally look at what they do and see if the same function fits with NUnit. MbUnit has a number of features that don't fit the NUnit vision when considered as core features, but which would work well as Addins.
>Charlie, Tuesday, November 8, 2005

---

There is so much innovation possible in unit testing that it's a shame NUnit is moving so slow.  Take a look at testng.org and you will get an idea of where Java testing is headed.
>John, Thursday, November 10, 2005

---

It's good to know that NUnit is not dead. It's the single best piece of open-source software I know of. I couldn't live without it, and I can't afford Microsoft's price for Team System. I very much appreciate what a few people have done for the .NET community by developing NUnit.

At the MS launch event this week, and at a recent local user group meeting, I had heard that "the original developers were hired by Microsoft on the condition that nobody ever touch NUnit again. It's just a matter of time before Microsoft's marketing people open their eyes and put unit testing in Visual Studio 2005 Professional" 

I now know, from reading this blog, that NUnit isn't dead. But I think the perception of its demise could kill it. NUnit seems to have lost a lot of momentum this year. I'm frustrated, because I don't have the skills needed to download and compile incremental releases. I've spent most of today trying. I can't even get the config file fix to work; I still get the bad image error. But that's my problem, and I own it.

Charlie &amp; Co., what this community needs now more than anything else is a release of NUnit 2.2 that can test projects created in the RTM version of VS 2005. There are thousands of us yeoman programmers out here who would sing your praises until the end of time (or at least the next VS release ) if we could just get our green bars up and running running under VS 2005. The Road Map is great, but we've got to get the car on the road first. Right now, it's in the shop, and it's been there for a long time.
>David Veeneman, Sunday, November 13, 2005

---

A timely comment, David. I'm just now trying to figure out whether to release one build or two - one for 1.1 and one for 2.0.  It's clear that the config file stuff isn't all that easy for most people. 

Any ideas on what would work best for you, while not messing up people who don't have VS2005 yet?
>Charlie, Sunday, November 13, 2005

---

I'd vote for a .NET 2.0 (RTM not beta) release ... ASAP.
>Bob, Monday, November 14, 2005

---

Well... that of course require my having the RTM. :-)
>Charlie, Tuesday, November 15, 2005

---

[...] He mentions the &#8220;NUnit team not wanting [his] additions&#8221;. I attempted to contact Charlie Pool after reading his post on the death of NUnit, in order to offer my development services on the project, but have not yet received any response. I&#8217;ll wait for it&#8230; [...]
>idp &raquo; Blog Archive &raquo; Nice NUnit Extensions, Friday, November 18, 2005

---

For idp -&gt;

It took me a search to figure out who you are, but I did it since I was concerned. Your note was received on the Tuesday, the 15th. Your (positive) response was sent on Thursday, the 17th. Please keep in mind that I do this in my spare time.

Regarding the quote from Eddie's blog, I don't think it's appropriate to write about a third party here, but  I'll give some thought to blogging about what people can expect when they send a suggestion, particularly if it seems more like an extension than a part of NUnit.
>Charlie, Friday, November 18, 2005

---

[...] In my earlier post, The Death Of NUnit - Will it be Ice?, I adopted the notion - promoted in some mailing list posts - that NUnit might be dead or dying. I identified two big worries: death by ice and death by fire. Ice stood for frozen inactivity on the part of the project itself - failure to move ahead. Fire symbolized competitive forces, which might make NUnit obsolete. [...]
>It&#8217;s the Tests &raquo; Blog Archive &raquo; The Death of NUnit Revisited - Will it be Fire?, Friday, December 30, 2005

---

[...] Another interesting topic we came upon was talking about our different Open Source projects.&nbsp; Charlie mentioned he had communicated with Miguel de Icaza&nbsp;when struggling with is project.&nbsp; I think you can see a bit of that in his blog entry about the exaggerated death of NUnit.&nbsp; We then discussed the relation of our Open Source projects with Microsoft.&nbsp; Charlie asked why not make WiX the foundation of Visual Studio setup projects.&nbsp; I told him I would love for that to work out.&nbsp; Then I asked him about NUnit and VSTS.&nbsp; Charlie pointed out that VSTS isn't structured properly to do true test driven development (TDD) but said he enjoyed the competition. [...]
>Rob Mensching Openly Uninstalled : Thursday Night &amp;amp;quot;Deployment Dinner Discussion&amp;amp;quot; with Charlie Poole., Thursday, October 5, 2006

---

Guys, I wanted to congratulate you for all the hard work you are given to this Project, and as an Open Source Project Leader myself I feel that all you are doing it is just plain amazing... Full details on my blog: http://flois.blogspot.com/2006/05/frozen-death-is-all-around-us.html#links
>Federico Lois, Sunday, May 21, 2006

---

I am a student at Penn State and we are required to look into NUnit.  I am trying to look for a good site that explains exactly what NUnit is and what it does.  (I also need to look into Log4net, but I don't know if I can get help for that here...)  I am very very novice to this stuff.  Before today I had never even heard of NUnit (or Log4net for that matter).  Any help[ would be greatly appreciated.

Thank you!
>Gary, Wednesday, September 20, 2006

---

Gary: Google is your friend. :-)

Actually, you are on the main NUnit site here. If you want a basic intro, look for online articles on NUnit or follow some links on this site.
>Charlie, Wednesday, September 20, 2006

---

hi,
 iwant advantage and disadvantge of n unit please reply me soon as possible
>samad, Monday, August 7, 2006

---

[...] I love the smell of green in the morning. I&#39;m in the middle of a huge refactoring project and although I&#39;m working in VSTS my old habits made me stick to NUnit that the tests were originally written in. I haven&#39;t even looked to see if VSTS will be able to use my tests as-is..but should I even try? NUnit is like an old pair of slippers now, and isn&#39;t gonna die just yet apparently.But that&#39;s all for another day. For now let me bask in the soft green glow of my jolly happy LCD&#39;s. &nbsp;&nbsp;ASP/ASP.NET Web Hosting: 3 Months FREE + FREE Setup - CLICK HERE!  Posted: Thursday, August 17, 2006 3:58 PM by james Filed under: Technical, Community Server, Unit Testing [...]
>British Inside : NUnit is green, Thursday, August 17, 2006

---

[...] Traditionally, it&#8217;s been relatively difficult to evaulate any given project&#8217;s potential for death, be it by &#8216;ice&#8216; or by &#8216;fire&#8216;, as Charlie Poole puts it. OpenSource users typically rely on metrics such as SourceForge&#8217;s activity percentile, mailing-list noise-to-signal ratios and even commits-per-period, to determine how likely a particular project is to survive. [...]
>Oh Oh OSS&#8230; | idp :: shmarya.net, Monday, July 17, 2006

---

Hi, 

Actually i am trying to get values from the session object created by my login page inside the NUnit test application. Can you kindly guide regarding this. I have 4 variables that i store in my session object when i am successfully logged in the Login page. Now in my test application i need this information to test other pages in the project (because of authentication) so i need to get these(session variables) information.

Kindly guide me regarding this.

Thanking you.
>Swimmy, Saturday, July 1, 2006

---

Can we connect to oracle database from nunit??can u please refer some code snippets ?
>Gunjan, Friday, June 30, 2006

---

Speaking of ebb and flow, this blog has been pretty inactive for a while, even while NUnit has been coming out with a Beta, etc. Guess I should write some stuff soon. :-)
>Charlie, Monday, July 24, 2006

---

Gunjan and Swimmy:

My blog is not a convenient place to provide general help or to discuss things other than the blog posts themselves. Please try the nunit-user mailing list or the help forum on sourceforge. Either one will get you the same help, so just pick the format you prefer.
>Charlie, Sunday, July 2, 2006

---

samad: I suggest you look at the documentation and ask some specific questions on the nunit-users mailing list, rather than here. You can subscribe to the list on sourceforge. If you would like advice about the suitability of NUnit for your use, you should tell us there what you want to use it for.
>Charlie, Monday, August 7, 2006

---

i have to test my code with Nunit.There is a search engine which searches database according to the word which you have entered and displays it in Datagrid.
so i have to test this thing using Nunit.please suggest me some solution.
>vikrant, Tuesday, June 20, 2006

---

vikrant: There should be no problem doing this. Please ask your question with some more details about the problem you see on one of the forums or mailing lists.
>Charlie, Tuesday, June 20, 2006

---

Hi Charlie,

Cheers to you and your team for creating such an amazing tool.  I, like everyone else, hope that development continues and thrives.  However, I also understand the ebb and flow of OSS development and know first-hand that teams need breaks (for personal reasons and also to replenish the energy that goes into the project).  For what it's worth, I appreciate your efforts and applaud the fantastic result.  Without doubt, I will be a faithful NUnit user for as long as it is available.  Thank you very much for such a valuable tool (one that I would truly hate to live without).
>devilbush, Monday, July 24, 2006

---

Yes, we're planning to integrate MbUnit w/ the Zanebug GUI, so that you can run MbUnit, Zanebug and Nunit tests all within the Zanebug GUI.  We're still working out what needs to be done, so it may be a little while. 

In the meantime, I'll be adding Row Testing to Zanebug, since I've gotten a lot of requests for it.

Should have something for that in the near future.

have a great day!
>Sean McCormack, Tuesday, October 3, 2006
