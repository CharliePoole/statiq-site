Title: "The Tester Tested : Fit and NUnit"
Published: 04 Oct 2006
Tags: [NUnit V2,TDD]
---
NUnit has lots of tests of its own - unit tests that is. It even has some fairly high-level tests that don't fit well into the normal unit-testing paradigm. But, up to now, the only acceptance tests were manual - a list of things I do before uploading a new release. Since they are manual, they don't get run all that often and surprises happen.

I've been meaning to do some experimenting with writing Fit tests for NUnit, and finally got some time last weekend. You can see the results of my initial experiments [here](files/fit-results.html).

For each row, the Fit fixture takes a snippet of code out of the first column, compiles it using the ICodeCompiler interface, loads the resulting assembly into an AppDomain and runs the tests. In addition to verifying that the code compiles, it checks the shape of the test tree that is produced and verifies that the correct number of tests were run, skipped or ignored and if the count of successes and failures are as expected.

This particular fixture verifies that tests written by a programmer using standard NUnit syntax will behave as expected. Of course, we need other sorts of tests, including those that test the various execution options of NUnit. This seems like a good place to start though.

Charlie

---

### Comments

---

Kishore:  NUnit is a unit-testing framework, not a static checker. See docs at http://nunit.org.
>Charlie, Thursday, October 12, 2006

---

Hello!
I am very new to this tool NUnit, Can this tool be used to check Naming conventions in ASP.NET(C#). Please reply soon.
>Kishore, Thursday, October 12, 2006

---

I'm not sure if they will be part of the standard distribution - maybe. But they will be available anyway.
>Charlie, Wednesday, October 4, 2006

---

Will the Fit tests become part of the NUnit distribution? It would be nice to have another example of Fit usage out there.
>Kelly, Wednesday, October 4, 2006
