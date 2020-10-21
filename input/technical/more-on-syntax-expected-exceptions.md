Title: "More On Syntax: Expected Exceptions"
Published: 01 Sep 2006
Tags: [NUnit,TDD,It's the Tests]
---
Sometimes you expect an exception to be thrown by a method. So, of course, you want a test for that. NUnit provides the ExpectedExceptionAttribute for that purpose. It has a bit of history...

First, it was a list of exception types. That's right, in NUnit 1.x, you could specify multiple expected exceptions. At some point, somebody thought "Wait, shouldn't you know what you're testing for?" and it was reduced in NUnit 2.0 to a single exception type.

```csharp
[Test, ExpectedException(typeof( MyException ) )]
```

Then there was the message. Folks wanted to check for a single exception, so a constructor argument was added that allowed you to specify the message.

```csharp
[Test, ExpectedException(typeof( MyException ), "My special message" )]
```

But some messages are pretty long, so you might want to specify a substring...

```csharp
[Test, ExpectedException(typeof( MyException ), "special", MessageMatch.Contains )]
```

This doesn't even get into specifying the exception by name rather than type, using regular expressions, providing a custom user message in case of failure,... but I'm sure you get the idea. The ExpectedExceptionAttribute has been overloaded with options, but people keep asking for more, like checking the value of specific properties on a specific type of exception.

Working on NUnitLite has given me the chance to look at this with fresh eyes. The heart of the problem is that there is only so much you can express between the square brackets when you use an attribute. Properties may only be of certain types and only so much fits on the line in a readable way. On the other hand, it's reasonably simple to check various properties of an exception in a catch block - or in any other code for that matter.

For NUnitLite, I'm trying a simpler approach. First of all, you can simply specify ExpectedException without any arguments...

```csharp
[Test, ExpectedException]
```

That means you expect some sort of exception - but you aren't saying what. I know that this isn't a very good testing practice by itself, but there are uses for it, as we'll soon see. Normally, you'll want to specify an exception type...

```csharp
[Test, ExpectedException( typeof(MyException) )]
```

This works slightly differently from NUnit. The test will succeed if MyException or any exception that inherits from it is thrown - NUnit requires the exact type.

Now suppose you want to verify that MyException is thrown and has an particular message and that a certain property it supports has been set to a certain value. If your test class implements the IExpectException interface, you are able to define a handler to validate the exception. Here's the interface...

```csharp
public interface IExpectException
{
        void HandleException( Exception ex );
}
```

Here's an implementation to deal with the hypothetical example

```csharp
public void HandleException( Exception ex )
{
        Assert.That( ex.Message, Is.EqualTo( "my special message" );
        MyException mex = (MyException) ex;
        Assert.That( mex.Param == "xxxxx" );
}
```

As I've experimented with this, I have found that it was rare for me to need more than one exception handler in a class. I can usually generalize one method sufficiently to make it work for all my needs. But, in some cases, there is a need for a different handler. In that case, you can specify the name of the handler method as an additional property of ExpectedException...

```csharp
[Test, ExpectedException(Handler="AlternateHandler")]
or
[Test, ExpectedException(typeof(MyException), Handler="AlternateHandler")]
```

The method must exist and have the correct signature or a run-time error will be thrown.

This is as far as I've taken it. I can think of other ways to expand (read complicate) it, but I'm not doing that. Users can write anything they want in the handler and if something turns out to be needed repeatedly we can think about removing the duplication later.

Charlie


---

### Comments

---

Hi Joakim,

So the exceptThatExceptionTrhown call primes the framework with a constraint to apply to the exception. Neat!

The handler approach I'm using allows use of the same assert expressions as in the test but doesn't give the same granularity of detecting the assert.

I'll thnk about how to fit this into NUnitLite.

Charlie
>Charlie, Tuesday, September 5, 2006

---

Hi Charlie,

Yes, that is what we are doing and that also clarifies my earlier post, of course we are not "keeping track" of exceptions; rMock catches them and matches them against the current constraint (if there is one) and decides if the exception should remain caught or be rethrown (as an error or failure depending on wether a constraint exist or not).

Glad you liked the idea!
/Joakim
>Joakim Ohlrogge, Wednesday, September 6, 2006

---

**Week in review**

Now I have access to RSS Bandit I can catch up fully with the weeks news (old news to you lot I know
>Andrew Stopford's Weblog, Monday, September 11, 2006

---

Joe: 

Well, some people have complained about the exact exception behavior and I have always said the same thing you do: You should know what exception you expect.

In this case, I changed the behavior because I was thinking that the exception handler could further restrict what is acceptable by making more assertions, but couldn't make an unacceptable exception acceptable. However, a slight change in how we handle ExpectedException without a type specified would make what you suggest possible

Right now, if you don't specify an exception, we test for System.Exception. If I change that so that we don't look at the type at all, then you can put the type-checking logic in the handler as you suggest. Good idea!

Charlie
>Charlie, Friday, September 8, 2006

---

The HandleException() idea is certainly cool. But what's the reasoning behind changing [ExpectedException(typeof(FooException))] so it also accepts descendants of FooException? Shouldn't the test document the exact behavior, including the exact exception type? I've never yet seen a use for the "or descendants" behavior; it would only be useful in really exotic cases, and doing it by default would introduce any number of subtle bugs. (And if anyone did need the or-descendants behavior, HandleException() would provide a very simple way to deal with it.)
>Joe White, Thursday, September 7, 2006

---

In rMock we handle exceptionExpectations inside the test pretty much in the same way as assertThat:

testException {

    RuntimeException expected = new RuntimeException();
    
    expectThatExceptionThrown(is.sameAs(expected));
    throw expected;
}

A few nice things with this:

1) You can use the same expressions as with assertThat to check yor exceptions (do i care about the class, some message, some custom field? the message containing a certain string? matching a regilar experession). Writing custom expressions are quite easy.

2) less obvious one, you can start expecting the exception right before it is supposed to occur so you don't pass your test by mistake because an exception is thrown to early.

To make this work we have some magic in rMock where rMock keeps track of the exceptions thrown during a test and before failing the test matches against any expectations setup with "expectThatExceptionThrown".

Maybe this input can give some more ideas?
>Joakim Ohlrogge, Tuesday, September 5, 2006

---

Sure, just as in NUnit or JUnit, you put in some assertions. Of course they have to be before the point you expect the exception to be thrown.
>Charlie, Friday, September 1, 2006

---

That, Charlie, is my "problem": There is only one method call to test, so I need my assertions after the call. But then it's too late, of course. The interface/delegate trick you mentioned will probably do the trick.
>Thomas Eyde, Saturday, September 2, 2006

---

Oops.  I should have proposed some generics magic too:

Exception caught = Assert.Throws(delegate {
    someCodeThatThrowsAnException();
});
>Nat, Monday, September 4, 2006

---

Doh! The generics disappeared.  How's this?

ExpectedException caught = Assert.Throws&lt;ExpectedException&gt;(delegate {
&nbsp;&nbsp;&nbsp;&nbsp;someCodeThatThrowsAnException();
});
>Nat, Monday, September 4, 2006

---

How about using anonymous delegates?

E.g. 

Exception caught = Assert.Throws(delegate {
    someCodeThatThrowsAnException();
});

Assert.Equals("expected message", caught.GetMessage());
>Nat, Monday, September 4, 2006

---

HI Nat,
Sorry 'bout that business with disappearing &lt;...&bt;  I've looked at the WordPress code, but it looks messy to modify, especially without tests! Maybe I'll just add a note above this box about typing code in.

Your solution is elegant and one I'd love to use. But in a product that claims to target everything from .Net 1.0 upward - even .Net CF 1.0 - it doesn't work.

OTOH, since NUnitLite is source-distributed, it could be put in as a conditionally compiled feature - I'll take a look at that. But for cross-platform use, I haven't been able to think of anything better than the excdeption handlers.

Charlie
>Charlie, Monday, September 4, 2006

---

Is there any way to specify the expected exception AND some additional assertion in the same test method?
>Thomas Eyde, Friday, September 1, 2006

---

I'm guessing that you want to verify that the method throwing the exception has left things in a proper state. In NUnit, you could only do that by inserting a try/catch block and making the tests in or after the catch. Using the handler approach in NUnitLite you would simply make those assertions in the handler. It's a simple mechanism, but you can do a lot with it.
>Charlie, Saturday, September 2, 2006

---

**Week in review**

Now I have access to RSS Bandit I can catch up fully with the weeks news (old news to you lot I know
>WPF Community Bloggers, Tuesday, September 19, 2006

---

I like the idea of the addition of custom exception handlers (in fact I use it in my own 'mess around test runner'). Some of the integration type work I do (involving crazy things such as BizTalk) can offer a large number of "exceptions" due to the huge code base a test is executing (thats 'functional' testing as opposed to 'unit' testing). Of course that said was the intension to roll this feature into NUnit also?
>Paul, Sunday, February 18, 2007

---

In fact, this is already in the NUnit source, along with a new set of assertions based on the Assert.That model. There never was a third beta, but the RC will be coming out later this week if all goes well.
>Charlie, Monday, February 19, 2007

---

Joshua: In fact, the same code is in the NUnit CVS head and will be part of the 2.4 beta 3 release. One change: I kept the historical interpretation of the expected Type - it must be the exact type to succeed.
>Charlie, Friday, December 8, 2006

---

this is like so cool (my daughter talks like this). We have a specific situation where a web service throws a SoapException but we need to check the SOAP message for the 'actual' application exception. We'll never get the application exception and cannot handle it declaratively ie. [ExpectedException(typeof (myRealApplicationException)]. So we could just expect a SoapException and unpack/evaluate in the handler. NUnit does not have these semantics unfortunately. Can you address that issue?
>Joshua Stein, Thursday, December 7, 2006
