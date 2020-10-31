Title: I "Discover" Ruby
Published: 29 Jul 2006
Tags: [OSCON,Ruby,TDD]
---
This week at OSCON 2006 I finally learned some Ruby. I've been meaning to do this and learning by listening to Dave Thomas and Mike Clark seemed like it would be much more fun than simply reading a book. Turns out I was right.

<!--more-->
In the process, I discovered a cool thing about Ruby and invented a really neat way to use it. Except not really: other folks already knew about my "discovery" and my "invention" has been invented a few times before! Oh well! It was fun and I learned a lot. If you don't know Ruby, some of this may be new to you as well. If you do know it, perhaps you'll find something to smile about here.

**Adding a Method to Object**

So... Anyone who knows Ruby understands that class and method definitions are executed dynamically - that there's no "compile time" in Ruby. An interesting side effect of this, as demonstrated to us by Dave, is that you can add methods to existing classes on the fly.

Well, that got me thinking. Can I even add methods to the Object class? And specifically, what if I add methods that make objects know how to test themselves? So here is the first Ruby program I wrote:

```ruby
class Object
  def mustEqual( expected )
    if self != expected
      puts "Expected: #{expected}"
      puts "But was:  #{self}"
    end
  end
end

(2+2).mustEqual(5)
```

Sure enough, when you run this, it outputs the message

```text
Expected: 5
But was:  4
```

So we added a **mustEqual** method to Object - the root class of all objects! This turns out to be old news to those who are into Ruby, but it was pretty surprising to me and takes me way beyond my previous understanding of what it means for a language to be "dynamic." And it's way cool!

**Moving Ahead**

I'd like to add some further predicates. But to do that using this approach, I would need to keep adding methods to object. That didn't seem like a great idea, so I decided that I'd have a Testable object containing my test methods and a single new method for Object itself. I refactored the above as follows

```ruby
class Object
  def shouldBe
    return Testable.new( self )
  end
end

class Testable

  def initialize( actual )
    @actual = actual
  end

  def equalTo( expected )
    if @actual != expected
      puts "Expected: #{expected}"
      puts "But was:  #{self}"  
    end
  end

end

(2+2).shouldBe.equalTo(5)
```

This worked just like the first cut, but now the additional methods could be added to my new class rather than to Object. I immediately added a few more methods.

```ruby
def greaterThan
  if ( @actual <= val )
    puts( "Expected: a value greater than #{val}" )
    puts( "But was:  #{@actual}" )
  end
end

def lessThan
  if ( @actual >= val )
    puts( "Expected: a value less than #{val}" )
    puts( "But was:  #{@actual}" )
  end
end
```

So now I can write...

```ruby
2.14.shouldBe.greaterThan( 2 )
2.14.shouldBe.lessThan( 3 )
```

...which pass.

**For My Next Trick...**

Before moving on, let's be clear that I'm not writing a test framework here. I haven't addressed how tests might be identified and run and my error reporting is rudimentary. I'm just exploring some aspects of Ruby that intrigue me. This is a learning spike. It could be turned into a framework, but, for reasons that will be obvious shortly, it won't be.

So now, I'm wondering if I could combine some of my predicates using boolean logic. I'd like to be able to write...

```ruby
2.14.shouldBe.greaterThan(2).and.lessThan(3)
```

As a first cut, I modified **equalTo**, giving it a return value

```ruby
def equalTo( expected )
  if @actual != expected
    puts "Expected: #{expected}"
    puts "But was:  #{self}"  
  end
  return self
end
```

I changed **greaterThan** and **lessThan** in the same way and added this new method

```ruby
def and
  return self
end
```

My test case works, and fails with the appropriate message if I change the values. However, I'm suspicious. I can see that both predicates will be executed, even if the first one fails. With and, this problem of evaluation is relatively hidden, but I can expose it more clearly by adding an **or** method and a corresponding test...

```ruby
def or
  return self
end

1.shouldBe.lessThan( 2 ).or.greaterThan( 5 )
```

This should pass, but it fails. The message is

```text
Expected: a value greater than 5
But was:  1
```

Of course, it's not terribly surprising, since my **or** method is identical to the **and** method! What's needed is some sort of predicate or constraint object that delays evaluation of it's component predicates until they are needed. I've done this sort of thing before, so I'm pretty sure it's the way to go.

That's an exercise for the future. What you have here is what I wrote over lunch at OSCON, with all its warts. I haven't even changed my naming conventions from camel case to the more usual underscores. I'll be more ruby-like in future exercises. I learned even more after lunch, and later in the conference, I came across the syntax for rSpec, which has obviously done it all already - and much better. Check out <a href="http://rspec.rubyforge.org">rSpec</a> if you're interested.

So where do I go from here? I'm intrigued by Ruby, so I'll definitely be doing more of it. For this little exercise, I'll probably take it just enough further to satisfy myself that I understand where it's going. I also played with RubyCLR at the conference and wrote a little program that combines Ruby and NUnit! I'll blog about that next.

---

### Comments

---

[...] much longer will I be able to hold out? ;-) Share this post: Email it! | bookmark it! | digg it! | reddit!  Published Sunday, July 30, 2006 9:48 AM by JamieCansdale [...]
>TestDriven.NET by Jamie Cansdale : Charlie Poole @ OSCON, Sunday, July 30, 2006

---

Congratulations :-)

It's a slippery slope...
>Josh, Monday, July 31, 2006

---

[...] Charlie Poole &#8220;Discovers&#8221; Ruby Mr NUnit does some fun dynamic-languagey things with Ruby, such as adding methods to the base Object class so he can write code like: [...]
>Jon Rowett&#8217;s Workblog &raquo; Links for 01 August 2006, Wednesday, August 2, 2006
