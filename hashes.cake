public class HashDictionary : Dictionary<string, string>
{
    public static HashDictionary FromFiles(IEnumerable<FilePath> filePaths)
    {
        int baseLength = System.IO.Path.GetFullPath(LOCAL_PATH).Length;
        HashDictionary dict = new HashDictionary();
        SHA256 Sha256 = SHA256.Create();

        foreach (var filePath in filePaths)
        {
            string path = filePath.ToString();

            using (FileStream stream = System.IO.File.OpenRead(path))
            {
                stream.Position = 0;
                dict.Add(
                    path.Substring(baseLength),
                    BitConverter.ToString(Sha256.ComputeHash(stream)).Replace("-", ""));
            }
        }

        return dict;
    }

    public static HashDictionary LoadFrom(string fileName)
    {
        var doc = new XmlDocument();
        doc.Load(fileName);
        return LoadFrom(doc);
    }

    public static HashDictionary LoadFrom(MemoryStream stream)
    {
        var bytes = stream.GetBuffer();
        var xml = System.Text.Encoding.Default.GetString(bytes, 0, (int)stream.Length);

        var doc = new XmlDocument();
        doc.LoadXml(xml);
        return LoadFrom(doc);
    }

    private static HashDictionary LoadFrom(XmlDocument doc)
    {
        var dict = new HashDictionary();

        foreach(XmlNode node in doc.DocumentElement.SelectNodes("file"))
        {
            string path = node.Attributes["path"]?.Value;
            string hash = node.Attributes["hash"]?.Value;

            dict.Add(path, hash);
        }

        return dict;
    }

    public void WriteTo(TextWriter writer)
    {
        writer.WriteLine("<files>");

        foreach (var entry in this)
            writer.WriteLine($"  <file path='{entry.Key}'\n        hash='{entry.Value}' />");

        writer.WriteLine("</files>");

        writer.Flush();
    }
}
