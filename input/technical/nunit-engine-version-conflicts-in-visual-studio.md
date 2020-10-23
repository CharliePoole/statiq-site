Title: NUnit Engine Version Conflicts in Visual Studio
Published: 10/20/2020
Tags: [NUnit, VS Test Adapter, NUnit Engine]
---
There have been some issues filed recently about exceptions being thrown when running
NUnit tests under the VS test window. In many cases, they result in trying to install
two different versions of the NUnit Engine into the same project directory. I'll try to 
explain how the problem arises and what to do about it.

There are a number of test runners one may use to execute NUnit tests. In this post I'll
focus on the NUnit Console Runner and the Visual Studio Test Adapter. Each of them uses
the NUnit TestEngine and is bundled with a copy of it. Each of them expects to use the
version of the engine against which they were built.

In the cases that I have seen, both the Console runner and the VS adapter were installed in
the same project. That caused the engine assemblies to be copied into the target directory
twice. That doesn't cause a problem if identical assemblies are used in each case.

However, if the two runners in use are built against different versions of the engine, a
conflict arises. If we are lucky, an exception will be thrown - I say lucky because then
we at least know that there is a problem!

The solution is simple: if you install packages for both runners into the same project
you must use runner versions that reference the __same engine version__. Consequently,
you have to know the version of the engine that is used by the runner.

In the case of the Console Runner, it's easy: the engine version is always the same as the
console package version. When NUnit 3 was released, we divided it into multiple projects,
which publish releases independently. However, we elected to keep the console runner and
engine together for ease of development and testing. So, if you use console runner version
3.10, for example, you are also using the 3.10 version of the engine.

With the adapter, it's not so simple. The releases come at a different pace from the engine and
it has not always been upgraded to the latest version. That decision is made each time the
adapter is released and - unfortunately - it is not always called out in the release notes.

To fill that information gap, I created the following table showing which engine version is used
with each release of the adapter.

| Adapter Version | Engine Version |
| :-------------: | :------------: |
|      3.0        |     3.0.1      |
|      3.2        |     3.2.1      |
|      3.4        |     3.4.1      |
|      3.5        |      3.5       |
|      3.6        |      3.5       |
|     3.6.1       |      3.5       |
|      3.7        |      3.6       |
|      3.8        |      3.7       |
|      3.9        |      3.7       |
|      3.10       |      3.7       |
|      3.11       |      3.7       |
|     3.11.1      |      3.7       |
|     3.11.2      |      3.7       |
|      3.12       |      3.7       |
|      3.13       |      3.7       |
|      3.14       |      3.10      |
|      3.15       |      3.10      |
|     3.15.1      |      3.10      |
|      3.16       |      3.10      |
|     3.16.1      |      3.10      |
|      3.17       |     3.11.1     |

You can use the above table to select compatible versions of the two runners, bearing in
mind that the engine version is also the version of the associated console runner.

Let's try an example. Suppose you are using version 3.10 of the console runner. What
versions of the VS adapter can you safely install in the same project? Looking at the
table, you can see that only versions 3.14 through 3.16.1 of the adapter use the same
engine. Going in the other direction, if you were using version 3.17 of the adapter
then you should only combine it with console runner 3.11.1.

A few final notes:

1. You __may__ sometimes be able to get away with combining different versions of the
engine without any visible error. However, it's not a safe practice and a new test
may end up triggering a hidden problem when you least expect it.

2. There is __no__ compatible console runner for the 4.0 beta release of the adapter.
Except for internal testing, the adapter is never built against pre-releases of the engine.
