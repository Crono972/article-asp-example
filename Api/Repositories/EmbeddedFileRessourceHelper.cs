using System.Reflection;

namespace Api.Repositories;

public static class EmbeddedFileRessourceHelper
{
    public static string GetFileContent(string fileName)
    {
        var assembly = Assembly.GetCallingAssembly();
        var resourcePath = assembly.GetManifestResourceNames().FirstOrDefault(f => f.Contains(fileName, StringComparison.InvariantCultureIgnoreCase));
        if (resourcePath == null)
        {
            return null;
        }

        using Stream stream = assembly.GetManifestResourceStream(resourcePath)!;
        using StreamReader reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public static byte[] GetRawContent(string fileName)
    {
        var assembly = Assembly.GetCallingAssembly();
        var resourcePath = assembly.GetManifestResourceNames().FirstOrDefault(f => f.Contains(fileName, StringComparison.InvariantCultureIgnoreCase));
        if (resourcePath == null)
        {
            return null;
        }

        using Stream stream = assembly.GetManifestResourceStream(resourcePath)!;
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}