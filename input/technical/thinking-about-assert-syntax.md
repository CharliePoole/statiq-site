Title: "Thinking About Assert Syntax"
Published: 31 Aug 2006
Tags: [NUnit V2,TDD]
---
I've been using the development of NUnitLite (coming soon!) as an excuse to try out alternatives to the standard NUnit syntax for Asserts.

NUnit allows us to write things like...

```csharp
Assert.AreEqual( expected, actual );
Assert.Greater( x, 5 ); Assert.Less( x, 10 );
Assert.IsTrue( x > 5 && x < 10 );
```

The last is instructive: if it fails, the message simply tells us that True was expected but the value was actually false. Any more intelligent message must be supplied as an argument...

```csharp
Assert.IsTrue( x > 5 && x < 10, "Value of x must be < 5 and > 10" );
```

An alternative idea, borrowed from NMock, is to encapsulate the comparison in some sort of an object. Then you can write things like...

```csharp
Assert.That( actual, new EqualsMatcher( expected ) );
Assert.That( x, new AndMatcher(
   new (GreaterThanMatcher( 5 ), new LessThanMatcher( 10 ) ) );
```

Of course, these aren't too easy to type, but with the addition of some helper methods and the use of operator overloading, we can replace it with

```csharp
Assert.That( actual, Is.EqualTo( expected ) );
Assert.That( x, Is.GreaterThan( 5 ) & Is.LessThan( 10 ) );
```

That pretty much covers what I've implemented so far. I'm not fully satisfied with the expressiveness of this syntax, though. It's clear but a bit wordy. I'm thinking of going back to a set of inherited methods to either replace or supplement the methods of the Is class. That might give me something like this...

```csharp
Assert.That( actual, eq( expected ) );
Assert.That( x, gt( 5 ) & lt( 10 ) );
```

So what do folks think? Is it worth pursuing this syntactic approach? Or should we just go back to Assert.AreEqual and friends?

Charlie

---

### Comments

---

My personal feelings are that the English-like expressions make the implementation too obfuscated for all but gurus to read. One of the goals of open source projects should, imho, be to create readable, extensible code. I've looked at plenty of the NUnit code to figure out how to best do things, particularly in the area of testing things. It's nice code. I would imagine that the implementation of these expressions would be a fairly nasty bit of code with operator overloading and such that is generally considered to be obfuscating.

Of course, there's always the possibility that I'm just a cave man, and that anything that doesn't remind me of K&amp;R C makes me a little nervous.

Just my 2c.

-Kelly
>Kelly, Wednesday, September 6, 2006

---

Kelly,
I've resisted that sort of syntax before, but enough people are interested in it that I'm giving it a try to see how I like it.
Charlie
>Charlie, Wednesday, September 6, 2006

---

Sure thing Charlie... some people like Opera too... :-)

Diversity is good, for the most part.

-Kelly
>Kelly, Thursday, September 7, 2006

---

Charlie,

I like some of the new syntax, particularly the Is.XXX() and the operator overloading stuff. It's a good combination of readable but not too terse. However, one thing to consider is that under that context, the initial Assert.That() seems somewhat redundant (i.e. it reads pretty, but looks like something I'd have to be writing all the time). In particular , there needs to be a way to continue using what works (or something just as terse) for simple asserts.

Of course, you could end up playing some pretty funky word games like
I.Assert(value).IsGreaterThan(5);

Something that allows you to chain assertions instead of using operators to string them together is something I could see myself getting used to, as well (like the Rhino.Mocks Expect and LastCall stuff does).
>Tomas Restrepo, Wednesday, August 30, 2006

---

Sorry, it seems WordPress has a small bug in the comment code and doesnt like "less than"-signs.

I think, that "Assert.IsTrue( ... )" is much easier to read than "Assert.That( x, gt( 5 ) &amp; lt( 10 ) )" . I just would split it into two asserts or create an Assert.IsInRange() or something like that. Trying to make code look like a natural language doesn't make it easier to write better unit tests - at least IMHO. That's what I don't like in NSpec as well.  What I would like to see, is a complete new meta language for specifications - whith it's own parser and execution engine.
>Tobias, Thursday, August 31, 2006

---

Maruis,

> To be honest, I don't know if people like the dot syntax. I haven't 
> really launched the framework. I want to do a bit more work on 
> integrating it into several test runners and completing the 
> documentation.

Well, everyone who is in favor of it seems to be implementing some framework or extension that uses it. :-) 

A number of folks find it hard to understand. 

My take is that it's easy to understand as words, but not as object oriented code. The nmock syntax always makes me stop and do a double-take - I keep wondering what the type of every stage is.
 
> The framework runs in NUnit by creating the necessary plumbing in 
> NUnit.Core (NSpecifyTestCaseBuilder, NSpecifyTestFixture and 
> NSpecifyTestFixtureBuilder). The newer 2.4 alpha version integration 
> works differently, but I’ve done basically the same to get the 
> framework working. It would be great if we can include the integration 
> files for NSpecify into the NUnit code base.

But if you're providing add-ins, you shouldn't need to change NUnit at all - that's the idea.

OTOH, I'm not clear why you even need an addin - although that may be because I don't know the details of NSpecify. If it's only a syntactic layer, shouldn't you be able to just distribute an assembly that the tests reference? What does an NSpecifyTestFixture do differently?
 
> Out of interest, what do you require for someone to become a developer 
> on the NUnit project? Because I would be interested in joining the 
> project.

Best thing is to join the developer list and get involved in discussions. Then volunteer to do some deadly boring task. :-)

Seriously, we're open to developers who want to work with us.
You have to be willing to accept the parameters of the project, shown in the Vision and Roadmap docs. I mention that because NUnit puts a lot of emphasis on not breaking things for existing users, at least within a major release. Many people have gotten frustrated because we won't put breaking changes into NUnit 2.4. However, NUnit 3.0 is just around the corner.

> I have a small problem with the syntax you proposed. The syntax won’t 
> be very discoverable. People new to the library will find it hard to 
> figure the syntax out, without reading the documentation on how to use 
> the library. Intellisense won’t be help either.
> 
> Expect(one, EqualTo(one));

True. And that's a plus for the dot syntax that I hadn't thought about. Maybe I should publish the 0.1 release of NUNitLite with three competing syntaxes - although I might end up stuck with all of them if I do that. :-(

> C# 3.0 will make it interesting. Then we can do something like this:
> 
> [Test]
> public void ValidateCustomer()
> {
>     Customer cust = new Customer();
>     cust.Firstname.Must.Equal( “Maruis” );
>     // Or
>     cust.Firstname.Should.Equal( “Maruis” ); }
> 
> So when the customer object is within the context of a test fixture, 
> the object will then have test extension methods on the object.

Sure, I've done something like that in Ruby. However, it's a problem for NUnit that this requires C# 3.0 and won't work in other languages - not to mention earlier frameworks. That's one of the reasons I'm particularly interested in libraries that can be referenced by the tests and just work, without modifying NUnit.

Charlie
>Charlie, Tuesday, September 5, 2006

---

Hi Joakim,

Thanks for the comments. 

Since some people prefer a terser syntax, I've been experimenting with keeping both the Assert.That and Is.xxx syntax and an inheritance-based
syntax that allows things like...
  Expect( 2, LessThan(3) & GreaterThan(1))
It does create some code duplication to do that, however.


Charlie
>Charlie, Tuesday, September 5, 2006

---

Charlie, 

To be honest, I don't know if people like the dot syntax. I haven't really launched the framework. I want to do a bit more work on integrating it into several test runners and completing the documentation.

The framework runs in NUnit by creating the necessary plumbing in NUnit.Core (NSpecifyTestCaseBuilder, NSpecifyTestFixture and NSpecifyTestFixtureBuilder). The newer 2.4 alpha version integration works differently, but I’ve done basically the same to get the framework working. It would be great if we can include the integration files for NSpecify into the NUnit code base. 

Out of interest, what do you require for someone to become a developer on the NUnit project? Because I would be interested in joining the project.

I have a small problem with the syntax you proposed. The syntax won’t be very discoverable. People new to the library will find it hard to figure the syntax out, without reading the documentation on how to use the library. Intellisense won’t be help either.

Expect(one, EqualTo(one));

C# 3.0 will make it interesting. Then we can do something like this:

```csharp
[Test]
public void ValidateCustomer()
{
    Customer cust = new Customer();
    cust.Firstname.Must.Equal( “Maruis” );
    // Or
    cust.Firstname.Should.Equal( “Maruis” );
}
```

So when the customer object is within the context of a test fixture, the object will then have test extension methods on the object. 

I’ve done a spike using the CTP of C# 3.0 and proved it was possible. But I still need to investigate the possibility more.

Maruis
>Maruis, Tuesday, September 5, 2006

---

Thomas:
I thought about Lesser vs Less. We say "a is greater than b" vs "a is less than b" but we also say "a is the greater of the two" and "a is the lesser of the wo" So it depends on what sentence you're imagining. Maybe it should be GreaterThan and LessThan, which also makes the order of arguments clearer.

We've already had a bunch of discussion with the existing NUnit syntax for Assert.Greater and Assert.Less. For some people, the notion of expected vs actual gets blurred with these assertions. We ended up changing the argument names to arg1 and arg2 for that reason.

Was that a typo - did you mean "vote no to lt() and gt()?" Several people have said that. It seems like it works syntactically, but not culturally. I suppose I'm ruby-infected.

I also feel that & and | are a bit awkward. The && operator gives an error unless you have defined operator true and operator false as well. I tried doing that but ran into this problem: I want evaluation to be shortcut when the match is performed, but not when the objects are constructed. I can define them so that either && or || works, but not both!!! I considered defining them both to throw, but I think a compiler error is better.

I'm actually getting the chaining syntax ( .and. ) to work, so I may put that out as well. Of course, InRange is easy enough to do, but the longer approach still makes a good example for combining assertions.
>Charlie, Thursday, August 31, 2006

---

The problem with 
  assert( x = y) {
    print x;
    print y;
    assert( x 
>JohnCarter, Thursday, August 31, 2006

---

John: Sorry, This business with the &lt; symbol is getting annoying. I'll check to see if there's a bug fix. BTW, I actually typed &amp;lt; in the preceding sentence.

Update: If you type x < y, you're OK.

But if you type x &lt;y (no space) it looks like this...

x <y


Charlie
>Charlie, Thursday, August 31, 2006

---

All:
Yes, I'm afraid I tend to overvalue terseness because I tend to just keep typing, even after the Intellisense shows up. I'm trying to train myself. :-)

So, maybe shortness of expressions is not such a big issue. I'll think about that.
>Charlie, Thursday, August 31, 2006

---

When developing rMock we thre in an assertThat(, ). A few comments and experiences:

* we opted for an "is" instance in the testcase as an expression factory. I think this was a good idea, a static class would probably do better in nUnit though. Still, I think Is. is a good idea and I would do it again.

* we opted for the shorter lt, eq, gt expressions, I think that was a mistake and I think that we will overload them with lessThan, greaterThan and equals in coming versions of the framework.

* we used an assertThat method, I think that your Assert.That syntax is better.

* I would have liked to have the operator overloading capabilities in java, now we chain expressions like assertThat(2, is.lt(3).and(is.gt(1)). It would be cool to have assertThat(2, is.lt(3).and.gt(1)) but the implementation becomes much more magical.

To sum it up, I think that the assertThat syntax reads atleast as easy as Assert.IsEqual(...) and that the extensibility and clearer error messages make all the difference. It also eliminates the "what should come first" problem. Should I have the expected or the actual value first?
I would like tho see an assertThat syntax in NUnit, in fact I missed it when I coded some C# recently.

Just my 2 cents.
>Joakim Ohlrogge, Tuesday, September 5, 2006

---

Charlie,

Personally I've found that the language you use when test-driving your code is very important. I really like the syntax you end with in the above example. The assumptions read like English sentences, which I feel is very important if your regression test suite is going to be used as documentation. I've also found that people new to TDD tend to understand TDD better when using a descriptive syntax like the ones I'm proposing. 

I agree that the word 'Specify' is not that important. 'Assert.That()' would be just as good. But I think ‘Expect’ might be a better verb to use.

Expect.That( one ).Must.Equal( one );

&gt;&gt; The existing Assert.Fail causes a test to fail. Are you intending Specify.ToFail to do that? It reads more like a specification: I expect 
&gt;&gt; something to fail. If it’s simply throwing an assertion error, I don’t think that Specify works as a verb in this particular case.

Yes, that was what I meant. But you are right; the verb does not work in this context.

&gt;&gt; One small thing: I understand that Ignoring a test and marking it as Inconclusive are theoreticaly two different things, but I have never 
&gt;&gt; seen anyone make the distinction in practice. So… is this simply a theoretical idea looking for a problem, or is there a problem to be solved 
&gt;&gt; here?

Inconclusive is more like Fail. In MSTest a new test method gets generated with an Assert.Inconclusive () which throws an exception and fail the test. Ignore causes the test to be ignored. To be honest, Inconclusive don’t make to much sense for me now that I think about it. 

I’ve completed a whole library using the above syntax called NSpecify (http://nspecify.sourceforge.net). I’ve also created some NUnit integration for NSpecify. 

Maruis
>Maruis, Monday, September 4, 2006

---

Maruis: 

Thanks for the extended example. I'll focus on a few points for discussion.

Regarding the use of 'Specify' - I understand the reasoning and agree with it. I've been touting tests as specifications for a number of years. I guess I'm a little tired of hearing from some people who seem to believe that I must not understand that tests are specifications, since I dont' use the word! In the end it's a matter of style. Personally, I prefer Expect, but so far I have stuck with Assert. IMO, this is trivial. Anyone who likes Specify can do...

public class Specify : Assert { }

The use of chained properties and methods with '.' is another matter. I originally started out with the idea of having a syntax that worked this way. I have a prototype that works - which was a bit tricky to create, btw. However, I've had lots of comments from people who say that they don't like this notation. My sense is that it reads easily as English, but doesn't really help programmers that much, as compared to a more standard Syntax. Many of the properties are just empty connectors, returning this. And it seems not very C#-ish as a matter of style. However, if I thought there were an audience for it, I might put it back in as an alternative.

The existing Assert.Fail **causes** a test to fail. Are you intending Specify.ToFail to do that? It reads more like a specification: I expect something to fail. If it's simply throwing an assertion error, I don't think that Specify works as a verb in this particular case.

One small thing: I understand that Ignoring a test and marking it as Inconclusive are theoreticaly two different things, but I have never seen anyone make the distinction in practice. So... is this simply a theoretical idea looking for a problem, or is there a problem to be solved here?

Charlie
>Charlie, Sunday, September 3, 2006

---

What about the following Syntax:

public void ObjectSpecificationSyntax()
{
   object actual = new object();
   Type type = typeof( string );

   Specify.That( actual ).Must.Equal( actual );
   Specify.That( actual ).Must.Be.AssignableFrom( type );
   Specify.That( 2 ).Must.Be.GreaterThan( 1 );
   Specify.That( 2 ).Must.Be.GreaterOrEqualThan( 1 );
   Specify.That( actual ).Must.Be.InstanceOf( type );
   Specify.That( actual ).Must.Be.LessThan( 1 );
   Specify.That( actual ).Must.Be.NaN();
   Specify.That( actual ).Must.Be.Null();
   Specify.That( actual ).Must.Be.SameAs( actual );
   Specify.That( actual ).Must.Not.Equal( actual );
   Specify.That( actual ).Must.Not.Be.AssignableFrom( type );
   Specify.That( 2 ).Must.Not.Be.GreaterThan( 1 );
   Specify.That( actual ).Must.Not.Be.InstanceOf( type );
   Specify.That( actual ).Must.Not.Be.LessThan( 1 );
   Specify.That( actual ).Must.Not.Be.NaN();
   Specify.That( actual ).Must.Not.Be.Null();
   Specify.That( actual ).Must.Not.Be.SameAs( actual );
   Specify.That( actual ).Must.Be.GreaterThan( 25 );
}

private void StringSpecificationSyntax()
{
   string str = string.Empty;
   Regex rx = new Regex( @"^-?\d+(\.\d{2})?$" );

   Specify.That( str ).Must.Equal( str );
   Specify.That( str ).Must.IgnoringCase.Be.EqualTo( str );

   Specify.That( str ).Must.Contain( str );
   Specify.That( str ).Must.EndWith( str );
   Specify.That( str ).Must.Match( rx );
   Specify.That( str ).Must.StartWith( str );
   Specify.That( str ).Must.Be.Empty();

   Specify.That( str ).Must.Not.IgnoringCase.Be.EqualTo( str );
   Specify.That( str ).Must.Not.Contain( str );
   Specify.That( str ).Must.Not.EndWith( str );
   Specify.That( str ).Must.Not.StartWith( str );
   Specify.That( str ).Must.Not.Match( rx );
   Specify.That( str ).Must.Not.Be.Empty();
}

public void AdditionalSpecificationSyntax()
{
   string message = string.Empty;
   object[] parameters = new object[] { 1 };

   Specify.ToFail();
   Specify.ToFail( message );
   Specify.ToFail( message, parameters );

   Specify.ToIgnore();
   Specify.ToIgnore( message );
   Specify.ToIgnore( message, parameters );

   Specify.Inconclusive();
   Specify.Inconclusive( message );
   Specify.Inconclusive( message, parameters );
}

Just to show some of the syntax.
>Maruis, Sunday, September 3, 2006

---

Tomas,

I've thought that too. Of course, Assert.That and Is.GreaterThan could both be replaced by a single method name each through inheritance. I may do that, but I wanted to have a basic format that would work for fixtures that didn't inherit from any sort of Asserter class. I think shorter names than I have used up to now would probably help: eq, ne, gt, lt, etc.

For the simplest of things, I overloaded Assert.That(bool) to simply check for true.

I'll have to look at how Rhino mocks does that chaining. I've imagined doing something like Assert.That(x).gt(5).and.lt(10) but I haven't actually sat down and figtured out how to make it work.
>Charlie, Wednesday, August 30, 2006

---

Do you know NSpec (http://nspec.tigris.org/). This looks pretty much the same. In fact I stumbled across NSpec yesterday and I wondered when it will be integrated in a unit testing framework.
>Jens Winter, Wednesday, August 30, 2006

---

Please don't over-abbreviate the names. I don't like typing any more than the next person, but proper words make all the difference in readability, and much of the typing load can be taken up by IntelliSense these days.
>Gavin Greig, Thursday, August 31, 2006

---

I prefere the way Thomas suggested. It looks for me the 'natural' way, it makes it easy to understand the tests done by other persons. The short names (gt, lt, eq) loose this advantage, at least for me as a non-native english speaking person. With the short names I have to think first what the abreviation means and can't fully concentrate on the test. With the Intellisense there is also not such a big trouble with longer names, you just write the first 2 chars and the IDE does the rest.
>Roland, Thursday, August 31, 2006

---

I think, that "Assert.IsTrue( x &gt; 5 &amp;&amp; x 
>Tobias, Thursday, August 31, 2006

---

I use the opportunity to express my feelings about the Assert.Greater/Less syntax:

Shouldn't it be "Lesser" when the opposite is called "Greater"?

All the other assertions are built on the template Assert.{Something}(expected, actual), but on Greater/Less it is the opposite. I have the expected-actual order hardwired in my brain, so on the few occasions I use Greater/Less, I have to stop and think. I don't like to think, it hurts :)

On the Lite syntax:

I vote no to ls() and gt(). They are so not .NET. I think they would stand out as very ugly in this environment.

I am also a little disturbed on Is.GreaterThan( 5 ) &amp; Is.LessThan( 10 ): I understand the need for the &amp; operator, but I think it is too easy to forget why this is important and use the &amp;&amp; operator instead.

I think I prefer something like this: Is.GreaterThan( 5 ).And.Is.LessThan( 10 ).
Or perhaps: Is.InRange( 5, 10 )
>Thpmas Eyde, Thursday, August 31, 2006

---

Maruis,
I'm about ready to make some naming changes. I think Expect is what I like best too. I may keep Assert as the base class though, and use Expect in the helper that you can inherit from. A lot of folks have complained about 'That' as an unnecessary bit of syntax...

Expect(one, EqualTo(one));

What about the chained dot syntax. Do your users like it? Some folks have said they find it un-C#-like in style.

Inconclusive in VSTS does the same thing as Ignore in NUnit. If it would actually do something else, I'd add it. Words do matter, as you say, but it's hard to figure out what words matter most to what people. As with Assert itself, anyone who wants to define Assert.Inconclusive can do it trivailly on top of NUnit.

One question - which  we can talk about here or offline as you like - how come NSpecify can't just run on top of NUnit without modification? Having different builds is potentially confusing both to users and to the .Net loader and I wouldn't mind making a few changes so that NSpecify could extend rather than modify NUnit.
Charlie
>Charlie, Monday, September 4, 2006
