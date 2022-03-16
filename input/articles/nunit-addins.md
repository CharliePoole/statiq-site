Title: NUnit Extended - Writing Addins
Published: 28 Oct 2006
Tags: [NUnitV2]
SummaryText: Development of a Custom Attribute for NUnit 2.4
---
> __NOTE:__ The custom attribute in this article was developed for NUnit version 2.4
> and is seriously out of date. The article may still be useful if
> care is taken to adjust the code for current NUnit releases. The MaxTime
> example attribute itself is actually implemented in later releases.

### The MaxTime Attribute

As a first addin, we will create an attribute that specifies the maximum time
a test case should take to complete the case. We'll write it like this:

```c#
[Test, MaxTime(20)]
public void SomeTest()
{
    ...
}
```

If the maximum time is exceeded, the test should be marked as a failure with
a special message. However, if the test fails for some other reason, or is
ignored, the original message should be retained.

The syntax above suggests the use of a TestDecorator - an addin that modifies
the behavior of a test after it has been constructed. It would also be possible
to use a TestCaseBuilder here but that would require duplicating a lot of the
logic already present in the test case.

### Getting Recognized

As a first step, let's get NUnit to recognize and load our addin. We'll be
able to see the addin listed by NUnit, but it won't do anything. This code
to do this is quite simple and is identical for all types of addins.

If you are working in Visual Studio, create a new library project. Add a
reference to the nunit.core.interfaces assembly in your installed copy of
the NUnit 2.4 Beta 2. It's best to avoid making extra copies of this assembly,
so set Copy Local to false for this reason.

Add a class to the assembly containing this code...

```c#
using NUnit.Core.Extensibility;

[NUnitAddin(Description="Causes a test to fail if takes longer than a specified maximum time")]
public class MaxTimeDecorator : IAddin
{
   public bool Install( IExtensionHost host )
   {
      return false;
   }
}
```

If you are working from the command line, create the above with any editor
of your choosing. Compile it using the command

```cmd
csc /target:library /r:nunit.core.extensibility.dll MaxTimeDecorator.cs
```

Copy the dll you just created into the `bin\addins` subdirectory of your NUnit
installation, creating the addins directory if necessary.

Start NUnit and display the list of loaded Addins using the Tools | Addins
menu item. Two addins should appear: the RepeatedTestDecorator addin, which
comes with NUnit, and the addin you just created. If you select MaxTimeDecorator
in the list, the description from the attribute is displayed.

<div style="border: 1px solid black; background-color: #eee; margin: 0 5% 1em 5%; padding: 0 1em">
   If you're an experienced practitioner of Test-Driven Development, you may be
   wondering why we merely observed the loading of the addin instead of first
   writing a test. There are several good reasons for this.

   First, loading the addin is actually NUnit's job. The idea is that we don't
   generally test "other people's code" especially if it's already tested.
   Second, the only way to write such a test currently is to access some of the
   internals of NUnit. The problem with that is that it changes the environment
   of your test significantly, since tests do not normally access the NUnit core.
   In fact, accessing the core from your test may cause the addin to fail to load!

   So, for now, we are sticking with simple observation for testing that the addin
   works with NUnit's infrastructure. At some future point, there may be some
   utilities that will allow automating this part of our development.
</div>

### Getting Installed

You'll note that the IAddin interface only has a single method, and that we
didn't really do anything in it. Our addin is known to NUnit, but not yet
installed. Before we do that, lets define a few terms.

NUnit provides `ExtensionPoints`, grouped into three types: Core,
Client and Gui. In this Beta, only Core extensions are supported, and that's
what we are writing. ExtensionPoints are identified and accessed by a unique
name.

An `ExtensionHost` provides a specific set of `ExtensionPoints`.
In this Beta, there is only one host. It examines the list of Addins for
those that contain Core extensions and invites each one to install itself.

An `Addin`, as used the term is used in NUnit, means some set of functionality
implemented by one or more `Extensions`. In our example there is only one
Extension but other addins might need to install several different extensions
in order to do their job. Each `Extension` is installed at an `ExtensionPoint`.

<!-- Need a diagram here -->

For details of the `IAddin`, `IExtensionPoint` and `IExtensionHost`
interfaces, see the NUnit documentation.

We can modify the code above to get our addin installed...

```c#
public bool Install( IExtensionHost host )
{
   IExtensionPoint decorators = host.GetExtensionPoint( "TestDecorators" );
   if ( decorators == null ) return false;

   decorators.Install( this );
   return true;
}
```

The above fails to install the decorator. Unfortunately, the Beta release
gives no indication of this unless you run it with Trace activated. You
can do this from Visual Studio by setting ...

To __really__ get installed, we need to implement the interface that is
required by the TestDecorators extension point: `ITestDecorator`.

```c#
public interface ITestDecorator
{
    Test Decorate( Test test, MemberInfo member );
}
```

The single method is passed a reference to the Test being constructed and
another reference to the MemberInfo - either a Type or MethodInfo - being
used to construct the test. If the decorator doesn't want to modify the
test, it can simply return the same reference.

In our case, we only want to modify tests that have the MaxTime attribute
on the MemberInfo.

```c#
public Test Decorate( Test test, MemberInfo member )
{
   if ( member is Type ) return test;
   ...
}
```
