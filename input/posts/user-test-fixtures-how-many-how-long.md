Title: "User Test Fixtures: How Many? How Long?"
Published: 16 Oct 2005
Tags: [NUnit, TDD, It's the Tests]
---
There's a discussion going on right now on the Yahoo Test-Driven Development list regarding the  use of a single instance of the user TestFixture  class in NUnit 2.x, as compared to multiple instances in NUnit 1.x and in JUnit before it. A related issue, discussed on the same list a while back, is the question of the lifetime of that single instance. Here's my take on how the issues relate and how NUnit deals with them.

<!--more-->
**How Many Instances?**

The original xUnit frameworks were all based on the Composite pattern and built a tree of tests prior to running them. In order to build such a tree, there had to be some object to represent each node. That seems to be one reason for having a separate instance of the user test class for each method.

In JUnit (pre version 4, anyway) and in NUnit 1.x, the user test class **isa** TestCase and is loaded in the tree itself. Like this...

```text
    TestSuite
     |
     *- User Test Class Instance ( method 1 )
     |
     *- User Test Class Instance ( method 2 )
```

Since the user test class forms a part of the tree, the creation of a separate instance for each method is pretty much a foregone conclusion. **NUnit 1.x** followed this pattern. 

With **NUnit 2.0** the composite pattern was modified, so that the participating classes were all internal to NUnit and user test class, now called a test fixture class, was only referenced by the tree. As a result, the user class was no longer required to exist in a separate instance for each test case in the tree. The way was open for a design choice. The design _might_ have looked like this...

```text
    NUnit TestSuite
     |
     |- NUnit TestCase--------------------> User Test Fixture Object
     |
     *- NUnit TestCase--------------------> User Test Fixture Object
```

...with semantics quite similar to the older design. **That isn't how we did it.** Instead, a single object was used, with each test case referring to that object...

```text
    NUnit TestSuite-----------------------> User Test Fixture Object
     |                                             ^
     |- NUnit TestCase-----------------------------|
     |                                             |
     *- NUnit TestCase-----------------------------*
```

It can be - and has been - argued that this was not the best choice. Indeed, it has opened up the possibility of test interdependencies that could not occur using the older design. On the other hand, it facilitates future hierarchical setup mechanisms, of which the current TestFixtureSetUp is the first example.

 In any case, that's how the current versions of NUnit work with regard to the _number of instances_ of the user test fixture that it creates. Future releases are likely to continue to work the same way. If someone would like to see multiple instances of the fixture object, they can use the extensibility features introduced with 2.2.1 to create a sort of fixture that works differently.

**How Long to Live?**

Again harking back to the early xUnit implementations, since the user class instances formed part of the tree that represents the tests, they had to be constructed at the time the tests were loaded. That meant that any side effects in the constructor of the class took effect only once per load, rather than once per run.

That's why side effects in test class constructors are dangerous: the test doesn't know what sort of runner will be running it. The runner might simply load the test,  run it once and then exit. Or it might load the test and keep it around for a while, running it from time to time! In fact, that's what gui runners generally do.

With **NUnit 2.0** it became possible to separate the creation of the user test fixture objects from the loading of the tests. As such things often go, the opportunity was missed, and the older behavior remained. In the NUnit 2.0 gui, for example,  the fixture objects are created when the tree is first displayed. So long as the tests are not reloaded, the originally created objects are used each time the tests are run. So long as the fixture objects are stateless or only maintain state in very restricted ways, there is no problem.

Beginning with NUnit 2.2, this behavior has changed slightly. The fixture object is no longer constructed until the first time the test is run. This prevents some errors that can occur on initial load when the fixture constructor has side effects.

_By the way, it seems that I missed this change when I wrote the release notes for NUnit 2.2, so don't be surprised if you didn't know about it._

In any case, this is not a complete solution. The fixture object still stays around for subsequent runs. A coming release will dispose of the fixture at the end of the test run.
