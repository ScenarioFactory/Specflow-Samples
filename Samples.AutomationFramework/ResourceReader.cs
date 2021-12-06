namespace Samples.AutomationFramework
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    public static class ResourceReader
    {
        public static byte[] GetBytes(string embeddedResourceName)
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResourceName);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            return bytes;
        }

        public static XDocument GetXmlDocument(string embeddedResourceName)
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResourceName);

            return XDocument.Load(stream);
        }

        public static string[] GetAllLines(string embeddedResourceName)
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResourceName);
            using StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd().Split(Environment.NewLine).ToArray();
        }
    }
}