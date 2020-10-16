Title: "NUnitLite and the GPL"
Published: 19 May 2006
Tags: [NUnit,Open Source,TDD,It's the Tests]
---
In my [last post](/posts/nunit-on-microsoft-codeplex.html), about the NUnit team's plans for trying out the Microsoft <a href="http://www.codeplex.com">CodePlex</a> site, I introduced the <a href="http://www.codeplex.com/Wiki/View.aspx?ProjectName=NUnitLite">NUNitLite</a> project. One thing I mentioned was the possibility of its being released under the GPL license. This seems to have gotten more reaction than anything else in the post. I find that a bit disappointing, because I thought there were some other cool things in it. :-)

Be that as it may, I'll try to explain here why my next software project might use GPL and what kind of considerations I'm looking at in making a final choice.

<!--more-->
NUnit is the sum of my experience with open source development. I came to it as a developer primarily oriented toward closed-source, Windows applications and gave little thought to the license that people before me had chosen. NUnit uses the zlib/libpng license, which puts very few limits on how the source may be used. For example, it's perfectly OK to take NUnit, change a few things and sell it as a proprietary testing framework. You are only required to place a notice somewhere that says the code is based in part on the work of the NUnit copyright holders.

In fact, NUnit is used in a number of proprietary systems you can buy today. I know this because people occasionally send me email telling me that such-and-such company is using NUnit code. I check it out and determine if they are complying with the license. So long as they do, they are perfectly within their rights.

The problem with this is that code developed by the open-source community is taken, possibly improved but never returned to the community that developed it. I don't like seeing this. I'd like to avoid having it happen with the next project I work on.

There is a class of licenses, generally known as "reciprocal licenses" that try to prevent this. GPL was the first, as far as I know, and is certainly the best known. In general, such licenses say something like "You may modify this program to create a program of your own and then distribute the new program, but you must license the new program using the same license that gave you the right to use it yourself." This has been called "viral" - which it certainly is - and "un-american" - which seems silly.

So here are a list of things I'd like to see happen with the licensing of NUnitLite...

* You can use it any way you like within your own shop, just as always.
* In particular, using NUnitLite to test your application should not place any additional requirements on distributing that application
* You can use the NUnitLite code as the basis of an internal test framework you don't re-distribute
* You can use NUnitLite as the basis or include it in an application you distribute to others BUT you must then distribute it under the same terms as NUnitLite.

Except for one small issue, that seems to be what the GPL does, which is why it came up in the first place.

Here's the issue...

As a lightweight framework, NUnitLite's source code may be embedded with the tests. If the tests are distributed, either because they are embedded in the application or because a decision has been made to provide them separately, then NUnitLite is being distributed. Some people might argue - and others might fear - that the application would then be subject to the same license as NUnitLite. I don't believe that's true, but it's untested and requires a bit of looking into. Lawyers could argue that such an application was a "derivative work" while other lawyers would counter that it was a mere "combination" of programs.

The fact is, I don't plan to sue anyone for testing their code with NUnitLite, no matter where or how they do it. But it is important that companies feel comfortable with the license for NUnitLite, which is why this is a matter for serious discussion.

Licenses on my list for consideration are GPL, LGPL, MPL, CPL and OSL. I'd really like to hear how people think these licenses would suit their potential use of NUnitLite. I'd also like to hear from other developers who have dealt with the specific problem of embedded test frameworks and how they have handled it.

Of course, I'll probably also hear from folks who think that requiring derived works to be open source is a bad idea. That's fine too, but since I'm not much of a zealot about these things, it may not be much fun for those looking for a holy war. I'm just looking for the next thing to try.

---

### Comments

---

Another vote for LGPL. You want to make the license such that any enhancements to the program will be redistributed. You don't want people to scratch their heads and figure out if they need to worry about open sourcing their whole app if they use NUnit. LGPL would seem to satisfy both of these things. SharpDevelop moved to LGPL semi-recently, also.
>Matt Hargett, Tuesday, June 13, 2006

---

Samuel: I've been browsing the FSF site and it appears that I could use GPL with a specific exception when the app is only used to create tests. I'm not sure this is the best way to go, however, since it means we would have to craft the legal language to include what we want included and exclude what we don't. I'm pretty tied up with getting the NUnit 2.4 Alpha release out, but I'll take a closer look at the options when that's done.
>Charlie, Sunday, May 21, 2006

---

I would suggest to use the LGPL instead of the GPL 'cause the LGPL is created to avoid the problem if the Program/Library is derived from NUnitLite. Many Companies don't want to use code/libraries if the legal issues are not clear, so using the GPL, but saying 'I don’t plan to sue anyone for testing their code with NUnitLite' leaves an unclear factor that would push companies to other, maybe even commercial testing frameworks to avoid legal problems.
>Roland, Sunday, May 21, 2006

---

I wouldn't try to modify or extend the GPL.  I think you would virtually create a new license, making the NUnit code unusable with other GPLed source code (because you cannot combine GPL and GPL+extension).

Have you examined the benefit you'd get from re-using other people's code?  I'd expect that most changes proprietary testing frameworks make to NUnit are not suitable for inclusion in the code base. What NUnit needs is more thoughtful design, not more code.  On the other hand, those companies that do add something valuable to NUnit would probably rather rewrite the underlying testing framework (NUnit) than GPL their extensions.  (For the record, my company doesn't sell testing frameworks, so I think I'm rather objective here.)

I'd also like to add that GPL causes a lot of headache when you try to combine it with other open source projects that do not use the GPL.

So, I would probably opt for a license that allows easy re-licensing because that offers you the most flexibility and makes you spend less time on thinking about licensing issues.
>Felix Wiemann, Monday, May 22, 2006

---

If I remember correctly:
The FSF distributes the "Readline" library under GPL and says that linking to it constitutes “derivative work” since there are no way to build the program without it. The LPGL is written to let people use libraries without fear of "viral-nes”.

I have seen this kind of concerns before and I always wonder; would there be unfortunate side-effects from amending the choosen licens with a statement saying "this kind of use is explicitely allowed"?
>Samuel Åslund, Sunday, May 21, 2006

---

We've handled this scenario by separating the products and applying different licenses to each product: the primary product and the redistributables. You could use the same strategy for NUnitLite to develop tests and the NUnitLite framework to execute embedded tests.
>James Johnson, Saturday, June 3, 2006

---

Felix: You're right of course. I was getting turned around regarding what who was linking to whom. And if someone distributed an enhanced test framework library based on NUnitLite, it would have to be LGPL.
>Charlie, Tuesday, May 23, 2006

---

LGPL won't be an issue for people who distribute their tests, I assume.  The tests can still be closed-source while "linking" against the LGPLed NUnit framework.
>Felix Wiemann, Tuesday, May 23, 2006

---

LGPL, as suggested by Roland is a possible option. It seems to have similar issues to GPL for people who distribute their tests though. ALthough it hasn't been a practice up to now, it would be great for proprietary vendors to distribute their tests. They may be less inclined to do that if they are required to make the source of the tests available. I think distributing tests is a practice to encourage, so that's why I'd want to address it specifically.

I wouldn't want to create a new license. As Felix points out, there are issues if you modify GPL. However, FSF shows examples of how to allow exceptions in the text of the notice. GPL definitely has some issues when it comes to re-licensing and combining with others. It's not the only one that's being considered, and others may have fewer issues.

BTW, we had to "choose" a license in order to create the NUnitLite project on CodePlex in the first place, which is why it looks as if the decision is already made if you go to the site. I've put in the suggestion that they allow the license choice to be left open until there is an actual release.
>Charlie, Monday, May 22, 2006

---

If you want your application to be broadly adopted, you are likely to have to use a non-viral license. From discussions with Jim Newkirk, this was the very reason that NUnit chose not to use GPL or LGPL, butto choose a more flexible license. Realize that your comment "I'm not going to sue anyone for using NUnitLite" and the GPL are inherently at odds. The GPL is a license that espouses a particular development paradigm. If you want to use that paradigm, fine. If you don't, then you probably should use a different license. 

I applaud you for working on such a project and giving it away. Think about what you are trying to accomplish and pick a license that supports that. The point about the tests being a derivative work, and linking with the runtimelibrary will be sticking points for lots of people that would likely love to use your application. Is the possibility that they will use and extend the application in new ways and not provide those changes worse than the fact that they can't use the application? There may be a license that supports the specific goal you are after--though I don't know what it is. I wish you well, and hope that you chose a license that will allow me to use your tool.

-mark
>Mark, Wednesday, August 2, 2006

---

Mark: You'll note that we have switched to the OSL. See http://www.codeplex.com/Project/License.aspx?ProjectName=NUnitLite. I should have blogged about this change back when it happened, but I was on the road.

My actual comment was "I don’t plan to sue anyone for testing their code with NUnitLite" which is substantially less broad than what you quoted me as saying. The fact is that there is a long tradition of people using GPL but also making statements of intent as to what they would and would not consider as a breach of the license. There's even a template on the FSF site that helps you grant such exceptions.

Some of my earlier arguments were around this point: I can use GPL without espousing the entire philosopy behind it, because I'm the only one with standing to enforce my own license. I still believe that to be technically true, and everything I've read on the subject indicates that I'm right. However, I've come to believe that it doesn't make any sense to use the GPL if you don't plan to interpret it strictly, as its authors intended. It just creates too much confusion.

Regarding picking a license that supports what I'm trying to accomplish... I have. I'll explain the reasons in a new article. 

Charlie
>Charlie, Wednesday, August 2, 2006

---

It's got to be LGPL - this is exactly the scenario it's meant for: keeping the module 'open' whilst not having any licence impact on the containing application.

Incidentally very, very interested in NUnitLite. Started to attempt it myself (for embedding NUnit test runner within our app) and whoo - all those interfaces! Much simpler if someone who knew what they were doing did all the work ;-)
>piers7, Monday, July 24, 2006

---

**discount zertifikat einfach erkl√§rt**

It&#039;s the Tests &raquo; Blog Archive &raquo; NUnitLite and the GPL
>discount zertifikat einfach erkl√§rt, Friday, February 14, 2020
