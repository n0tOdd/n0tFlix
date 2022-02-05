using System;
using System.IO;
using System.Xml;

namespace n0tFlix.Plugin.NapiSub.Helpers
{
    public static class XmlParser
    {
        public static string GetSubtitlesBase64(string xml)
        {
            if (xml == null) return null;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            var content = xmlDocument.GetElementsByTagName("content");

            return content.Count <= 0 ? null : content[0].InnerText;
        }

        public static Stream GetSubtitlesStream(string subtitlesBase64)
        {
            if (subtitlesBase64 == null) return null;

            var data = Convert.FromBase64String(subtitlesBase64);

            Stream stream = new MemoryStream(data, false)
            {
                Position = 0
            };

            return stream;
        }

        public static string GetStatusFromXml(string xml)
        {
            if (xml == null) return null;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            var status = xmlDocument.GetElementsByTagName("status");

            return status.Count <= 0 ? null : status[0].InnerText;
        }
    }
}
