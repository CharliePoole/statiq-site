Title: "NUnit 2.4 Assert Syntax - the Latest Developments"
Published: 12 Mar 2007
Tags: [NUnit V2,TDD]
---
NUnit 2.4 RC2 is out now, correcting a naming conflict with several mock object frameworks that was present in RC1. You can download it at [NUnit.org](http://nunit.org/). For a full list of the extensive new features in NUnit 2.4, check out the [Release Notes](https://docs.nunit.org/2.4/releaseNotes.html).

One of those new features is the new **constraint-based** design for assertions, supported by a new syntax. That syntax, in fact, was the cause of the aforementioned naming conflict, which is now resolved. As a result, RC2 makes more changes than would normally be seen in a release candidate.

I've already blogged quite a bit about the new syntax. I originally developed it for NUnitLite, based on design concepts in NMock2. Since there were various experiments and numerous alternatives have been discussed, I'll describe the whole thing here from scratch, as it is being implemented in NUnit 2.4.

**Design Concepts**

The "new syntax" is really a second order effect. What this change is really all about is encapsulating individual assertions as objects. We call these objects "constraints" in NUnit, and they encapsulate both a test and the generation of a message.

Readers who have been following the development of NUnit over the past few years will recognize that this is not the first such encapsulation. Back in 2004, we replaced the original procedural implementation of assertions with an object-oriented approach centered around "asserter" objects. Asserters encapsulated everything about an assertion: the test to be made, any test parameters, a message and the actual value being tested.

By eliminating the actual value from the encapsulation, and providing it separately in a method call, we are able to form compound constraints and then apply them to a single actual value. This is the essence of the design, which was arrived at through a series of spikes using various encapsulations.

In addition, the new design provides a MessageWriter, which is used internally by constraints. Most users will not need to use this interface, but those creating their own constraints will want to be familiar with it. There is a clear division of responsibility between the constraint and the writer. A constraint is responsibile for defining message content but the writer is responsible for the appearance of the message. NUnit 2.4 is delivered with one writer, TextMessageWriter, but additional writers supporting formats like html will be available in a future release.

**Two Models**

NUnit 2.4 supports both the old model of static assert methods and the new syntax. The documentation refers to the older syntax as the "classic model" and the new one as the "constraint-based" model. The classic model has now re-implemented using constraints, so the same underlying code is used no matter which syntax you use.

Some users have expressed concern that the older syntax will eventually fade away. There's no plan to do that, and I don't expect it to happen for a long time, if at all. The older syntax maps directly to other frameworks. For example, CollectionAssert uses the same methods and arguments as the class of that name in the Microsoft test framework. Even though the new syntax provides added power, many users will undoubtedly prefer the compatibility that the classic model gives them.

**Constraint Objects**

The constraint-based model uses the Assert.That method, with an actual value being tested as it's first argument and a constraint object as the second. The most direct, although not necessarily most convenient, way to express a constraint is to construct the object in-line. The following example uses an instance of the EqualConstraint to perform a test...

```csharp
Assert.That( 2 + 2, new EqualConstraint( 4 ) );
```

The same assertion can be written using a helper class to construct the constraint...

```csharp
Assert.That( 2 + 2, Is.EqualTo( 4 ) );
```

NUnit 2.4 Supports a wide range of constraints and syntax helpers:

| Syntax Helper | Constraint Constructor |
| ------------- | ---------------------- |
| Is.Null | EqualConstraint( null ) |
| Is.True | EqualConstraint( true ) |
| Is.False | EqualConstraint( false) |
| Is.NaN | EqualConstraint( double.NaN ) |
| Is.Empty | EmptyConstraint() |
| Is.Unique | UniqueItemsConstraint() |
| Is.EqualTo( object expected ) | EqualConstraint( object expected ) |
| Is.SameAs( object expected ) | SameAsConstraint( object expected ) |
| Is.GreaterThan( IComparable expected ) | GreaterThanConstraint( IComparable expected ) |
| Is.GreaterThanOrEqualTo( IComparable expected ) | GreaterThanOrEqualConstraint( IComparable expected ) |
| Is.AtLeast( IComparable expected ) | GreaterThanOrEqualConstraint( IComparable expected ) |
| Is.LessThan( IComparable expected ) | LessThanConstraint( IComparable expected ) |
| Is.LessThanOrEqualTo( IComparable expected ) | LessThanOrEqualConstraint( IComparable expected ) |
| Is.AtMost( IComparable expected ) | LessThanOrEqualConstraint( IComparable expected ) |
| Is.TypeOf( Type expected ) | ExactTypeConstraint( Type expected ) |
| Is.InstanceOfType( Type expected ) | InstanceOfTypeConstraint( Type expected ) |
| Is.AssignableFrom( Type expected ) | AssignableFromConstraint( Type expected )  |
| Is.SubsetOf( ICollection expected ) | CollectionSubsetConstraint( ICollection expected ) |
| Is.EquivalentTo( ICollection expected ) | CollectionEquivalentTo( ICollection expected ) |
| List.Contains( object expected ) | CollectionContainsConstraint( object expected )</td></tar>
| Text.Contains( string expected ) | SubstringConstraint( string expected ) |
| Text.StartsWith( string expected ) | StartsWithConstraint( string  expected) |
| Text.EndsWith( string expected ) | EndsWithConstraint( string expected ) |
| Text.Matches( string pattern ) | RegexConstraint( string pattern ) |
| Has.Property( string name, object expected ) | PropertyConstraint( string name, object expected ) |
| Has.Length( int length ) | PropertyConstraint( "Length", length ) |
| Is.Not.Xxxx, Has.Not.Xxxx, etc. | NotConstraint( Xxxx ) |
| operator ! | NotConstraint( Xxxx ) |
| Is.All.Xxxx, Has.All.Xxxx, etc. | AllItemsConstraint( Xxxx ) |
| operator & | AndConstraint( Constraint left, Constraint right ) |
| operator \| | OrConstraint( Constraint left, Constraint right ) |

**Note** that `Not` and `All` are used as prefixes to any of the other constraints. The `AllItemsConstraint` causes the following constraint to be applied to each item in a collection, succeeding only if the constraint succeeds on every item.

Examples of use:

```csharp
Assert.That( new object[] { 1, 3, 5 }, Is.SubsetOf( new object[] { 5, 4, 3, 2, 1 } );
Assert.That( new string[] { "abc", "bac", "cab" }, Has.All.Length( 3 ) );
Assert.That( 2.0d + 2.0d, Is.EqualTo( 4.0d ).Within( .000005d ) );
Assert.That( "Hello World!", Text.StartsWith( "HELLO" ).IgnoreCase );
```

The last two examples illustrate the use of **constraint modifiers**. These are properties or methods of constraints, which are used to modify their operation. They are syntactically valid on all constraints and are ignored by those constraints that do not make use of them. The following constraint modifiers are supported.

* **AsCollection** is recognized by EqualConstraint and causes arrays to be compared using the underlying collections, without regard to their respective ranks or dimension lengths.
* **IgnoreCase** causes any string comparisons to be case insensitive. It is recognized by EqualConstraint as well as by all the Text constraints.
* **Within(  tolerance )** is recognized by EqualConstraint when both values are a floating point type. It causes the comparison to succeed when the difference between the values is less than or equal to the tolerance.

**To Inherit or Not**

Beginning with version 2.0, NUnit eliminated the need to use inheritance to identify test classes. That policy continues with NUnit 2.4. Test fixtures are identified by use of the TestFixture attribute and users may develop whatever hierarchy of test classes they need. In fact, most large projects develop a set of base classes for use in defining their test fixtures.

With NUnit 2.4, we are introducing a class that is intended to be used as a base for individual fixtures or for entire project test hierarchies. Our choice of a name for this class, AssertionHelper, indicates both what it is and what it is not. It provides a number of methods that are useful in making assertions. It does *not* serve to identify a class as a test fixture in and of itself.

AssertionHelper provides an Expect method, which is identical to the Assert.That method, as well as equivalent methods to those supported by the syntax helpers.
Consequently, assuming the containing class inherits from AssertionHelper, the following assertions are equivalent:


```csharp
   Assert.AreEqual( 1.5, myObject.calculate(), .00005 );
   Assert.That( myObject.calculate(), Is.EqualTo( 1.5 ).Within( .00005 ) );
   Expect( myObject.calculate(), EqualTo( 1.5 ).Within( .00005 ) );
```


**What Next?**

The constraint-based design will continue to be expanded in the final release of NUnit 2.4 and beyond. Where the syntax will go is another question. The underlying constraint model is flexible enough to support a variety of syntactic layers, and the community is just getting started at trying out these ideas.

We expect that many users will want to develop their own constraints and to layer alternate syntax on top of the constraint model. The best of these new ideas will eventually be incorporated either as extensions to NUnit or as part of the framework itself.

So try out the release candidate and then try your hand at writing constraints of your own. See the NUnit documentation or source for a description of the interfaces.

---

### Comments

---

[...] Check out Charlie Poole&#8217;s blog or the release notes for more information. [...]
>The Disco Blog &raquo; Blog Archive &raquo; Practicing constraint with NUnit, Monday, March 12, 2007

---

[...] NUnit 2.4 Assert Syntax - the Latest Developments [...]
>TestDriven.NET by Jamie Cansdale : TestDriven.NET 2.4 Beta + NUnit 2.4 RC2, Monday, March 12, 2007

---

Hi Charlie,

I like the contraints syntax very much! It has made me think to bring this concept to WatiN as well. Keep up the good work!

Jeroen
>Jeroen van Menen, Monday, March 12, 2007

---

[...] NUnit 2.4 Assert Syntax - the Latest Developments- NUnit 2.4 has some interesting new assertions&#8211; check it out, man! [...]
>The Disco Blog &raquo; Blog Archive &raquo; The weekly bag&#8211; March 16, Friday, March 16, 2007

---

[...] is my set of add-ons to DUnit to make assertions more readable, in the spirit of NUnit&#8217;s Assert.That(...) assertions (originally from NUnitLite). For [...]
>Joe White&#8217;s Blog &raquo; Blog Archive &raquo; DUnitLite 0.4: bugfixes and Should.ReferTo, Monday, March 24, 2008

---

[...] Basic.Net and NUnit Gotcha   I like the NUnit 2.4 Constraint based syntax. So In my new role I am having to implement Unit Testing so I decided on going with NUnit to begin [...]
>Visual Basic.Net and NUnit Gotcha - Kevin Isom, Wednesday, February 27, 2008

---

[...] always forget these syntax handler shortcuts, and end up referring to a&nbsp;post from Charlie about NUnit 2.4 RC2, or using Reflector. So here is a summary for my future reference (so I can bug myself instead of [...]
>NUnit 2.4+ constraint-based assertion syntax - dave^2=-1, Friday, February 1, 2008
