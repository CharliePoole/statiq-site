Title: NUnit Extended - Beyond Assert
Published: 2 Jan 2005
Tags: [NUnitV2]
SummaryText: Developing a custom assertion.
---
> __NOTE:__ The custom assertions described in this article were developed for NUnit
> version 2.2 and and are seriously out of date. The article may still be useful if
> care is taken to adjust the code for current NUnit releases.

### Introduction

Assertions are pretty important. Everything we do in setting up a unit-test is
preparation for making an assertion. If it fails, the test fails.

The ideal test contains a single assertion. With one assertion, the
name of the test can be chosen to give all the information that might otherwise
have to be placed in a custom message. This isn't always possible, so we
often finish a test method with a series of related assertions that perform
what we view as one logical test.

Ideally, we would have a way to express that _logical assertion_
in a single statement. Putting it another way, we want each assertion to
do as much work as possible and to return as much information as possible 
when they fail.

If we would like to create our own assertions, NUnit provides the basic building
blocks in the Assert class. We can compare two objects for equality or identity,
test a boolean expression for truth or falsehood or check whether an object is null.
Building on these assertions, it's possible to develop more complex tests.

But that doesn't mean it's easy! Using these simple building blocks directly means 
that the developer must provide all the code to make the checks as well as building
a meaningful error message in case of failure.

To better understand the problem, let's look at two cases in which we might
want an extended assert method. In the first case, we'll write all the code
to make the test and produce an error message ourselves. In the second, we'll
use a new feature of NUnit that allows us to leverage its internal Assert
mechanism to make the tests we need.

### Checking the Type of an Object

Suppose we are testing a factory method that returns an object...

```c#
object GetInstance( int typeCode ) { ... }
```

Depending on the typeCode argument, objects of different classes will be returned.
We need to test that the object returned in a certain situation is of a certain
type or one derived from it. As a first cut at a solution, we might write this

```c#
Assert.IsTrue( theObject is SomeType );
```

This makes the correct check, but gives no particular error message. In some 
cases, that might be OK: for example, in this context:

```c#
[Test]
public void CheckThatReturnedObjectIsSomeType()
{
    object theObject = factory.GetObject();
    Assert.IsTrue( theObject is SomeType );
}
```

See how good practices help? The name of the method tells us exactly what
went wrong. Even so, it would be useful to know the type of the object that
was actually returned. NUnit overloads each of its Assert methods
to take a user-provided message. Since NUnit 2.2, it has been possible
to add an optional list of parameters. Taking advantage of this feature,
we can write

```c#
Assert.IsTrue( theObject is SomeType,
               "Expected oject to be type SomeType, but was {0}",
               theObject.GetType() );
```

which gives us a pretty clear message. The only drawback of this approach
is that we must write three lines of code each time we need to check the
type of an object. So we'll extract a method that can be used by all the
tests in the fixture.

```c#
void AssertObjectType( Type expectedType, object theObject )
{
    Type actualType = theObject.GetType();
    if ( !actualType.IsAssignableFrom( expectedType ) )
        Assert.Fail( 
               "Expected object to be type {0}, but was {1}",
               expectedType,
               actualType );
}
```

Now we can write

```c#
AssertObjectType( typeof( SomeType ), theObject );
```

in any test that needs to check the type of an object.

Notice that we had to change our algorithm slightly in order to use a type
passed as an argument. When we first did this, our test failed because we
got confused about which type should be assignable from the other. Obviously,
you wouldn't write an extension like this without tests, would you?

After trying this out in a single test fixture, we decided that it was
useful enough to include in our standard test utilities. We put it in
a class called `TypeAssert` in our test utility assembly. The
class looks like this

```c#
public class TypeAssert
{
    public IsType( Type expectedType, object theObject )
    {
    Type actualType = theObject.GetType();
    if ( !actualType.IsAssignableFrom( expectedType ) )
        Assert.Fail( 
               "Expected object to be type {0}, but was {1}",
               expectedType,
               actualType );
    }
    
    ...
    
    // Other methods that examine types

    private TypeAssert() { }
}
```

It's a good idea for a team to have a set of utility classes that
are commonly needed in their tests. Some of these will be more or
less generic, like our TypeAssert, while others will relate to the
application under development. Some teams like to keep those two
sets in two different libraries.

TypeAssert is usable as it stands. While we could have used some of
the extensibility techniques you'll read about next, it didn't seem
necessary for this problem. Anyway, I wanted to show you some alternative
ways of doing things.

> _You'll find a more fully worked out TypeAssert, with several additional methods, in the NUnit extensions directory, beginning with version 2.2.3._

### Case-Insensitive String Comparison

It's common to want to compare two strings for equality without regard to
case. Using the existing methods in Assert, the usual approach is to
write something like

```c#
Assert.AreEqual( "expected string", actual.ToLower() );</pre>
```

This has the benefit of simplicity and leverages NUnit's ability to create
a useful message, even for very long strings. But it has a drawback. Any error
message displayed will show the lower-case version of the actual string. If
we attempt to extract this as a method, we will have to convert the expected
string as well, increasing the chance of a confusing message.

An alternative is to display the strings ourselves:

```c#
Assert.IsTrue( "expected string" == actual.ToLower(),
               "Expected: expected string\n" but was{1}",
               actual );
```

This avoids the problem of the first approach, but costs us its advantages.
For long strings, the entire length will be displayed and we won't see
a caret marking the point where the strings diverge. With a fair bit of work,
we could create a method that provides these features. However, they already
exist in NUnit and we would prefer an approach that takes advantage of that fact.

We'll develop such an approach shortly. But first, let's take a look at what
we mean by "extending" Assert.

## Approaches to Assert Extensibility

Past approaches to extending Assert relied on inheritance from
the Assert class. Doing so allows us to use protected methods of
Assert and to access both our extensions and the original public
assertion methods through the derived class

In practice, however, this has presented a number of problems:

* The Assert class consists entirely of static methods and
  is not intended to be instantiated. Originally, its constructor
  was private for that reason. As a stop-gap measure, it was given 
  a protected constructor in an earlier release, so that individuals
  wanting to extend Assert would not have to modify NUnit in order
  to do so.

* Use of inheritance makes the derived class depend on Assert
  in its entirety. This tends to create more coupling than is 
  desirable and extensions created in this way have a tendency
  to be somewhat brittle.

* The meaning of inheritance, as applied to a class that is
  never instantiated, is somewhat vague. No polymorphism is involved,
  but the derived class is simply able to make use of the static
  methods of the base. This makes many of us uncomfortable.

* If methods are added to the base class for the benefit of
  derived classes, they too must be static. Naming problems arise
  when different derived classes need slightly different behavior.

We noticed that many of the problems described are normally dealt
with in object-oriented programming by use of polymorphism. We needed
an object. Starting with NUnit 2.2.1, we have one and the creation
of extended Asserts has become much simpler.

In the new model, each individual assertion is represented by an object.
It might have been logical to call these objects Assertions, but that name
is already used in the NUnit framework. For that reason, we call them
Asserters and the interface they must implement is `IAsserter`, defined
as follows:

```c#
public interface IAsserter
{
    void Assert();
}
```

The built-in Assert methods are now implemented by creating an asserter 
object and passing it to the `Assert.DoAssert` static method. The object
then does all the work: making the appropriate test and - in case of
failure - formatting a message and throwing an `AssertionException`.

An example may help here. The <span class="c">Assert.IsNotNull()</span> method previously
checked the argument and threw an appropriate exception if it was null.
Now it passes all the work to a <span class="c">NotNullAsserter</span>. The code looks 
like this

```c#
public void IsNotNull( object anObject, string message, params object[] args )
{
    DoAssert( new NotNullAsserter( anObject, message, args ) );
}
```

Of course, the NotNullAsserter object must now do everything that
was previously done in the static method. In this simple case, it may not
be clear what - if anything - has been gained. But by introducing
an added layer of indirection, we provide a point of extensibility
within the assertion mechanism. We can now create new asserter objects
and use that mechanism to test and report on conditions such as those
described earlier - and on others that we have not yet imagined.

### The TypeAssert Extension

Going back to our original problem set, let's take a look at the need
for asserting that an object is of a certain type.

Since we no longer wish to inherit from Assert, we'll package our
method - and possibly other type-related asserts - in a TypeAssert
class.

```c#
TypeAssert.IsType( typeof( SomeType ), theObject );
```

By the way, we developed this class test-first but in this article we're only
interested in the final result. Here it is, with an extra method thrown in.

```c#
public class TypeAssert
{
    public void IsType( Type expected, object theObject )
    {
        Assert.DoAssert( new IsTypeAsserter( expected, object ) );
    }
    private TypeAssert() { };
}
```

Yup, that's the whole thing! Of course, we now need a IsTypeAsserter class,
which will implement the IAsserter interface, defined as follows:

```c#
public interface IAsserter
{
    void Assert();
}
```

Here is a first cut at our IsTypeAsserter class

```c#
public class IsTypeAsserter : IAsserter
{
    private Type expected;
    private object theObject;
    
    public IsTypeAsserter( Type expected, object theObject )
    {
        this.expected = expected;
        this.theObject = theObject;
    }
    
    public void Assert()
    {
        if ( !theObject.GetType().IsAssignableFrom( expected ) )
            throw new AssertionException(
                "Expected Type: {0}\n      but was: {1}",
                expected, theObject.GetType() );
    }
}
```

We can do better by leveraging some of the classes provided by NUnit, but
we'll save that for the next example. You'll find a slightly different implementation
of TypeAssert in the extensions directory of NUnit 2.2.3 and later.

### The StringAssert Extension


You may have noticed that our implementation of `TypeAssert` does not allow
for a user provided message or formatting arguments, which makes it inconsistent
with the standard Assert methods. For `StringAssert` we'll do a full-blown
implementation. Here's the code...

```c#
public class StringAssert
{
    static public void AreEqualIgnoringCase( 
           string expected, string actual, string message, params object[] args )
    {
        Assert.DoAssert( new EqualIgnoringCaseAsserter( 
           expected, actual, message, args ) );
    }

    static public void AreEqualIgnoringCase( 
           string expected, string actual )
    {
        AreEqualIgnoringCase( expected, actual, string.Empty );
    }
}

private class EqualIgnoringCaseAsserter : AbstractAsserter
{
    private string expected;
    private string actual;
    
    public EqualIgnoringCaseAsserter( 
        string expected, string actual, string message, params object[] args )
            : base( message, args )
    {
        this.expected = expected;
        this.actual = actual;
    }

    public override void Assert()
    {
        if ( string.Compare( expected, actual, true ) != 0 )
        {
            AssertionFailureMessage msg = new AssertionFailureMessage( message, args );
            msg.DisplayDifferences( expected, actual, true );
            throw new AssertionException( msg.ToString() );
        }
    }
}
```

Our asserter class inherits from NUnit's `AbstractAsserter`, which
provides fields to hold the user message and parameters and can do some
basic formatting of the message. In this case, we used another NUnit class,
`AssertionFailureMessage` to provide message formatting.

By doing a comparison that ignores case, but still displaying the strings
that were actually used, we are able to create a more meaningful message.
Use of the `AssertionFailureMessage` class gives us messages that are
consistent in appearance with those displayed by the
built-in Assert methods.

NUnit provides a number of asserter classes, which you may want to use
in deriving your own extensions:

* `AbstractAsserter` is the base class for all the NUnit built-in assertions
* `ConditionAsserter`, derived from AbstractAsserter, is the base for
  the built-in condition assertions IsTrue, IsFalse, IsNull and IsNotNull.
* `ComparisonAsserter`, derived from AbstractAsserter, is the base class for
  all the built-in assertions taking expected and actual arguments.
* `EqualityAsserter`, derived from ComparisonAsserter is the common base for
  both EqualAsserter and NotEqualAsserter

Examine the source code to see how these are used.

> _NUnit version 2.2.2 and later includes a more complete implementation of StringAssert in the extensions directory. In addition to AreEqualIgnoringCase(), it includes a number of other useful methods for working with strings._

### Conclusion

Development of a set of customized assertion methods is a key to productive
test-writing for NUnit. In many cases, a private method in one of your
test fixtures will be all that is needed. In others, you may wish to
deploy one or more assertions so that they can be reused by the entire
development team.

NUnit's new Assert extensibility model allows tailoring specific assertions
for your application in a way that is consistent with the basic NUnit
assertions. These extensions can provide messages that are consistent
in appearance with those provided by NUnit and use features of the
NUnit framework to avoid duplication.

For more information, consult the source code for the versions of TypeAssert
and StringAssert that are provided with the latest versions of NUnit.
