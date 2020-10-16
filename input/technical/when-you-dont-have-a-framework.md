Title: "When You Don't Have a Framework"
Published: 08 Nov 2005
Tags: [NUnit,TDD,It's the Tests]
---
We forget sometimes how easy it is to write tests - even without a framework. I was sitting in a cafe, working on my new laptop. It's an Acer Ferrari 4000 that I'm very happy with, but I haven't completely set it up yet.

<!--more-->
Using the cafe's wireless connection, I downloaded and tried to install NUnit 2.2.2, running into a typical NUnit user problem: NUnit 2.2.2 won't install on a system that has .NET 2.0.50215 as its only CLR version. This is an understandable outcome of the fact that Visual Studio requires exact specification of framework versions in creating the install, but its still pretty annoying, since I know the version will run quite well using .NET 2.0.50215.

In any case, without my source code for NUnit on the laptop, I didn't have any other options, so I just started writing tests. By the way, the exercise in question is based on a question Bill Wake asked on the TestDrivenDevelopment list, but my focus here is on the "test framework." I'll blog about magnets later.

Anyway, after two tests and some refactoring, here's what I had...

```csharp
namespace PoetryMagnets
{
    public class MagnetTests
    { 
        static void Main()
        {
            TestNoMagnets();
            TestOneMagnet();
        }

        private static void AssertEqual(string expected, string actual)
        {
            if (expected != actual)
            {
                Console.WriteLine("\tExpected: < {0}>", expected);
                Console.WriteLine("\tActual:   < {0}>", actual);
            }
        }

        static void TestNoMagnets()
        {
            MagnetSpace magnets = new MagnetSpace();
            AssertEqual("", magnets.Text);
        }

        static void TestOneMagnet()
        {
            MagnetSpace magnets = new MagnetSpace();
            magnets.Add("Hello");
            AssertEqual("Hello", magnets.Text);
        }
    }
}
```

It ain't much, but it works! Further tests led me to the need to do some initial setup - I used a static for that - and an easy way to automatically run the tests. I made them all start with Test and found them by reflection.

Here's the final version I was using when I finished my coffee. It's interesting that the rudiments of a testing framework are already there, although it would take more work to make it convenient to use for more than just this project.

```csharp
namespace PoetryMagnets
{
    public class MagnetTests
    {
        private static MagnetSpace magnets;
        private static Magnet magHello = new Magnet(10, "Hello");
        private static Magnet magWorld = new Magnet(20, "World");

        public static void Main()
        {
            Type myType = typeof(MagnetTests);
            ConstructorInfo ctor = myType.GetConstructor(Type.EmptyTypes);

            foreach (MethodInfo method in myType.GetMethods())
            {
                if (method.Name.StartsWith("Test"))
                {
                    Console.WriteLine(method.Name);
                    magnets = new MagnetSpace();
                    try
                    {
                        MagnetTests tests = (MagnetTests)ctor.Invoke(new object[0]);
                        method.Invoke(tests, new object[0]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private static void AssertEqual(string expected, string actual)
        {
            if (expected != actual)
            {
                Console.WriteLine("\tExpected: < {0}>", expected);
                Console.WriteLine("\tActual:   < {0}>", actual);
            }
        }

        public void TestNoMagnets()
        {
            AssertEqual("", magnets.Text);
        }

        public void TestOneMagnet()
        {
            magnets.Add(magHello);
            AssertEqual("Hello", magnets.Text);
        }

        public void TestTwoMagnets()
        {
            magnets.Add(magHello, magWorld);
            AssertEqual("Hello World", magnets.Text);
        }

        public void TestTwoMagnetsInWrongOrder()
        {
            magnets.Add(magWorld, magHello);
            AssertEqual("Hello World", magnets.Text);
        }
    }
}
```

As frameworks become more complex, it's easy to forget how simple it can be to just start writing tests, letting the framework grow around them. The logic of my little example isn't that much different from what's at the core of NUnit itself, but it's right there to see. So if you find yourself without an installation of NUnit - or even if you're working on a platform for which there is no test framework - just start writing tests!

---

### Comments

---

Charlie Poole has an interesting post about writing tests without a pre-built framework


>the blog of michael eaton, Wednesday, November 9, 2005
