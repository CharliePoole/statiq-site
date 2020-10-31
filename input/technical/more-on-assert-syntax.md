Title: "More on Assert Syntax"
Published: 03 Oct 2006
Tags: [NUnit V2,TDD]
---
In an [earlier post](thinking-about-assert-syntax.html), I presented some ideas about syntax for expressing assertions in tests. I was doing this as a part of the development of NUnitLite, with the idea of eventually putting some of the same concepts back into NUnit.

I received many useful comments. Some of them are still percolating in my brain, but others have already found there way into the implementation of NUnitLite. As a way of paying back, I thought I would tell you here how I ended up resolving some of the issues we discussed.

**NUnitLite Syntax by Example**

Here are some valid NUnitLite Asserts, assuming proper declaration of variables...

```csharp
  Assert.That( result, Is.EqualTo( 4 ) );
  Assert.That( result, Is.Not.EqualTo( 4 ) );
  Assert.That( result, !Is.EqualTo( 4 ) );
  Assert.That( obj, Is.Type( typeof(string) ) & Is.EqualTo( "Hello" ) );
  Assert.That( greeting, Contains.Substring( "Hello" ) );
  Assert.That( greeting, Contains.Substring( "HELLO" ).IgnoreCase );
  Assert.That( array, Is.EqualTo( new int[] { 1, 2, 3, 4 } ) );
  Assert.That( matrix, Is.EqualTo( new int[] { 1, 2, 3, 4 } ).AsCollection );
  Assert.That( collection, Contains.Item( obj ) );
  Assert.That( collection, Contains.Item( "HELLO" ).IgnoreCase );
  Assert.That( collection, Is.All.Type( string ) );
  Assert.That( collection, Is.All.Not.Null & Is.All.Type( string ) );
  Assert.That( obj, new UserConstraint(arg) );
  Assert.That( obj, XXX.UserConstraint(arg) );
```


**Roads Taken and Not Taken**

I started out trying to make the syntax more terse. However, several people pointed out that modern IDEs provide auto-completion in context, which makes the length of an expression relatively unimportant. In addition, at least one non-English speaker indicated that abbreviations like eq, gt, etc. can be confusing.

I spiked expressions like


```csharp
  Assert.That( 2+2, Is.GreaterThan(3).And.LessThan(5) );
```

While this is definitely doable, it's more complex and I don't find it very readable. That could be just me, of course. In addition, while it can be implemented as a simple 
operator precedence grammar, a simplistic implementation doesn't provide type safety. That is, you can end up compiling expressions like **This.And.And.That** or **And.That**. The current implementation won't compile such invalid sequences and Visual Studio's Intellisense only prompts for valid continuations.

The postfix operators like IgnoreCase are what I call Modifiers. They are simply getters that modify the state of the underlying object and return **this**.

The two final examples illustrate how one might extend the Syntax. UserConstraint is some user-defined test that implements the IConstraint interface. It can be used 'raw' as in the first of the examples, or hidden behind some syntactic sugar by defining a helper class similar to NUnitLite's Is class.

That's where it is so far... You can download pre-release code from <a href="http://www.codeplex.com/SourceControl/ListDownloadableCommits.aspx?ProjectName=NUnitLite">Codeplex</a>. Or watch for an 0.1 release soon.

Charlie

---

### Comments

---

I think that the (subject,predicate) thing is pretty good. I could make this work in Python too.   I think that it's probably the way to go. I do have trouble mentally remembering where to put a dot or where to munge words together.  I think something has to give.  I suppose if I were working in intellisense languages even, I wouldn't expect that
        AssertThat(x, 
would give me a list of classes derived from my predicate class that I could use in the given spot.  It surely wouldn't temper its results based on the idea that x is an integer v. string v. whatever.  So I guess there has to be some kind of rules or conventions that govern the use of CamelCase v. dotted.sentence notation.
>Tim Ottinger, Tuesday, October 3, 2006

---

Hmmm... could the AssertThat take variable-length argument list? Could it be (object, *predicates)? 

AssertThat(x, IsLessThan(4), IsGreaterThan(1), HasType(Integer))
AssertThat(y, IsType(String), Contains("Jules"))

It is a little ugly because you have multiple effects per invocation, but I think it's not so bad compared to many of the alternatives.
>Tim Ottinger, Tuesday, October 3, 2006

---

Tim: 

Well, of course this is NUnitLite, so we're talking about .NET as the (cross-)platform. With the exception of VS2005, the IDE prompts don't generally kick in till you type a dot.

I like the variable length approach. Since there are overloads that take additional arguments like message, it would have to be supplemented with explicit overaloads that allow, for example, one or two predictaes.

Charlie
>Charlie, Tuesday, October 3, 2006

---

[...] some time ago, in response to a blog on NUnit I decided to write up some python assertion syntax. More recently I had occasion to go looking [...]
>Blogging Ottinger (tim) :: Decorator-Based Assertions for Python :: January :: 2008, Thursday, January 24, 2008
