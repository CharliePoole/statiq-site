Title: An Engine Extension for Running Failed Tests
Lead: "Part 2: Installing the Extension"
Published: 30 Nov -000
Tags: General
---
In [part 1](/technical/an-engine-extension-for-running-failed-tests-part-1.html) of this article, I showed how to create an extension that would save the names of all failed tests in a file that could be used to re-run those same tests at a later time. We tested the extension by installing it temporarily in our project. Now it's time to make the extension available for more general use.

### How NUnit Finds Extensions

In order to install the extension, it will be helpful to have an understanding of how NUnit finds extensions. The engine uses files of type `.addins` to indicate where extensions are located. Each line of the file contains the path of an addin assembly or a directory containing assemblies. Wildcards may be used for assembly names and also in directory paths. Relative paths are interpreted based on the location of the `.addins` file itself. Absolute paths are permitted, but are usually a bad idea! The following example illustrates entries that might be found in an `.addins` file.

```text
# This line is a comment. Blank lines are ignored as well.

*.dll                   # include all dlls in the same directory as this file
addins/*.dll            # include all dlls in the addins sub-directory too
special/myassembly.dll  # include a specific dll in a special sub-directory

# Directory entries are allowed as well. A terminating slash is required.
/some/other/directory/  # process another directory, which may contain its own addins file or files
../addin*/              # process all sibling directories with names starting with "addin"
*/                      # process all sub-directories
**/                     # process all sub-directories, recursively
```

In [part 1](/technical/an-engine-extension-for-running-failed-tests-part-1.html), we tricked NUnit into loading our extension for test purposes by creating a directory structure that made it look as if the extension had been installed as a NuGet package. We were able to do that because the ConsoleRunner package includes an `.addins` file that looks like this:

```text
../../NUnit.Extension.*/**/tools/     # nuget v2 layout
../../../NUnit.Extension.*/**/tools/  # nuget v3 layout
```

If you work your way through the paths listed, starting with the tools directory under the ConsoleRunner package, you'll see that your extension is indeed located this way.
