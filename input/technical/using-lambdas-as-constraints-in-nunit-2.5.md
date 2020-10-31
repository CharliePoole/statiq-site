Title: "Using Lambdas as Constraints in NUnit 2.5"
Published: 03 May 2009
Tags: [NUnit V2]
---
Let's say you have an array of ints representing years, all of which should be leap years.

One way to test this would be to write a custom constraint, LeapYearConstraint. You
could then use it with the Matches syntax element to write your test as

```csharp
Assert.That( array, Is.All.Matches( new LeapYearConstraint() );
```

But creating a new constraint for this adhoc problem seems like a bit of overkill.
Instead, assuming you are working with C# version 3, try this:

```csharp
Assert.That( array, Is.All.Matches<int>( (x) => x%4 == 0 && x%100 != 0 || x%400 == 0 );
```

If it fails, it will give a generic message: "Expected: matching lambda expression" since NUnit is actually
built with .NET 2.0, but for a quick test it may be just the tool you need.

---

### Comments

---

**view**

It&#039;s the Tests &raquo; Blog Archive &raquo; Using Lambdas as Constraints in NUnit 2.5
>view, Monday, August 19, 2019
