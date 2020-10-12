Title: "Speed Up Reloading in NUnit 2.4.2"
Published: 08 Aug 2007
Tags: [NUnit,It's the Tests]
---
When running under .NET 2.0, NUnit is rather slow at loading tests these days. Although many folks only noticed the slowdown with the release of NUnit 2.4, loading a large set of tests with NUnit 2.2 also takes more time - about twice as much - under .NET 2.0 as .NET 1.1.

There doesn't appear to be any easy solution to this problem. Inefficiencies in the Reflection mechanism and/or how we are using it seem to be the root of it and NUnit has been making more and more use of Reflection as new options, attributes and add-in capabilities have come along.

However, with NUnit 2.4.2, there is a simple workaround that may reduce the pain!

Try these steps:

1. Load your test assembly or NUnit project as usual with the NUnit Gui.

2. Identify a branch of the tree in which you will be working for the next 30 to 60 minutes. This can be either a TestFixture or a namespace that includes multiple fixtures.

3. Right-click on the tree node you selected and select Load Fixture from the context menu. NUnit will reload only the selected tree branch.

4. As you modify your assemblies and reload the tests, NUnit will continue to reload only the tree branch you selected. This can be a substantial time savings. If you wish to select a smaller subset of the tests, you can repeat the above steps. If you need to go back to the full set of tests, right-click in the tree and select Clear Fixture.

At this time, the fixture setting is not remembered between NUnit sessions, so this speed-up is only available to those who keep the Gui running while they work. This is actually how the NUnit Gui is designed to be used.

I'll be working on speeding up test loading in the long-term, but in the meantime I hope this workaround helps.
