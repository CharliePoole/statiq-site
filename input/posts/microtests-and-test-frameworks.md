Title: "Microtests and Test Frameworks"
Published: 24 Jan 2018
Tags: [TDD,TC-Lite,It's the Tests]
---
I'm a big fan of microtests - both the term and the thing itself. My friend Hill coined the term quite a while back and I felt it completely solved the problem of ambiguity we agile folks were having when we talked about unit tests in front of people who understood the term in the way it was used 30 or more years ago.

I'm not going to describe what microtests are here. If you aren't familiar with the term, go watch <a href="https://www.youtube.com/watch?v=H3LOyuqhaJA" rel="noopener" target="_blank">Hill's video clip</a> about them right now. We'll talk more later.

...

Back already? Great! So... a while back I started to wonder if the NUnit test framework, which I have worked on since 2004, might be leading people to write bigger and more complex tests by having too many features. What would happen, I asked myself, if a framework only had those features that support microtests. And by the way... what features would that be?

I asked the question to a group of experienced coaches and - as you might expect - I got varying answers. Some people thought it was a good idea while others felt that having a separate tool for microtests would be a burden. There was a fair amount of agreement about what features were needed but also some disagreement on specific items.

I've decided to start out with a specification of features that will be most useful in microtests. There could be separate frameworks to support those features or you could just use a standard test framework and limit yourself to a particular subset. A framework could even support a setting that would warn you if you got out of the usual territory for microtests.

So here's my short list of features at least for now. Let me know what you think of it.

1. A full set of assertions, such as are supported by nunit or junit. Assertions designed for access to the file system or databases, however, would be excluded.

2. A full set of test-identification attributes, including those that support data-driven tests.

3. Some way to create shared setup and teardown for tests. This is controversial as some people think it's an antipattern to use separate setup methods. In the end, I decided it should be available but de-emphasized. Higher-level setup (fixture, namespace, assembly) would not be supported, however.

4. Simple reporting of test results without adding on any extra components.

5. NOTHING else, at least initially. In particular, NO way to order tests or define dependencies between them.

Tell me what you think of the list. Would you use such a framework if it existed?

---

### Comments

---

[&#8230;] Microtests and Test Frameworks &#8211; Charlie Poole [&#8230;]
>The Morning Brew - Chris Alcock &raquo; The Morning Brew #2513, Thursday, January 25, 2018
