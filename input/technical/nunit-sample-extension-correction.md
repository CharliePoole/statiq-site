Title: "NUnit Sample Extension Correction"
Published: 08 Nov 2006
Tags: [NUnit, It's the Tests]
---
As pointed out by Sara Lee McLindon, the SampleFixtureExtension example in the NUnit 2.4 Beta 2 release doesn't actually work. You can correct it by adding the following method to the SampleFixtureExtensionBuilder class.

```csharp	
protected override TestSuite MakeSuite(Type type)
{
	return new SampleFixtureExtension( type );
}
```

Without this fix, the builder simply creates an NUnitTestFixture and no new behavior takes place when the test is run.
