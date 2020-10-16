Title: "Using Addins with NUnit-Console"
Published: 02 May 2008
Tags: [NUnit,It's the Tests]
---
A recent bug pointed out that addins are not recognized when running tests under the console runner. This is due to a missing entry in the **nunit-console.exe.config** file, which you can easily fix yourself. Follow these steps to have your addins recognized when using the console runner:

1. Open the **nunit-console.exe.config** file in any convenient editor - notepad is fine.
2. Find the `<runtime>` element
3. Insert the code below within the `<runtime>` element. You can copy it from **nunit.exe.config** if you prefer

```xml
<assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <probing privatePath="addins"/>
</assemblyBinding>
```

From this point on, your addins should work equally well in both the Gui and Console runners.
