Title: "NUnit 2.2.10 - Final 2.2 Release"
Published: 16 Mar 2007
Tags: [NUnit V2]
---
Although NUnit 2.4 is almost out the door, there were a few outstanding bug fixes to the NUnit 2.2 series hanging around, so I decided to issue a final 2.2 release. Like all the releases since NUnit 2.2.4, this is primarily a bug fix release.

See the [Release Notes](https://docs.nunit.org/2.2.10/releaseNotes.html) for details of what has changed. You can download it from NUnit.org.

Since NUnit 2.4 is coming out in a matter of days, this release is for those who plan to stay with the 2.2 series for some time. Future work will concentrate on NUnit 2.4 enhancements and on planning for NUnit 3.0.


---

### Comments

---

What are you planning for Team System Test / Orcas testing? It occurs to me that lots of folks have lots of NUnit tests already, and being able to integrate all the different test frameworks together is a very valuable feature, though it isn't clear if the main 'runner' should be the Microsoft tool or NUnit itself. Thoughts?
>Will Ballard, Monday, April 23, 2007

---

NUnit 2.2.10 actually contains an undocumented runner for VSTS tests. It's buggy and incomplete but it can be used to run simple tests. I do a demo, for example using a version of the Money sample, which is built against the Microsoft unit test framework. 

This feature has been separated from NUnit in the 2.4 release. An updated version will be released in the form of an NUnit 2.4 add-in. Some folks have complained about removing this support from the core of NUnit, but there are several good reasons for doing so:

1) The Microsoft unit test framework can't be installed on all platforms
2) The Microsoft unit test framework is not open source
3) The test recognition and loading architecture works better this way
4) There are many third-party frameworks that could also be supported, but putting them all in the core would make it hard to maintain.
5) Making it an add-in allows others to modify and improve it while the NUnit team deals with NUnit
>Charlie, Wednesday, April 25, 2007
