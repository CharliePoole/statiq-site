Title: "What's a Test Worth?"
Published: 11 Sep 2005
Tags: [TDD, XP, It's the Tests]
---
In a discussion about unit-test suites that take too long to run, Ron Jeffries writes

_Splitting the tests into slow and fast is a tradeoff, and it's an easy one. But is it ideal? I think not. I think a better approach might be to split the tests into "likely to provide interesting information" and "unlikely to do so". Then make the ones that are likely, also fast._

This struck me as an interesting point.  If the value of a test is seen as the amount of information it is likely to provide and the cost is - at least in part - the time to run it, then the problem of which tests to keep in that "too slow" test run is quite similar to the value versus cost balancing problem we face in XP when we schedule stories into an iteration.

<!--more-->This is something we already know how to do. In scheduling stories, we fix the allowed total cost by time-boxing and try to get the most valuable ones into the iteration. Our objective is to maximize the value delivered per iteration, which essentially requires scheduling those stories first, whose ratio of value to cost is highest.

Seen in this light, setting an arbitrary length on test run time makes quite a bit of sense. Within a given amount of run time, we want to include those tests that are the "best buy" of information for time spent. This is time-boxing applied at a very low level. It's also a great example of the fractal nature of XP, noted by Kent in the second edition of _Extreme Programming Explained_.

So what exactly makes one test provide more value - that is, information - than another? I can think of three kinds of information that a test gives me:

1. Design Information - Driving where the code needs to go
2. Failure Detection - Whether the code is or is not working correctly
3. Failure Diagnosis - What has caused the code to stop working

I'll leave aside the first of these, since the thread that inspired me had to do with test execution, not test writing. As luck would have it, the other two seem to be in conflict, calling for us to make one of those annoying tradeoff decisions that plague software development.

Why are they in conflict? Imagine a single test case which would fail on any application error whatsoever. As far as detection of errors goes, you wouldn't need any other tests. Unfortunately, such a test would be of no value in diagnosing the cause of the error.

On the other hand, a test that can only fail if a single line of code in the application is changed, has very high diagnostic value but can, by definition, only detect a small percentage of possible errors in the application.

It seems to me that TDD gives us the best combination of these values. We write small, isolated tests, giving us good diagnostic value. But we write lots and lots those tests, all independent, giving us broad failure detection. The inevitable gaps between those tests, are filled in by higher level tests -customer/acceptance/story tests - and by the practice of adding unit tests when an unanticipated failure occurs at a higher level.

But - in spite of TDD - sometimes the tests just take too long. So what should we do? I don't think there's a pat answer, but here are some rules of thumb.

1. Divide the tests by level of granularity, depending on your project. For example, one project might have customer tests, module-level integration tests and unit tests, while another might only find two levels
2. Give each level an execution time budget.
3. Within each level, eliminate duplication. Consider two tests to duplicate one another if the same coding error would cause both of them to fail. In that case, you don't need both tests.
4. Duplication across levels is less of a problem - even expected - but you might consider eliminating an entire level if you have more than two of them.

All in all, tests that take too long to run should generally be made to run faster. But it is sometimes possible to end up with a subset of tests that don't carry their weight. You should treat them just as you treat any other code that is no longer useful - remove them.
