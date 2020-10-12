Title: "What's With NUnit 2.6.5?"
Published: 01 May 2018
Tags: [NUnit,It's the Tests]
---
Some folks have expressed surprise at my release of NUnit 2.6.5. Their surprise is **no surprise**, given that the NUnit framework is now at version 3.10.1!

So why release a 2.6.5 version now?

For an answer, we have to go back to the first release of NUnit 3.0. At that time, we felt that NUnit 3 was going to be so attractive that people would rush to convert. For that reason, NUnit 2.6.4 was to be the last release of the NUnit V2 series. We archived the code and stopped accepting bugs on it.

We weren't entirely wrong. NUnit 3 is a great improvement, so great that even those who are **unable to convert** to it wish that they could! And there lies the problem we didn't consider: there are many, many **external factors** that can keep people from being able to convert. We knew that this would be a factor but seriously underestimated how many people would be affected.

To make matters worse, as NUnit 3 progresses through new releases, these folks get farther and farther behind, making their eventual conversion that much harder. The list of things you have to change in your code when converting from NUnit V2 to the latest NUnit 3 version just keeps getting bigger. It would be very convenient if folks on V2 could keep up with at least some of the ongoing changes.

Enter the **<a href="https://github.com/nunit-legacy">NUnit Legacy Project</a>**!

The aim of this project is to support users of older NUnit V2 software in two **specific** ways:

1. By giving them information about exactly how much of their code may need changing in order to convert to NUnit 3.

2. By providing them with ways to keep their code more closely conformant with NUnit 3, where doing so doesn't require a complete re-architecting of NUnit V2.

**NUnit 2.6.5** is the first software release from the **NUnit Legacy Project**. It provides enhancements in both of the categories just listed, including a compatibility report that points out the places in your code where changes may be needed in order to upgrade. Additional enhancements in 2.6.5 provide compatible replacements for classes and methods that are no longer supported in NUnit 3, allowing you to immediately reduce the list of compatibility issues you may have to deal with.

Documentation and downloads for NUnit 2.6.5 and coming releases may be found at **<a href="http://nunitsoftware.com/nunitv2">http://nunitsoftware.com/nunitv2</a>**.

---

### Comments

---

[&#8230;] What&#8217;s With NUnit 2.6.5? &#8211; Charlie Poole [&#8230;]
>The Morning Brew - Chris Alcock &raquo; The Morning Brew #2576, Tuesday, May 1, 2018
