Title: "NUnit Stable Releases"
Published: 04 Nov 2007
Tags: [NUnit V2]
---
While NUnit 2.4 was in development, I continued to maintain the NUnit 2.2 series as a stable, bug-fix-only branch. NUnit 2.2.10 was the last in that series.

After NUnit 2.4 came out, I decided to experiment with using a single code line. I would make bug fixes and - at the same time - carefully add new features. Each of the releases since then (2.4.1, 2.4.2, 2.4.3 and 2.4.4) has fixed a number of things, introduced some new features and broken one or two things.

A single code line can work, but it requires discipline. Part of that discipline involves doing many of the same things manually that would be handled automatically by a well-thought-out branching structure. The outcome of my experiment is that the approach didn't work well for me, so I'm going back to the use of release series branches.

If you use the source code from CVS, the Release-2-4-Branch will get you the latest fixes in the 2.4 series. I'll be tagging the next releases - 2.4.5, etc. - on that branch. The CVS head will be used for any new development, but I'm not sure how much of that there will be. My best guess is that there will be a 2.5 feature release and that will be it for NUnit 2.x.

NUnit 3.0 is now in the planning stages and I'll be blogging about it in the coming weeks.
