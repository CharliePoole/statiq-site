Title: "Time for a New NUnit"
Published: 13 Dec 2007
Tags: [NUnit]
---
As I've worked on NUnit 2.4 and its follow-ups, I have begun to feel that it's time for a much more significant update to NUnit, a rethinking of what it's all about and how it should work. Of course, at the same time, I don't want to lose all the work that has gone into NUnit up to now. Puzzling over this, and helped by a group of dedicated NUnit users and contributors, I think I now have a direction to go.

The new NUnit 3.0 Extended Testing Platform is at the same time a simplification of NUnit and an order of magnitude larger and more complex.

How can that be, you ask? Well, it's simpler because it's being divided into pieces and users won't need to understand everything, just the parts they use. Similarly, contributors will be able to focus on a small part of NUnit, adding their own touches, without rebuilding the whole thing.

On the other hand, it's more complex because it is moving from being a framework for writing unit tests to a testing platform, one that can work with many frameworks and many runners. The new platform is centered around a testing engine, with an API that can be used by any program needing to load and run tests. You'll be able to write your own runner, in any suitable language, and get test execution functionality from the NUnit test engine. Want to write it in Ruby? No problem. Using Silverlight or Mono's Moonlight? Again, you're free to do so.

At the other end of things, you'll be able to use our new plugin architecture to enable NUnit to run whatever tests you like. Plugins will be available for common alternative frameworks and to add special functionality like parallel execution to the standard NUnit tests.

I'll post more about what's coming in the near future. Meanwhile, for a more detailed overview, read the [NUnit 3.0 Vision](files/nunit-30-vision.pdf) document.

You can expect early (alpha ) releases in the first part of next year.


---

### Comments

---

[...] Imag&iacute;nense escribir una &quot;test fixture&quot; en NUnit y correrlo en un &quot;test runner&quot; hecho en Ruby. Imag&iacute;nense una Test Framework dise&ntilde;ada para ser una plataforma de testing y no una para escribir test. Imag&iacute;nense el incorporar varias plataformas de test en un solo core. Bueno, eso es lo que Charlie Poole uno de los creadores de NUnit dice del futuro de NUnit 3.0. [...]
>NUnit 3.0, el futuro, Sunday, March 30, 2008

---

[...] de test en un solo core. Bueno, eso es lo que Charlie Poole uno de los creadores de NUnit dice del futuro de NUnit 3.0.    Be the first to rate this postCurrently 0/5 [...]
>NUnit 3.0, el futuro, Tuesday, April 8, 2008

---

[...] Time for a new NUnit - The NUnit guys lay out their plans for version 3.0. [...]
>Our daily link (2007-12-13) - Trumpi&#39;s blog, Thursday, December 13, 2007

---

[...] Here is the posting, give it a read - here [...]
>NUnit 3.0, comments by Charlie Poole - Derik Whittaker, Thursday, December 13, 2007

---

[...] NUnit 3.0, el&nbsp;futuro&#8230; Guardado en: .net framework, Programming &#8212; kementeus @ 12:14 am   Imagínense escribir una &#8220;test fixture&#8221; en NUnit y correrlo en un &#8220;test runner&#8221; hecho en Ruby. Imagínense una Test Framework diseñada para ser una plataforma de testing y no una para escribir test. Imagínense el incorporar varias plataformas de test en un solo core. Bueno, eso es lo que Charlie Poole uno de los creadores de NUnit dice del futuro de NUnit 3.0. [...]
>NUnit 3.0, el futuro&#8230; &laquo; My kemenworld&#8230;, Monday, December 17, 2007
