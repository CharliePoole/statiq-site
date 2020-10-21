Title: "NUnit 2.2.7 Release / NUnit Version Numbers"
Published: 19 Feb 2006
Tags: [NUnit,It's the Tests]
---
NUnit 2.2.7 has just been released. It fixes some bugs that were filed against the 2.2.6 release, although some of them had been in the code for a while. You can read about what's fixed in this latest release, as well as earlier releases, in the Release Notes.

<!--more-->
The numbering of these releases seems to confuse people and I have to admit it was probably a mistake. NUnit 2.2.4 was a new production release, just like 2.0, 2.1 and 2.2. NUnit 2.2.5, 2.2.6 and now 2.2.7 are all fixes on the same set of features found in 2.2.4. Bug fix releases are intended to be safe and easy upgrades, in the sense that we don't add much new code to cause new bugs. We only fix what's broken.

In the future, this will be clearer. A new set of features will have a new minor version number. The next such release will be 2.4. Once it's out, any bug fixes will be released as 2.4.1, 2.4.2, etc.

In case you hadn't noticed, we're now using even minor versions for our stable production releases: 2.2, 2.4, 2.6... As you might guess, these are the releases we hope you'll for your production work.

Having those releases allows us to be a little more daring and experimental in our development - or unstable - releases. They use odd minor version numbers, so you won't use them by mistake. You can get the source for these releases - no binaries - from our CVS repository or in the periodic snapshots we issue. The current development version is 2.3 and the latest snapshot was 2.3.6024.

---

### Comments

---

Yaron: Not yet. We plan to move to running tests optionally in a separate process first, then to running in remote machines. See our Roadmap for other future directions.
>Charlie, Sunday, March 5, 2006

---

do you have a feature of running tests in remote machine ?
>Yaron, Sunday, March 5, 2006

---

I was wondering if there's going to be a matrix type of test added soon?

For example, writing a test for a matrix of values and having the core treat each matrix value as a test instance?  I believe Microsoft has touted this as a feature in their testing framework.
>Eric Newton, Wednesday, March 22, 2006

---

I'm not aware of this feature of the Microsoft framework, can you point me to it? I do know about their data-driven tests, based on SQL, and I guess you could create something like what you describe using that, but it seems like a very big hammer for a very small nail.

Do you mean to treat each matrix value as a test instance, or each row? I can see different possiblilities here?

I can see this as a published add-in, and maybe even one that is bundled with NUnit. The place to start is to describe a desired syntax for a certain class of problems. You could post your ideas here as a comment or to the developer mailing list.
>Charlie, Sunday, March 26, 2006

---

I am facing a problem when using VSTS with NUnit 2.2.6.
I am testing a website. After opening the browser it closes immidiately and
I am getting the following error.

 !!!SCENARIO FAILURE!!!
    Info:       Failed to execute test cases due to run time error: 'The HWnd is no longer valid'
    Image:     file:///C:\Program%20Files\NUnit-Net-2.0%202.2.6\bin\MAUI_Chrome%20MastHead%20section-Scen10.jpg

Is the problem with NUnit 2.2.6? Let me know.
>Deevish, Monday, April 17, 2006

---

Updating the icon has been on my list of todos for a long time. Several folks have sent in candidates and it only requires my taking the time to set up a place for users to view them, to offer others and to express their opinions. Only that... :-(
>Charlie, Thursday, April 20, 2006

---

Are there any plans to update the icon for NUnit GUI? It's a shame that such a great application has such an... unprofessional logo. I know it's only aesthetics but you do have to click that logo every time you run the GUI, and I cringe every time. It's especially eye-wateringly bad when sitting on my desktop in full 64x64 pixel glory. No offense intended, I love NUnit and wouldn't be without it :)
>Luke Sampson, Wednesday, April 19, 2006

---

Hi Gerard, long time no talk!

As you note, I asked those who thought we should change how this works to come to the dev list to discuss it. But nobody came!

There was a fairly substantial discussion of it on the TDD list at one point, which you may want to look for. Ron and others expressed opinions, but we didn't come up with any substantial agreement on chaning to the multiple instance pattern. [There was agreement that we should fix the somewhat broken operation under the gui, which reuses objects across test runs.]

As of 2.2.8, we still use one object for all the tests in a TestFixture, and we still keep the object around for subsequent runs. This doesn't cause a problem so long as all the initialization is done in SetUp or TestFixtureSetUp. For the 2.4 release, which is about to go Alpha, I'm trying to get the object life down to one run. This turns out to be harder than you might think. We'll also honor any IDisposable implementation by calling Dispose before releasing the object.

If you need any copies of the latest code in preparation for the book, drop me a note.

Charlie
>Charlie, Saturday, April 29, 2006

---

I'm almost finished my book on xUnit Test Patterns and I want to make sure my information on NUnit is as recent as possible. Back in December 2004, James Newkirk opined at http://blogs.msdn.com/jamesnewkirk/archive/2004/12/04/275172.aspx that changing the way NUnit treats instance variables from how JUnit does it was a mistake because it let fixtures created in one test method be visible in another test method. (I actually got burned by this in one of my first forays into .Net, but that's another story!) Charlie responded on that blog that it wouldn't be that hard to change and suggested moving the discussion over to the NUnit mailing list. I'm trying to find out whether that change was ever implemented and if not yet, whether there are any plans to do so in the next year.

Thanks in advance, 

Gerard

http://xunitpatterns.com
>Gerard Meszaros, Friday, April 28, 2006

---

You will have to make that request of the author of NUnitAsp, which is an entirely different project.
>Charlie, Thursday, June 8, 2006

---

i am facing the problem when i am using NUnitAsp with 2.2.6 version.
i need NUnitAsp.dll for my project.so please suggest me the correct version of NUnit for that.
>vikrant, Thursday, June 8, 2006
