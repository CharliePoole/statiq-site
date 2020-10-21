Title: "NUnit Extras: Test Result Summarizer"
Published: 08 Aug 2007
Tags: [NUnit,It's the Tests]
---
NUnit is currently built under five different runtimes on Windows - plus two on Linux. Each build is tested under the same runtime on which it was built and on a number of the other runtimes. That's a lot of TestResult files.

I use nunit-summary.exe to quickly view what happened in a particular run. For example,


```com
nunit-summary TestResult-mono-1.0.xml -noheader -brief
```

gives me a simple, one-line summary of what happened when I ran my tests under mono-1.0 - the naming convention is built into my build script. The following command gives me  a set of files TestResult-net-1.0.txt, TestResult-net-1.1.txt, etc. each containing the summary line as well as any failed or not run tests for the corresponding xml file.

```com
nunit-summary TestResult*.xml -out=*.txt
```

See the help for the command for other things you can do, including using your own xsl to transform the output.

The nunit-summary program is available on <a href="https://github.com/nunit/nunit-summary">GitHub</a>.

---

### Comments

---

[...] NUnit Extras: Test Result Summarizer- NUnit is so hip. [...]
>The Disco Blog &raquo; Blog Archive &raquo; The weekly bag&#8211; August 31, Sunday, September 2, 2007
