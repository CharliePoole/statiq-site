private class FtpDeploy : IDisposable
{
    FtpClient _client;

    public FtpDeploy()
    {
        _client = new FtpClient();

        _client.Host = HOST_SITE;
        _client.Credentials = new System.Net.NetworkCredential(USER_NAME, PASSWORD);
        _client.Connect();

        Console.WriteLine($"Connected to {HOST_SITE}");
    }

    public bool Verbose { get; set; }

    public bool DryRun { get; set; }

    public int Uploaded { get; private set; }

    public int Deleted { get; private set; }

    public HashDictionary GetHashDictionary()
    {
        if (_client.FileExists(HASH_FILE))
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (_client.Download(stream, HASH_FILE))
                    return HashDictionary.LoadFrom(stream);
            }
        }

        return null;
    }

    public bool FileExists(string path)
    {
        return _client.FileExists(path);
    }

    public bool Download(Stream stream, string path)
    {
        return _client.Download(stream, path);
    }

    public void UploadFile(string path)
    {
        string localPath = LOCAL_PATH + path;
        string remotePath = REMOTE_PATH + path;
        Console.WriteLine($"Uploading {localPath} to {remotePath}");

        if (!DryRun)
        {
            _client.UploadFile(localPath, remotePath);
        }

        Uploaded++;
    }

    public void DeleteFile(string path)
    {
        string remotePath = REMOTE_PATH + path;
        Console.WriteLine($"Deleting {remotePath}");

        if (!DryRun)
        {
            _client.DeleteFile(remotePath);
        }

        Deleted++;
    }

    public void UploadHashFile(HashDictionary hashes)
    {
        if (Verbose)
            Console.WriteLine($"Uploading {HASH_FILE} to {HOST_SITE}");

        if (!DryRun)
        {
            using (MemoryStream stream = new MemoryStream())
            using (TextWriter writer = new StreamWriter(stream))
            {
                hashes.WriteTo(writer);
                _client.Upload(stream, HASH_FILE);
            }
        }
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
