Title: "Four Ways to Test Expected Exceptions"
Published: 02 Aug 2008
Tags: [NUnit,TDD,It's the Tests]
---
Let's say we are testing a piece of code, using arguments that should cause an exception to be thrown. We want the test to ensure that an exception was thrown, that it was the expected Type of exception and - possibly - that the properties of the exception are what they should be.

In this blog, I'll show how this might be done:

 1. Without any support from the testing framework.
 2. With a basic **ExpectedException** attribute
 3. With a handler, as provided by NUnit 2.4
 4. With a **Throws.Exception** constraint, as provided by NUnit 2.5

What I hope to show is that **Throws.Exception** gives us the level of control we have when we do it all ourselves, while providing a level of ease of use that is comparable to - if not better than - what we get from **ExpectedExceptionAttribute**.

First, we need an example. Let's say that we have a method that is supposed to throw an `ArgumentException` if the passed in argument is in an invalid state for the desired operation. Of course, this would be a pretty poor design choice in most situations, but lets just assume this is the way it has to be and that we simply need to test it.

Here's the test I might write using a try/catch block.

```csharp
[Test]
public void TestThatProperExceptionIsThrown()
{
  try
  {
    MethodUnderTest(anInvalidObject);
    Assert.Fail("Expected an exception, but none was thrown");
  }
  catch(ArgumentException ex)
  {
    Assert.AreEqual( ex.ParamName, "myParam" );
    Assert.AreEqual( ex.Message, "My message" );
  }
  catch(Exception ex)
  {
    Assert.Fail( "Expected an ArgumentException but got a "
      + ex.GetType().FullName );
  }
}
```

This is somewhat tedious to write, so programmers were happy to get an **ExpectedExceptionAttribute** to use instead. With that support, you could write

```csharp[Test,ExpectedException(typeof(ArgumentException))]
public void TestThatProperExceptionIsThrown()
{
  MethodUnderTest(anInvalidObject);
}
```

Or even

```csharp
[Test,ExpectedException(typeof(ArgumentException), ExpectedMessage="My message")]
public void TestThatProperExceptionIsThrown()
{
  MethodUnderTest(anInvalidObject);
}
```


But what about ParamName? NUnit 2.4 introduced the notion of an exception handler, allowing you to write:

```csharp
[Test,ExpectedException(typeof(ArgumentException), ExpectedMessage="My message",Handler="MyHandler)]
public void TestThatProperExceptionIsThrown()
{
  MethodUnderTest(anInvalidObject);
}

public void MyHandler(Exception ex)
{
  Assert.AreEqual("myParam", ((ArgumentException)ex).ParamName);
}
```

This gives us the functionality, but is somewhat verbose. A more serious problem is that there may be additional code in the test method, before or after the method call. This approach does not guarantee us that the exception came from a particular method.

NUnit 2.5 takes care of this with a new Assert and a corresponding Constraint. Assert.Throws was borrowed from the **xunit.net** framework, and is best for simpler cases:

```csharp
Assert.Throws<ArgumentException>( { delegate MethodUnderTest(anInvalidObject) } );
```


The corresponding constraint syntax, using **Throws.Exception** is unique to NUnit and allows us to meet our original requrements in a single statement:

```csharp
Assert.That( delegate { MethodUnderTest(anInvalidObject },
  Throws.Exception&lt;ArgumentException&gt.(),
  Has.Property("ParamName").EqualTo("myParam") &
  Has.Property("Message").EqualTo("My message") );
```

For new applications, I recommend you set aside **ExpectedException** and use either **Assert.Throws** or **Throws.Exception**. That way, what you are testing is clearly stated right in the code, for everyone to see.

---

### Comments

---

[...] Four Ways to Test Expected Exceptions - Charlie Poole looks at four different ways of testing exceptions thrown in code under test, ranging from no framework support to using the latest NUnit 2.5 functionality [...]
>Reflective Perspective - Chris Alcock &raquo; The Morning Brew #150, Sunday, August 3, 2008

---

[...] we need to put an ugly try/catch block around the code in question. Â This post describes how the different ways of capturing an exception in NUnit can be done. Â (Just a note, the last example with getting the parameters of an exception [...]
>Unit Tests, Extension Methods, and Lambda Expressions, Oh My! &laquo; Discovering Code, Wednesday, April 29, 2009

---

[...] can find more details information from http://nunit.net/blogs/?p=63  LikeBe the first to like this [...]
>Test Expected Exception with NUnit 2.5 &laquo; Hoa Chau&#8217;s weblog, Sunday, December 5, 2010
