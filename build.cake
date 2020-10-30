#reference "bin/Debug/net5.0/FluentFTP.dll"

using System.Security.Cryptography;
using System.Xml;
using FluentFTP;

var target = Argument("target", "Default");

// Supplementary options for CreateHash and Deploy targets

// Display additional information
bool verbose = HasArgument("verbose");

// Run CreateHash even if when hashes.xml already exists.
// Run Deploy even if hashes.xml does not exist, creating it.
bool force = HasArgument("force");

// After running target, check that hashes.xml matches 
// the local and remote content. Equivalent check to
// the Verify target.
bool verify = HasArgument("verify");

// Don't actually upload, but show what would happen
bool dryRun = HasArgument("dry-run"); 

#load constants.cake
#load hashes.cake
#load deployment.cake

Task("Build")
    .Does(() => StartProcess("dotnet", "run"));

Task("Serve")
    .Does(() => StartProcess("dotnet", "run -- serve"));

Task("Preview") // equivalent to build + serve
    .Does(() => StartProcess("dotnet", "run -- preview"));

Task("CreateHash")
    .IsDependentOn("Build")
    .Does(() => 
    {
        using (var ftp = new FtpDeploy { Verbose = verbose, DryRun = dryRun })
        {
            if (!force && ftp.FileExists(HASH_FILE))
                throw new Exception(HASH_ALREADY_EXISTS);

            if (!dryRun)
                ftp.UploadHashFile(HashDictionary.FromFiles(GetFiles(LOCAL_FILES)));

            Console.WriteLine(HASH_FILE_CREATED);
        }
    });

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() => 
    {
        using (var ftp = new FtpDeploy { Verbose = verbose, DryRun = dryRun })
        {
            // Download remote hash file and create dictionary
            HashDictionary remoteHashes = ftp.GetHashDictionary();

            if (!force && remoteHashes == null)
                throw new Exception(HASH_NOT_FOUND);

            // Create dictionary of local file hashes
            var localFiles = GetFiles(LOCAL_FILES);
            var localHashes = HashDictionary.FromFiles(localFiles);

            foreach(var path in localHashes.Keys)
            {
                if (remoteHashes == null)
                {
                    ftp.UploadFile(path);
                }
                else if (!remoteHashes.ContainsKey(path))
                {
                    ftp.UploadFile(path);
                }
                else if (remoteHashes[path] != localHashes[path])
                {
                    ftp.UploadFile(path);
                }
                else if (verbose)
                    Console.WriteLine($"No change: {path}");
            }

            if (remoteHashes != null)
                foreach(var path in remoteHashes.Keys)
                {
                    if (!localHashes.ContainsKey(path))
                        ftp.DeleteFile(path);
                }

            Console.WriteLine($"Files Uploaded: {ftp.Uploaded} Deleted: {ftp.Deleted}");

            if (ftp.Uploaded > 0 || ftp.Deleted > 0)
                ftp.UploadHashFile(localHashes);
        }
    });

Task("Verify")
    .Description("Check that local and remote files match content of hashes.xml")
    .Does(() =>
    {
        using (var ftp = new FtpDeploy { Verbose = verbose })
        {
            var localHashes = HashDictionary.FromFiles(GetFiles(LOCAL_FILES));
            var remoteHashes = ftp.GetHashDictionary();
            if (remoteHashes == null)
                throw new Exception($"Error: {HASH_FILE} was not found.");

            int msgCount = 0;
            
            if (localHashes.Count != remoteHashes.Count)
                Console.WriteLine($"{++msgCount}) Local and remote dictonaries are different sizes");

            foreach (string path in localHashes.Keys)
            {
                if (!remoteHashes.ContainsKey(path))
                    Console.WriteLine($"{++msgCount}) Hash file does not contain {path}");
            }

            foreach (string path in remoteHashes.Keys)
            {
                if (!localHashes.ContainsKey(path))
                    Console.WriteLine($"{++msgCount}) File {path} not found locally");
                else if (localHashes[path] != remoteHashes[path])
                {
                    Console.WriteLine($"{++msgCount}) Hashes for {path} do not match");
                    Console.WriteLine($"    Local: {localHashes[path]}");
                    Console.WriteLine($"   Remote: {remoteHashes[path]}");
                }
            }

            if (msgCount > 0)
                throw new Exception($"Found {msgCount} errors");
        }
    });

Task("HashIsUnique")
    .Does(() =>
    {
        var files = GetFiles("output/*");
        var hash = HashDictionary.FromFiles(files);

        assertNotEqual(hash["bio.html"], hash["status.html"]);
    });

Task("HashIsRepeatable")
    .Does(() =>
    {
        var files = GetFiles("output/bio.html");
        var hash1 = HashDictionary.FromFiles(files);
        var hash2 = HashDictionary.FromFiles(files);

        assertEqual(hash1["bio.html"], hash2["bio.html"]);
    });

Task("BuildingSiteDoesNotChangeHash")
    .Does(() =>
    {
        var files = GetFiles("output/bio.html");
        var hash1 = HashDictionary.FromFiles(files);

        StartProcess("dotnet", "run");

        var hash2 = HashDictionary.FromFiles(files);

        assertEqual(hash1["bio.html"], hash2["bio.html"]);
    });

private void assertThat(bool condition, string msg)
{
    if (!condition)
        throw new Exception(msg);
}

private void assertEqual<T>(T expected, T actual)
{
    if (!expected.Equals(actual))
        throw new Exception($"Expected: {expected}\n  But was: {actual}");
}

private void assertNotEqual<T>(T expected, T actual)
{
    if (expected.Equals(actual))
        throw new Exception($"Expected unequal values\n  But both were: {actual}");
}

Task("Test")
    .IsDependentOn("HashIsUnique")
    .IsDependentOn("HashIsRepeatable")
    .IsDependentOn("BuildingSiteDoesNotChangeHash");

Task("Default")
    .IsDependentOn("Build");
    
RunTarget(target);
