Title: "Running NUnit Tests from Code"
Published: 06 May 2006
Tags: [NUnit, It's the Tests]
---
There are some traps involved in running tests from code that are not immediately obvious. This was brought forcibly to my attention when I recently worked on a failing test that someone else had written.

The test in question is internal to NUnit. It attempts to run a single fixture out of a particular assembly, and then verifies that the results are as expected. I'll cover some of the traps encountered in the order that I resolved them.

<!--more-->
**Trap 1 - Tests versus Results**

After running the test suite and getting a result, the test code asserts as follows:

```charp
Assert.AreEqual( 1, result.Test.TestCount );
```

This was where the developer writing the test was stuck: the test count was 90 rather than 1, which seemed to indicate that all the tests had been run. In fact, it didn't mean that at all.

`TestCount` is a property of `Test`, which tells how many test cases it contains. It has nothing to do with what is being run. Info about what has been run and whether it passed is contained in a `TestResult`, not a `Test`. So the assertion should be examining the result.

**Trap 2 - All Tests Run?**

Even so, we might ask why the count of tests was so large, since the developer was attempting to run a single fixture, containing a single test case! The tests were run using code similar to this:

```csharp
TestSuite suite = builder.Build( assemblyName );
TestResult result = suite.Run( new NullListener(), filter );
```

As it happens, the latest (version 2.3) NUnit code always runs tests from the top down. That is, in order to run a single fixture, the containing suites must be run, all the way up to the top of the tree. The reason for this is that higher level suites may contain SetUp and TearDown code, which must be executed in order for the selected fixture to run correctly. The developer expected the returned **TestResult** to pertain to the test selected by the filter. In fact, it pertained to the entire assembly!

**Trap 3 - NameFilter Confusion**

`TestResult` doesn't contain methods for recursively examining the subordinate results. In order to count the number of cases actually run, we need to summarize the results:

```csharp
ResultSummarizer summ = new ResultSummarizer( result );
Assert.AreEqual( 1, summ.ResultCount );
```

After I made that change, I expected the test to pass. Unfortunately, it didn't. The new error message told me that I had not executed any tests at all! Hmm... Apparently, the filter, which is supposed to select our fixture, is not working as expected.

So far, I've only shown code fragments. To make things clearer, I'll list all the code for the test so far...

```csharp
TestSuiteBuilder builder = new TestSuiteBuilder();
TestFilter filter = new NameFilter( new TestName( "SomeTestFixture") );
TestSuite suite = builder.Build( assemblyName );
TestResult result = suite.Run( new NullListener(), filter );
ResultSummarizer summ = new ResultSummarizer( result );
Assert.AreEqual( 1, summ.ResultCount);
```

Why isn't the `NameFilter` working? The answer is that `NameFilter` compares tests by their `TestName`, which is more than a simple string comparison. In addition to the string name of the test, `TestName` encapsulates a unique identifier, which is given to the test by the loader. The gui has access to this information and is able to construct a filter that locates the exact test, even if there are multiple tests with the same string name in the tree. There is no way to know, before the test is loaded, what unique identifier will be associated with a test.

A corrected version of the above code might work in two different ways. One approach would be to only load the tests starting at the desired fixture...

```csharp
TestSuiteBuilder builder = new TestSuiteBuilder();
TestSuite suite = builder.Build( assemblyName, "SomeTestFixture" );
TestResult result = suite.Run( new NullListener() );
ResultSummarizer summ = new ResultSummarizer( result );
Assert.AreEqual( 1, summ.ResultCount );
```

Using this approach, the full tree of tests in the assembly is not loaded. `TestSuiteBuilder` is able to load tests for a particular fixture, suite or namespace. In fact, this is how the gui and console runners deal with their optional `/fixture` parameter.

Unfortunately, this approach would not work for the test in question. The developer was testing our new higher-level setup and teardown functions, which require the entire test suite to be present. Using existing code, he could have loaded the entire test but executed only a single fixture like this...

```csharp
TestSuiteBuilder builder = new TestSuiteBuilder();
TestSuite suite = builder.Build( assemblyName );
Test test = suite.Tests[1] as Test; // See note
TestFilter filter = new NameFilter( test.TestName );
TestResult result = suite.Run( new NullListener(), filter );
ResultSummarizer summ = new ResultSummarizer( result );
Assert.AreEqual( 1, summ.ResultCount );
```

>**Note:** It turned out here that the test to be executed was in a known position in the tree. If that were not the case, a recursive search of the tree would be needed to find it.

The example shows approximately how the gui itself constructs a filter to select certain tests. Of course, in the gui, the user helps out by selecting the exact test or tests to be run from the tree.

**Conclusions - Improvement is Possible**

One conclusion I reached in examining this test is that **NUnit** actually needs an easier API for running a test based simply on it's name. As shown above, we have one for loading tests, but not for running them. The distinction wasn't important before, but with higher-level setup and teardown, it's significant.

I wouldn't have realized that a design problem existed so quickly without the problems presented by this test. We'll likely add a new filter to make this possible.

This article has covered one way of running **NUnit** tests through code: by loading and executing a **TestSuite**. This is a pretty low level approach, but is needed for our own tests and may also be needed by some folks who use **NUnit** from their own code. The more normal way to load and run tests is through one of our classes that implement the **TestRunner** interface. I'll write about that another time.

---

### Comments

---

Hi Charlie! Thankyou for posting this! In our custom testing framework, when we added NUnit support, we used the NUnit.Core.SimpleTestRunner and passed either one test name or multiple test names to the Run method when running NUnit tests in code. The only downside is that the setup/teardown is called on the fixture for each test (a slight performance hit) and I had not found an easy alternative until your post here.

Looking forward to a new API to run one or more tests in code!
>Jim Bennett, Tuesday, May 16, 2006

---

For Jim: It's of course intended that SetUp/TearDown run for each separate test. Why not simply use TestFixtureSetUp and TestFixtureTearDown if you want the code to run one time only? Using SimpleTestRunner and the TestRunner interface is probably a lot more future-proof than what I describe here - I'll have to blog on that approach soon, I guess.
>Charlie, Tuesday, May 16, 2006

---

Hi Charlie, I forgot to give the nunit team Kudos for having good source which is flexible! Is there a different TestRunner I can use that will allow me to run a selection of tests in one fixture where the fixture setup/teardown only fires once?

I'm not super familiar with the nunit implementation. I had to look at your tests and use the mini walker from our CodeProject article to figure out a few things already:) I was trying to run a fixture of tests in one batch hoping that I could get one fixture setup and teardown call around all of the tests that I wanted to run. Right now the way I'm using it I think if anyone was creating a db connection in the fixturesetup for a selection of tests, it might slow down if the fixturesetup had to fire for each test. It is just an optimization I was looking for. What I have works fine right now, but if there is a better way I'm all for it!
>Jim Bennett, Wednesday, May 17, 2006

---

[...]  [...]
>mattonsoftware.com : .NET Resources, Saturday, May 6, 2006

---

Jim: I missed your comment back when you wrote it. Belatedly, that's exactly what TestFixtureSetup and TestFixtureTeardown do. They execute one time only for all the tests in the fixture. You're advised to only do global things - like setting up a DB connection - in the fixture setup. I suggest you save the connection in a static member variable, just to remind yourself not to change it once its set up. In the current NUnit releases, you could also use an instance variable, since we use the same instance for each test case, but this could change in the future.
>Charlie, Thursday, August 24, 2006
