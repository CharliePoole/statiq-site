Title: "Moving to WiX"
Published: 23 Sep 2006
Tags: [NUnit,Open Source,WiX,It's the Tests]
---
NUnit has finally gotten rid of it's Visual Studio Install projects in favor of WiX

VS projects support only a simple, single-feature install. What's more, if you outgrow your install project, you can't simply migrate it to another tool. You have to start over with the new tool. It would be cool if VS could use WiX syntax as it's internal format, but I suspect that's not likely to happen since it would tie future development of the two together in some ways that would probably discomfit both WiX and Visual Studio. But an open source converter from the vdproj format to WiX would definitely be a cool thing!

One side effect of this change is that users of C# Express can now load and build the entire NUnit solution. I'm hoping this will gain us a few volunteers.

Jamie Cansdale - of NUnit Addin and TD.Net fame - did the initial implementation of the WiX scripts and I made additional changes and figured out how to use the the NAnt tasks so it could be made part of our standard build.

It's unfortunate that the WiX tasks for NAnt come without documentation. By reading the source, I was able to construct the commands I listed below, which I'm posting in the hope that they will be of help to somebody.

```xml
&lt;candle out="${wix.work.dir}/" exedir="${wix.dir}">
  &lt;defines>
    &lt;define name="ProductVersion" value="${package.version}" />
  &lt;/defines>
  &lt;sources basedir="${project.install.dir}">
    &lt;nclude name="bin.wxs" />
    &lt;include name="nunit-gui.wxs" />
    &lt;include name="doc.wxs" />
    &lt;include name="tests.wxs" />
    &lt;include name="samples.wxs" />
    &lt;include name="NUnit.wxs" />
  &lt;/sources>
&lt;/candle>

&lt;light exedir="${wix.dir}"
  out="${project.package.dir}/${msi.file.name}" 
  locfile="${wix.dir}/WixUI_en-us.wxl">
  &lt;sources>
    &lt;include name="${wix.work.dir}/NUnit.wixobj" />
    &lt;include name="${wix.work.dir}/bin.wixobj" />
    &lt;include name="${wix.work.dir}/nunit-gui.wixobj" />
    &lt;include name="${wix.work.dir}/doc.wixobj" />
    &lt;include name="${wix.work.dir}/samples.wixobj" />
    &lt;include name="${wix.work.dir}/tests.wixobj" />
    &lt;include name="${wix.dir}/wixui.wixlib" />
  &lt;/sources>
&lt;/light>
```

You'll find our WiX scripts in the latest version of the NUnit source on SourceForge.

Charlie

---

### Comments

---

[...] Tonight I traveled to Anthony's Beach Cafe in Edmonds, WA to have dinner with Charlie Poole.&nbsp; Charlie is the active maintainer of the NUnit project.&nbsp; He contacted me (via my blog, IIRC) and we set up tonight's meeting.&nbsp; I expected he wanted to meet because NUnit is now using the WiX toolset too. [...]
>Rob Mensching Openly Uninstalled : Thursday Night &amp;quot;Deployment Dinner Discussion&amp;quot; with Charlie Poole., Thursday, October 5, 2006

---

Though your comments about missing tasks doc are fair, there are several build files in the WiX source that use the tasks. So code-spelunking isn't strictly necessary. :)
>Bob Arnson, Wednesday, September 27, 2006

---

I know it's hard to find the time for docs and I've been guilty of the same thing myself, so you have my sympathy.

Finding the build files that use the tasks appears to be harder than you think. At least, I can't find them. :-) Are they in V3, perhaps?
>Charlie, Wednesday, September 27, 2006

---

I like the fact that you can call &lt;candle&gt; and the same for light in NAnt.  My question, kinda goes along with what your saying no documentation, how do I install those tasks?  I'm using a nightly build of NAnt and the RC of NAntcontrib and I cannot seem to find that info anywhere?  How did you install those tasks?  Was there a how-to guide that you used?
>RLovelett, Friday, May 18, 2007

---

I simply copied the task assembly to my nant bin\tasks\net directory. The assembly is Microsoft.Tools.WindowsInstallerXml.NantTasks.dll.
>Charlie, Monday, May 21, 2007

---

I also tried created WiX under the toolds folder and unzip the bin into it; then add
    in the .build file 

but  I get the following when calling nant  create-msi ..... : 
r extensions.

BUILD FAILED - 2 non-fatal error(s), 0 warning(s)

C:\Automation\ThirdParty\Nunit\src\nunit.build(563
Property evaluation failed.
Expression: ${project.package.dir}/${msi.file.name
                                     ^^^^^^^^^^^^^
    Property 'msi.file.name' has not been set.


what am I missing?
>edwardt_tril, Monday, July 9, 2007

---

The target create-msi is no longer intended for direct use from the command line. Instead use package-msi, which builds nunit first, then calls create-msi.

More info: I originally was creating the msi as a separate manual step, but I found that I continually forgot to rebuild nunit so I changed this. Create-msi is a vestige of the older approach. I'll either make it work correctly or move it to the include file so it isn't listed by -projecthelp.

Charlie
>Charlie, Tuesday, July 10, 2007
