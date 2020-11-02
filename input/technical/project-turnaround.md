Title: "Project Turnaround: XP to the Rescue?"
Published: 3/26/2002
Tags: [Articles, Extreme Programming]
---
<blockquote style="text-align:center"><b>NOTE:</b> This article is slightly modified from a <a href="/files/xp2002paper.html">workshop paper</a> I presented at XP2002.</blockquote>

In July of 2001, I spent three weeks trying to turn a project around. I had only recently learned about Extreme Programming and this was my first attempt at using it. The short-term results were quite spectacular, the long-term, less so.

The project manager had just left and a replacement was selected but
not available for three weeks. I had been working as a lead responsible 
for one part of this project. With my group's work basically done, I was
anxious to leave - I'm an independent consultant and value my time off.
Asked to fill the project manager role temporarily, I agreed with the
understanding that I might make changes in how the project operated.

I accepted this task as both a challenge and an opportunity
to learn. I felt I could accomplish something in the short period
but I wondered whether the changes I made would last.
  
### Problems Faced

* The team didn't know what it was supposed to be doing. We had written
  requirements but there were large gaps. We had no clear way
  to fill those gaps or to clarify what the requirements meant.

* The project was late - already at twice its original estimate.
  Although two throwaway demos had been written, there was still
  no integrated version of the real product after a year in development. 

* Morale was low. Team members failing to meet deadlines were closely 
  questioned in our weekly status meeting and often left it feeling
  humiliated. Two project managers had already been removed and my own 
  short-term status was probably not a great source of comfort.
  
### What We Did

We canceled the much-feared weekly meeting and replaced it with a Monday
morning breakfast at which team members told what they expected to work
on that week and what help they might need. We acquired extra machines
or integration and a "war room" where we could display charts and 
diagrams.

I also set up a Wiki page describing what I was trying to do and ended up
receiving helpful suggestions from many in the XP community. See
<a href="http://c2.com/cgi-bin/wiki?ThreeWeekProjectTurnaround">ThreeWeekProjectTurnaround</a>.

We arranged for an Onsite Customer who moved into the team area.
The customer and two other team members formed a 
_Requirements Coordination Group_, which went through the backlog
of unanswered questions and published the results on a web page.

The Planning Game identified stories previously unknown to the developers.
We dropped lower priority stories and created a new release plan. The
release date was farther out than management wanted but was
accepted after some discussion.

We created a plan for the first iteration and began working on it. Those
of us who had already begun to work with Unit Tests and Refactoring
continued to do so. Some Pair Programming was practiced on a voluntary basis.
	  
### Emergency Drill

Our planning process had given us a set of stories to finish in the
first two weeks. At the beginning of the second week, a crisis:
a major purchasing group in another city needed to see a working system
by Thursday. Otherwise, they would drop our product.

The team decided to meet the date. Our position was improved by the 
planning that had already been done. We revised the iteration plan and
worked excessive hours for a few days. On Wednesday night, the first 
integrated build of our application passed its tests. It was installed
on a clean machine and packed for shipping. We were heroes for a day.
	  
The third week of my three-week tenure was spent on firming up the
practices used by the team and planning for continuity.
We had started with practices that met our immediate
needs. We were fully practicing Continuous Integration, Small Releases,
Onsite Customer and the Planning Game. Our next emphasis was to spread
Test Driven Development and Refactoring across the whole team. We were
trying to introduce Common Code Ownership in spite of organizational
and ego problems. In short, we needed enabling practices to allow us
to continue what we had started.

Our fear was that the new manager wouldn't support this approach.
In the team, we planned for continuing the growth of XP practices so
long as they weren't explicitly forbidden. We discussed the need for
someone to act as an evangelist. I lobbied management vigorously.
	
### Aftermath

I maintained some contact with the team over the following months to
find out how things turned out. The new manager didn't make much effort
to learn about XP. However, the project was moving along well and she
didn't interfere at first. During the next few months, the team
continued with the practices we had introduced and added a few more.
Iteration planning continued and estimates became more accurate.
Tests were automated.

After a few months, the new manager began to change some of the practices.
The onsite customer was reassigned and was only occasionally available
to the team. Time spent writing tests was discouraged. The project 
progressed but at a slower rate. As formal testing approached, more 
shortcuts seem to have been taken. One team member describes it this way:
"As we get closer to the end, management wants to sprint across the finish line."
At the time of writing, the team was working to correct performance 
problems that arose in final testing. The product will ship - an outcome
that was once in doubt - but the full promise of our initial experience
using XP hasn't been fulfilled.
	
### Lessons Learned

* Relatively trivial, symbolic changes are a good way to begin.
  Team morale increased out of proportion to the effort put into
  these changes and everyone expressed much stronger interest
  in trying new practices.
	
* Work first on what's bothering people. In our case, this was the
  lack of clear understanding of requirements. The team was enthusiastic
  about having a clear statement of what was expected and a schedule 
  that they believed in. 
	
* Harness emergencies. Our "crisis" gave us much greater
  freedom to develop as we saw fit. Solving this urgent problem 
  produced a strong sense of accomplishment.

* Even a few practices can help. We received tremendous benefit from
  adopting four practices consistently and trying out two others to
  a lesser extent. We weren't yet doing XP but we were on our way.

* Use the community. Of course, everyone reading this knows that
  already, but it bears repeating.

* Keep improving your process. As soon as the team stopped making
  improvements, they began to coast and then slow down. Lack of
  management support played a part here but wasn't the whole story.

* Find an XP Evangelist. It's outside the XP roles but seems to
  be an important one with a different set of skills. If I had
  found someone to replace me in this role, things might have been different.

* Do use XP for short-term problem solving even if you aren't sure
  you'll be able to make a permanent change.
