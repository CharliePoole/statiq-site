Title: The Ebb and Flow of Open Source
Published: 10/8/2020
Tags: [Open Source, NUnit, TestCentric]
---
I'm in the process of moving many of my old posts to a new website - this one -
and that involves thinking about some things I have not thought of in years.

Yesterday, I imported two old posts from 2005, which caused me a lot of reflection:

* [The Death of NUnit: Will it be Ice?](/technical/the-death-of-nunit-will-it-be-ice.html) (6 November 2005)
* [The Death of NUnit Revisited: Will it be Fire?](/technical/the-death-of-nunit-revisited-will-it-be-fire.html) (30 December 2005)

Believe it or not, back in 2005, NUnit was considered a pretty old piece of software.
Originally published in 2000, it had become the major xUnit-style test framework for
.NET developers. But NUnit 2.0 was released in 2002, 2.1 in 2003 and 2.2 in 2004.

But in 2005, NUnit was in a bit of a slump, only releasing bug fixes and minor improvements.
Jim Newkirk, Michael Two and Alexei Vorontsov, who were the major developers of NUnit V2, had
moved on and there was only one major contributor to the project: me. There were 5 point
releases that year, 2.2.1 through 2.2.5 and that was about all I could manage.

Then, in that same year, Microsoft announced that the Visual Studio 2005 Team System (VSTS)
would support unit testing out of the box. People began to speculate about the "Death of NUnit."
Would there even be a need for independent Open Source test frameworks once Microsoft produced
a framework of their own?

That bothered me. I wanted to make some kind of statement. I decided to embrace the notion
that the project _could_ die and think about how and why it might happen. I used two lines
from a poem of Robert Frost as my theme...

<blockquote style="margin: 20px 10%">
Some say the world will end in fire;<br>
Some say in ice.
</blockquote>

You can read the full poem in the first of the two posts listed above. In fact, if you haven't
read them, how about doing it now? It will make the rest of this easier to write. I'll wait. :-)

-----

Back already? OK, as you have seen, I used death by ice to stand for the project's simply winding
down, freezing, failing to produce new innovations. Death by fire, for me, stood for loss in the
face of competition, which might come from other open source frameworks or from commercial
products like Microsoft's.

Those two posts kicked off quite a lot of discussion, some of which I preserved as comments.
Partly as a result of the exposure, a number of new contributors were attracted. By 2007 we were
back on track with a new feature release. In fact, NUnit has continued to produce new releases
for 15 years since it's death was first announced!

It seems that there is an ebb and flow in projects as a whole as well as in the work of
individual contributors. Things can slow for a while and then pick up again. Some developers
find they don't have the amount of time available that they once had. Other developers step up.
Some folks find that their particular interest or focus changes with time. They may step back
from one project and move to another.

That's the place I find myself in now, which is probably why those two old posts gave me so
much pause for thought. I have stepped back from NUnit significantly, although I'm still a part
of the project. My work there is pretty much oriented toward the engine.

My main focus in Open Source these days is the TestCentric GUI runner and the TC-Lite test
framework. I've switched most of my development work to Linux, where I use C# and .Net 5.0
but I still run tests under NUnit!
