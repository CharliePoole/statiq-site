Title: "Parameterized Tests With NUnit 2.5"
Published: 23 May 2008
Tags: [NUnit V2]
---
It has been possible to write parameterized tests for NUnit for some time, using Andreas Schlapsi's **RowTest Extension**, which recognizes the RowTest and Row attributes familiar to MbUnit users.

With the NUnit 2.5 alpha releases, NUnit continues to support that extension - and even bundles a copy of it. But NUnit 2.5 also introduces its own set of attributes for data-based testing, beginning with the **TestCaseAttribute**, which I will describe here.

Here's a simple use of this new attribute...


```csharp
[TestCase(12, 3, 4)]
[TestCase(12, 2, 6)]
[TestCase(12, 4, 3)]
[TestCase(12, 0, 0, ExpectedException = typeof(System.DivideByZeroException),
      TestName = "DivisionByZeroThrowsExceptionType")]
[TestCase(12, 0, 0, ExpectedExceptionName = "System.DivideByZeroException",
      TestName = "DivisionByZeroThrowsNamedException")]
public void IntegerDivisionWithResultPassedToTest(int n, int d, int q)
{
      Assert.AreEqual(q, n / d);
}
```

This creates five different test cases, displayed and reported separately. The two last tests show alternate ways to specify an expected exception. Note the use of the TestName property, to specify a meaningful name for each of these cases.

Since this test consists of a single Assert.AreEqual statement, we can simplify it further by modifying the test method to return a result and indicate the expected result with the result property...

```csharp
[TestCase(12, 3, Result = 4)]
[TestCase(12, 2, Result = 6)]
[TestCase(12, 4, Result = 3)]
[TestCase(12, 0, ExpectedException = typeof(System.DivideByZeroException),
      TestName = "DivisionByZeroThrowsExceptionType")]
[TestCase(12, 0, ExpectedExceptionName = "System.DivideByZeroException",
      TestName = "DivisionByZeroThrowsException")]
public int IntegerDivisionWithResultCheckedByNUnit(int n, int d)
{
      return n / d;
}
```

For more information on this feature, see the [NUnit documentation](http://docs.nunit.org/2.5/testCase.html).

---

### Comments

---

[...] The second and shorter option is to use the ScenarioTemplate attribute, which is inherited from NUnitâ€™s new TestCase attribute: [...]
>Parameterized Scenarios with NaturalSpec &raquo; Rash thoughts about .NET, C#, F# and Dynamics NAV., Saturday, February 28, 2009
