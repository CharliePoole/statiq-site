Title: "NUnit on Microsoft CodePlex?"
Published: 16 May 2006
Tags: [General,NUnit, It's the Tests]
---
A reader asks "What is this about?" pointing to the new Microsoft <a target="_blank" href="http://www.codeplex.com">CodePlex</a> site, which mentions NUnit.

<!--more-->
For more info on what Microsoft is trying to achieve with the site, keep an eye on <a href="http://blogs.msdn.com/jamesnewkirk">Jim Newkirk's blog</a>. What I'll try to do in this article is describe what the NUnit team is doing on CodePlex and why we're there. Just for fun, since we started out with a question, I'll organize it as a sort of imaginary interview...

**So what is this about?**

Microsoft has announced a new open source project repository called CodePlex. They invited a number of non-Microsoft open source contributors to help them in designing the site and to place projects on it during the Beta period. I was one of that group and I'll be working with other members of the NUnit team to put the new site through its paces.

**Why is Microsoft doing this?**

I have no inside info on this one. I do know that a number of people at Microsoft would like to see the company come to a better understanding of the open source community and have championed this site as a way of drawing closer to it. It most likely has something to do with competitive advantage as well. :-)

**Why was NUnit invited to participate?**

Because we're one of the most widely used pieces of open source software used on the .Net platform.

**Why Did You Agree to be Involved?**

Because it may be an opportunity to be part of an "opening up" of Microsoft to a community toward which it has been less than welcoming in the past. Because being involved is the only way to have any influence at all. Because being involved is the only way to judge how welcome the open source community really is at Microsoft.

**Do You Have Any Reservations?**

You're kidding, right?

**So, that's a Yes?**

Well, Microsoft has said some very strange things about open source in the past and continues to do so. On the other hand, the people we're working with do "get" it. We don't know whether they will succeed in influencing others in their company, but we hope they will and we plan to help them.

**What about technical issues?**

The biggest one seems to be how well a VSTS-based site will work for folks using the free clients that Microsoft is distributing - or the future open source clients that we imagine will spring up. This is a good example of the kind of issue that we can only resolve by putting some code on the site.

**Will you be moving NUnit to CodePlex?**

Maybe, but not at present. We're just beginning the release cycle for NUnit 2.4 on SourceForge. We expect to complete the release there over the next few months. In order to evaluate the site more critically, we want to do an entire project and have selected the NUnitLite project for that purpose.

**What is NUnitLite?**

I envision it as a light-weight version of NUnit that people can add right into their projects. There are many purposes for which the full NUnit framework is overkill. I'll be blogging more about how I see this project but utimately the community will decide what it needs to be.

**Does moving to CodePlex mean you will be using a Microsoft license?**

While I like the Microsoft Common License, it hasn't been reviewed by OSI so many people won't consider it a valid open source license. Also, it's not a template license, which is an approach I've come to appreciate and would prefer in future licenses I select.

NUnit is still using the zlib/libpng license. We're currently planning on GPL for the NUnitLite project and that's what it says on the Microsoft site. There could be some issues with the GPL for those folks who like to distribute their tests with the code, so we'll be seeking input on that question.

**Will you continue to support Mono and other non-Microsoft runtimes?**

NUnitLite will support Mono and NUnit will continue to support it, no matter where we host our project.

**So what's next for NUnit?**

We're considering starting a project for discussing and writing stories for NUnit 3.0 on CodePlex. This could go on in parallel with the hands-on development of NUnitLite. If all goes well with our evaluation of the site, we might then start the next major release of NUnit on CodePlex. Right now, however, the focus for NUnit is on the 2.4 release, which will be on SourceForge.

I'll be blogging more on NUnitLite, NUnit 3.0 and our use of the CodePlex site over the coming months.

---

### Comments

---

Well, the main problem with making NUnitLight GPL is that NUnitLight is meant (as i understand it) to be included in the application itself. This again, as I read the GPL license mean that the the including project in its whole would have to be GPL.

This would take away our option to use other Open licenses or include the code to help out in non open software.
>hfztt, Thursday, May 18, 2006

---

Responding to Mike: Well, there are those who put us down for not using GPL and there are those who consider GPL evil. It's clear that we can't please everyone.

If GPL keeps people from using NUnitLite because they are unable to comply with the terms, that's OK. If GPL keeps them away because they don't understand and fear it, that would be unfortunate, and I agree it's possible. We'll have to provide some interpretation, I suspect. However, other well-known development tools like NAnt and Fit use GPL without any problem.
>Charlie, Wednesday, May 17, 2006

---

I think the GPL will make a lot of people just avoid using it.
>Mike Gale, Wednesday, May 17, 2006

---

If you are - as you seem to be - philosophically opposed to the GPL as a whole, there is no point for the two of us to discuss a minor point of its interpretation. I do agree that the uncertainty is one point against it. However, I'm discussing the pros and cons of various licenses in the same class as GPL - those known as reciprocal licenses - and you have suggested I just use zlib/libpng, which is not one of those licenses. It permits all use and does not require that derivative works be licensed as open source and many of them are not.

In spite of your belief that the existing license has been very good to the community, I have seen the source incorporated in several proprietary products, from which improvements have never been returned to the community. I am therefore motivated to do something different in my next projects and a reciprocal license seems like a logical step. I'm open to discussion as to how to accomplish my goals. I'm not open to discussion of the sort you seem to be initiating, discussion about whether I ought to have such goals at all.

When I hear somebody argueing against the mere consideration of an alternative, I find that I am usually in the company of a zealot. I'm not a GPL zealot, nor am I an anti-GPL bigot. I just want to figure out how to do what I want to do with my work. Please respect my right to do that.
>Charlie, Thursday, May 18, 2006

---

Doesn't the ambiguity of the GPL license give you enough reason to throw it out with the bathwater? Why change from zlib/png? It has been so good to the NUnit community. What is the motivating factor causing you to consider something else for NUnit lite?

And I really don't like your statement, "However, this is only an issue if you distribute your tests."

You know as well as I do that any code deliverable in source form that does not distribute its tests is ridiculous.

So again, I ask. Why are you considering GPL? What is the benefit to the project? To the community? To you?
>Peter, Thursday, May 18, 2006

---

**CodePlex Beta**

Microsoft just launched a Team Foundation Server based online software
development environment for open...
>Impersonation Failure, Monday, May 15, 2006

---

This is not completely decided - we'll be discussing the pros and cons on the NUnitLite site itself - http://www.codeplex.com/Wiki/View.aspx?ProjectName=NUnitLite - where you're welcome to join in. Here's my understanding...

The GPL does not define precisely what sort of "inclusion" or "linking" is needed to create a derivative work. That's up to lawyers and - based on my research - the lawyers are not entirely clear about it. Taking FIT as an example, Ward has stated what he considers to be a derivative work. While that does not change the license, Ward is the only person with standing to sue under the license. Even if he went mad and decided to contradict himself :-) he would not be likely to win in court since he has publicly stated a position. In the Linux world, Linus has made similar statements, which is what makes it possible to write applications that depend on Linux itself! The last may seem silly, but without any court precedents to depend on, someone could always claim that an application was "linked" to the operating system.

Here's what I see with NUnitLite...

In a binary distribution, your tests would reference the assembly and the NUnitLite classes. If you elected to use the source and include it with your tests, the tests would reference the NUnitLite classes. Whether your tests should be considered a derivative work is a legal issue. I agree, however, that it could be confusing enough to put people off using it. However, this is only an issue if you distribute your tests.

OTOH, if you take our source and use it to create NUnitLiteExtended, and distribute that, you are definitely creating a derivative work. You are required to use GPL for it. And that's what I'd like to have happen.

All of this may end up causing us to use some other reciprocal license, perhaps LGPL, perhaps MPL, etc. We need to discuss it. But I don't think it's quite as obvious as one might think that the GPL "viral" property would be triggered just by using it to test your application.
>Charlie, Thursday, May 18, 2006

---

[...] 16 May 2006  [News] Announcing CodePlex Public Beta   Over the past few months a number of people (including myself) worked together with Korby Parnell[3], GotDotNet Guru,&nbsp;to create CodePlex, a new Web-based collaborative development application&nbsp;including Release Management, Work Item Tracking, Source Code Dissemination, Wiki-based Project Team Communications, Project Forums and News Feed Aggregation. Everything you need to successfully run your community project. Some of the projects listed already:  NUnitLite - according to Charlie Pool [4] NUnit 3.0 will most probably be planned and developed on the platform as well.&nbsp; Managed Stack Explorer&nbsp; - Managed Stack Explorer is a lightweight tool that provides a quick and easy way to monitor .NET 2.0 managed processes and their stacks. Team Foundation Server Administration Tool - A central tool to manage user and permissions on WSS, RS and TFS.&nbsp; MSBuild Toolkit for .NET 1.1 - MSBee - The build environment for Everett allowing you to build 1.1 projects from Visual Studio 2005.  Codeplex is a place&nbsp;to create new projects to share with your fellow developers around the world, join others who have already started their own project, or simply use the applications on this site and provide feedback. I am more than happy to announce the first Public Beta! Watch out for James Newkirk post [2] about CodePlex. Much more to come, stay tuned ...  [1] http://www.codeplex.com/[2] http://blogs.msdn.com/jamesnewkirk[3] http://blogs.msdn.com/korbyp[4] http://nunit.com/blogs/?p=26 .NET&nbsp;|&nbsp;Blog&nbsp;|&nbsp;Community&nbsp;|&nbsp;Development&nbsp;|&nbsp;English   Damir Tomicic Axinom  posted on 09:25:44 GMT Daylight Time &nbsp; &nbsp; &nbsp; &nbsp; Comments [0]  Related Posts:[Comic] Dilbert goes Web 2.0[Development] Introducing .NET Micro Framework - Beyond the Compact Framework [Presentation] Download Slides From Israeli TechEd 2006 [Weekend] Das Wort zum Wochenende[Geek] Will Code for a Badge [INETA] Influencer Party at TechEd 2006 Boston [...]
>Damir Tomicic, Microsoft Regional Director, Tuesday, May 16, 2006

---

[...] Charlie blogged a &#8216;press release&#8216; explaining what we&#8217;re doing there and generally giving more details. [...]
>idp :: shmarya.net &raquo; Blog Archive &raquo; Out of the bag&#8230;, Tuesday, May 16, 2006

---

Coming from a security background, I can verify there are many well-meaning people at Microsoft who "get it" with regard to security. They realise it shouldn't be treated as a PR issue and should be critical for Microsoft. These people are also not in charge, and are still some of the most frustrated people I've ever talked to. Microsoft is taking heat from the press and customers on interoperability, so they are treating the problem with a PR initiative to reach out to open source projects. If you're looking to try an experiment away from sourceforge.net, maybe http://savannah.gnu.org/ would be a better choice?
>Matt Hargett, Tuesday, June 13, 2006

---

[...] While some people get nervous about MS and their Open Source efforts (probably for good reason), several teams are discussing about moving their projects from SourceForge onto CodePlex. One of those is NUnit, though as Charlie Poole says: Will you be moving NUnit to CodePlex? [...]
>ablog &raquo; NUnit, and MS&#8217;s new Open Source website, Wednesday, May 17, 2006

---

[...] XP (2)        &laquo; NUnit on Microsoft CodePlex? [...]
>It&#8217;s the Tests &raquo; Blog Archive &raquo; License Questions Regarding NUnitLite, Friday, May 19, 2006

---

I'm sorry that you read me to be a zealot. That certainly wasn't my intent. I didn't think I said anything zealot-like, but I must have. Can we avoid name-calling and just focus on the question I asked? You said above, "We need to discuss it." That's what I thought I was doing. Calling me names to scare me off makes it seem like you would rather not have a discussion at all.

I thought I was just asking a simple question: What is the motivation for considering a new license for the NUnitLite. It sounds like your concern is that proprietary products have been made based on Nunit without returning that work to the community.

Is this correct? Can we focus on this instead of calling each other names?

For the record, I am not opposed to considering an alternative. I asked what the motivations were. I asked how it will benefit the project. How will it benefit the community? The project developers? 

I'm also interested in understanding why you feel that a reciprocal license is better for the project than the zlib/libpng license that is in place for NUnit now.

(I also want to address a slightly older comment and remind you that Ian and Gert (from the NAnt project) have said on more than one occasion that they want to switch from GPL to an Apache or BSD license for NAnt, but the project has then gone round and round trying to figure out how to do it. It isn't something that can easily be done IIRC. You can dig through the NANT developer archives for examples, or here is one: http://sourceforge.net/mailarchive/forum.php?thread_id=3254708&amp;forum_id=863 )
>Peter, Friday, May 19, 2006

---

I think I'll deal with the main issue here by starting a new blog entry, since the mention of a license was only incidental to this one. It's apparently the most interesting part. :-)

Actually, I didn't call you a zealot, I said I wasn't one. I did say that you seemed to be philosophically opposed to GPL, which wasn't intended to be a criticism. So your perception of name-calling is a response to my tone. Just as my perception of you us is in response to the tone I seemed to see in your note.  So we're both responding to more than just the dictionary meaning of the words used.

I felt I had made it fairly clear, in context, what I was trying to do. Other folks responding seemed to get it, based on their responses. You seemed to be ignoring the rather obvious reason one uses GPL: to force derivative works to be open source. I interpreted your ignoring it as objecting to it, perhaps because I was too primed for objections from the "GPL is anti-American" crowd. It was a big assumption and I apologize for it.

I'll go blog in detail about licenses for NUnitLite now.
>Charlie, Friday, May 19, 2006

---

Ugh - VSTS?  Why can't Microsoft use a standard like Subversion or CVS?  At least with Subversion, you can mount the repository as a read-only drive in Windows.  With VSTS, I need to install their client (which I am wary of after having to use SS many years ago).

The rest of the world runs on CVS and SVN for the most part.  I'm pretty much going to avoid any open-source project that I can't use those two tools to contribute with.
>Ned S., Thursday, June 8, 2006

---

Ned: Well, we all have our doubts. That's why we're trying it out. We'll let you know how it works.
>Charlie, Thursday, June 8, 2006

---

>Microsoft has said some very strange things about open source in the past and continues to do so.

How true! <a href="http://www.thunderguy.com/semicolon/2005/05/09/microsoft-doesnt-understand-the-gpl/" rel="nofollow">Microsoft doesn't understand the GPL</a>. Of course, Microsoft is far too big to have a coherent policy on anything at all, but still they have come up with some weird notions. See also <a href="http://www.thunderguy.com/semicolon/2005/06/14/microsoft-versus-free-software/" rel="nofollow">Microsoft versus Free Software</a>.
>Bennett, Sunday, August 27, 2006

---

[...] ScottGu ya había anunciado que el Atlas Control Toolkit iba a utilizar un modo abierto de desarrollo (donde todos puedan colaborar), y además existe la posibilidad que otros proyectos como NUnit se sumen a la idea y se pasen de SourceForge a CodePlex. Inclusive Microsoft está buscando gente para contratar del mundo Open Source para trabajar en el proyecto. [...]
>Edgardo Rossetto .NET: CodePlex beta, Wednesday, July 19, 2006

---

[...] ScottGu ya había anunciado que el Atlas Control Toolkit iba a utilizar un modo abierto de desarrollo (donde todos puedan colaborar), y además existe la posibilidad que otros proyectos como NUnit se sumen a la idea y se pasen de SourceForge a CodePlex. Inclusive Microsoft está buscando gente para contratar del mundo Open Source para trabajar en el proyecto. [...]
>Edgardo Rossetto .NET &raquo; Blog Archive &raquo; CodePlex beta, Wednesday, September 27, 2006
