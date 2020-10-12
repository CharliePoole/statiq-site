Title: "Code Generation in NUnit"
Published: 08 Jan 2009
Tags: [NUnit,It's  the Tests]
---
The latest code for NUnit 2.5 includes seven generated files, including the Assert class and most of the classes that allow you to write constraint expressions using the NUnit fluent syntax. Some people have asked if generating these files is worth the effort, since the code created is very simple anyway.

There are two reasons for generating this code. The first relates to the syntactic constructs. While it's relatively straight forward to create a custom constraint and various people have done so, such constraints must be used by invoking their constructors rather than by use of a simple key word. So, for example, if you have written an OddNumberConstraint that tests whether a number is odd and displays an appropriate failure message, you are still not able to write `Assert.That(num, Is.Odd)` without directly modifying NUnit.

It turns out, based on experience of several people who have tried, that the syntactic modification has a lot of places where you can go wrong. You have to modify at least three additional files, even after you have written the constraint. Using NUnit's code generation facility, you would simply add a line like this to NUnit's SyntaxElements.txt:


```text
Gen3: Is.Odd=>new OddConstraint()
```

Then, after running NUnit's code generation tool, the files Is.cs, ConstraintFactory.cs and ConstraintExpression.cs would be updated. After rebuilding NUnit - or just the framework - the statement **Assert.That(num, Is.Odd)** would compile and work correctly. If you wanted a classic assert, you could add the line

```text
Gen: Assert.IsOdd(int num)=>Assert.That(num, Is.Odd)
```

and Assert.IsOdd would become available for your use, including overloads with an error message and optional arguments.

So, one good reason for generating code is to make it easier to extend NUnit. But an even more important reason is reliability. Take the Assert class as an example. Some of the methods have as many as 24 overloads. In the past, we have seen hidden bugs that affected only one infrequently used overload. By generating the code, we can ensure that the same logic is used in each overload. This doesn't prevent errors, but it does make it likely that the error will be caught, since it will generally impact many of the overloads in the same way. What's more, the layout of the SyntaxElements file puts things that need to be updated together right next to one another, so it's much harder to forget a step.

The NUnit code generation program, GenSyntax.exe, is distributed with the NUnit source, in the tools directory.
