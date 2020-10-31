Title: "Common.Addins.Build : Addin Building Made Simple"
Published: 05 May 2008
Tags: [NUnit V2]
---
Let me start right out by saying: I **know** some of you won't find this to be simpler - but it is! If you can't bring yourself to install <a href="http://nant.sourceforge.net">NAnt</a> or work from the command line, then this isn't for you. But if you can get past the initial hump - or if you're already past it - then this is for you.

The hardest part about **building** - as opposed to **writing** - addins is getting the references right. Addin solutions in Visual Studio generally contain from three to five projects: the addin assembly, the assembly referenced from user tests, a unit test assembly, possibly a separate test fixture assembly for use by the tests and one or more samples. All of those have to reference the nunit assemblies they will be used with when they are installed. Getting it right is tedious at best, requiring multiple visits to the Add Reference Dialog. And from time to time, Visual Studio will decide to use a different assembly from the one you expected.

I finally got fed up with all this and decided to write a NAnt script for one of my addins - **CSUnitAddin** actually. After it was done, I looked closely and realized that most of it was boilerplate - the same code with a few property changes would build other addins. So I started to factor out the common code into an include file, with the result that my CSUnitAddin.build file now looks like this:

```xml
<?xml version="1.0"?>
<project name="CSUnitAddin" default="build" basedir=".">

  <!-- Include the common build file -->
  <include buildfile="../common.addin.build"/>    <!-- 1 -->

  <!-- Define non-default names for sample project -->
  <property name="sample.dll" value="money.dll"/>
  <property name="sample.dir" value="money"/>
  
  <!-- Define non-default names for fixture project -->
  <property name="fixture.dll" value="CSUnitTestFixtures.dll"/>
  <property name="fixture.dir" value="CSUnitTestFixtures"/>

  <!-- This addin is for the csunit framework -->
  <property name="target.framework.dll" value="csunit.dll"/>

  <!-- Define alt library with proper version of csunit         -->
  <!-- Must be here since lib.dir is defined in the common file -->
  <property name="csunit.version" value="2.0.4"/>
  <property name="alt.lib.dir" value="${lib.dir}/csUnit-${csunit.version}"/>

</project>
```

This example actually has to do more than most, since it uses a "foreign" testing framework. I'll walk through the steps for you:

1. First, the script includes the `common.addin.build` script, which will do all the work.
2. Next, it defines non-standard names for the sample assembly and it's directory. I could have eliminated this by calling them both "sample" but I wanted a non-generic name here.
3. I do the same thing for the "fixture" assembly - an assembly that contains csunit-based tests, which are loaded and run by my unit test assembly. In this case, I might have stuck with the default (CSUnitAddinFixture) and saved a few lines of xml.
4. I set the target.framework.dll to csUnit so that my sample and fixture assemblies will be built with a reference to that framework rather than the default nunit framework.
5. This particular addin has multiple library subdirectories for different versions of csUnit, so I set the alt.lib.dir property to indicate which one I want to use.

So, let's say that I want to build and test this addin with NUnit 2.5, using both the .NET 1.1 and 2.0 builds. I can do this with two commands:

```csharp
nant release net-1.1 nunit-2.5 clean build test
nant release net-2.0 nunit-2.5 clean build test
```

The script will locate my NUnit 2.5 installation, build the addin and it's associated assemblies (four in all), install it and run the tests using that same installation. NUnit 2.5 has separate net-1.1 and net-2.0 directories, which the script knows about, so the two builds will be installed separately.

This is how I'm building addins now. I use Visual Studio to do editing and syntax checking, leaving the actual builds to the script. As a result, I no longer need to deal with mismatched references or with the tedium of using the gui to switch between NUnit versions.

The Common.Addin.Build script is open source, licensed under the Academic Free Licence 3.0. You can get it on [NUnit.org](http://nunit.org) [Note: no longer available].
