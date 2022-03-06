Title: Moving to TestCentric 2.0
Tags: [NUnit, TestCentric]
---
NUnit 2.0 through 2.7 was delivered as an all-inclusive package: test framework,
console runner and GUI runner. With NUnit 3 that changed. The framework and
console runner became separate packages and the GUI was no longer developed.

Like many NUnit users, I happen to be a fan of standalone test runners - as
opposed to those integrated in a commercial IDE. I set up the TestCentric
project in order to continue development of my own ideas about what a GUI
test runner should be. That was the origin of the TestCentric GUI.

## TestCentric 1.0 Through 1.6.2

The 1.x version of the TestCentric GUI was intended to pretty much look like
the original NUnit GUI. It used a lot of code from the NUnit GUI but had its
test-running functions modified to work with NUnit 3.0.

Using a separate process per test assembly, the GUI is able to run NUnit Tests
targeting .NET Framework 2.0 through 4.8, .NET Core 1.0 through 3.1 and .NET 5.0,
all in the same test run. At the time of its release, it was the only standalone
runner able to do so.

Version 1.6.2 of the GUI was the last release of the 1.x series. I'm now putting
my efforts into the next major release, TestCentric 2.0. The examples that
follow are based on the second alpha release of 2.0.

## The TestCentric 2.0 GUI

All through the development of TestCentric 1.x, a separate "Experimental GUI"
has been delivered alongside the standard version. With 2.0, that experimental
version has been updated to become the new face of the application, as shown in
_Image 1_.

<figure>
  <img src="/images/TestCentricGui1.png">
  <figcaption>Image 1</figcaption>
</figure>

As you can see, the layout is the classic two-panel display: a tree or list of tests
on the left with details displayed in tabs on the right. In the image, the tree
display has been set to use the familiar NUnit test tree. The "Test Properties" tab
on the right shows information about the selected test.

The example in _Image 2_ shows a slight variation. The user has selected the name
of an assembly on the left and the "Test Properties" now include information about
the test package itself. This is done when either an assembly or a test Project is
selected and is a new feature of the 2.0 GUI.

<figure>
  <img src="/images/TestCentricGui2.png">
  <figcaption>Image 2</figcaption>
</figure>

One of the most important features of the TestCentric GUI is the flexibility of the
"tree" display. In addition to the NUnit tree, it may be set to display lists of
tests or fixtures and those lists may be grouped in a variety of ways. (Users of
Visual Studio will be familiar with this style of display.)

In _Image 3_ the user has chosen to display a list of tests grouped by outcome. Currently,
the list may be group by Assembly, Fixture, Category, Outcome or Duration. In this example,
the user has selected the "View XML" tab on the right-hand side, in order to view the XML
representation of a test result.

<figure>
  <img src="/images/TestCentricGui3.png">
  <figcaption>Image 3</figcaption>
</figure>

You'll note that the right-hand side also includes tabs for "Errors and Failures" and
"Text Output." These have been carried over from the version 1 GUI and are under
review for potential modification and inclusion in the new GUI. For more information,
see the "What's Coming Next" section, below.

#### The "Mini-GUI"

<figure class="left">
  <img src="/images/TestCentricGui4.png">
  <figcaption>Image 4</figcaption>
</figure>

In addition to the standard GUI layout, the new GUI continues to support the "mini-GUI"
layout, as shown in _Image 4_. This layout is almost identical to the left-hand panel
of the standard layout, except that the progress bar is included.

To make up for the absence of the right-hand side tabs, various popups will be supported.
In the current alpha release, there is only a Properties Dialog, which provides the same
information as the Properties Tab. Additional windows will be added as development progresses.

## What's Coming Next?

I expect to have additional alpha releases with fixes, new features and breaking changes.
Once the feature set is stable, I anticipate a single beta followed by the final release
sometime before the end of the year.
