using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Channels;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace n0tFlix.Manifest.Creator
{
    internal class Program
    {

        public class AppPath : IApplicationPaths
        {
            public string ProgramDataPath =>"";

            public string WebPath =>"";

            public string ProgramSystemPath =>"";

            public string DataPath =>"";

            public string ImageCachePath =>"";

            public string PluginsPath =>"";

            public string PluginConfigurationsPath =>"";

            public string LogDirectoryPath =>"";

            public string ConfigurationDirectoryPath =>"";

            public string SystemConfigurationFilePath =>"";

            public string CachePath =>"";

            public string TempDirectory =>"";

            public string VirtualDataPath =>"";
        }

        public class xmls : IXmlSerializer
        {
            public object DeserializeFromBytes(Type type, byte[] buffer) => throw new NotImplementedException();
            public object DeserializeFromFile(Type type, string file) => throw new NotImplementedException();
            public object DeserializeFromStream(Type type, Stream stream) => throw new NotImplementedException();
            public void SerializeToFile(object obj, string file) => throw new NotImplementedException();
            public void SerializeToStream(object obj, Stream stream) => throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            string rooturl = "https://raw.githubusercontent.com/n0tOdd/n0tFlix/main/Build/";// Console.ReadLine();
            if(!rooturl.EndsWith("/"))
                rooturl = rooturl + "/";
            List<Manifestdata> liste = new List<Manifestdata>();
            FileInfo me = new FileInfo(Assembly.GetExecutingAssembly().Location);
            foreach(var zipfile in me.Directory.Parent.Parent.Parent.GetFiles("*.zip", SearchOption.AllDirectories))
            {
                using ZipArchive zipArchive = ZipFile.OpenRead(zipfile.FullName);
                foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                {
                    if (zipArchiveEntry.Name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                    {
                        
                        try
                        {
                            Stream stream = zipArchiveEntry.Open();
                            using MemoryStream memoryStream = new MemoryStream();
                            stream.CopyTo(memoryStream);
                            memoryStream.Position = 0;
                            Assembly dll = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromStream(memoryStream);
                            dll.GetReferencedAssemblies().ToList().ForEach(assembly => {
                                Console.WriteLine(assembly.FullName);
                                try
                                {
                                    AppDomain.CurrentDomain.Load(assembly);
                                }
                                catch (Exception ex)
                                {

                                }
                            });
                            var type = typeof(IPlugin);
                            var matched = dll.GetTypes().Where(p => type.IsAssignableFrom(p));
                            foreach (var m in matched)
                            {
                                IApplicationPaths applicationPaths = new AppPath();
                                IXmlSerializer xmlSerializer = new xmls();
                                Object[] arg = { applicationPaths, xmlSerializer };

                                var Instance = Activator.CreateInstance(m, arg) as IPlugin;
                                Console.WriteLine(zipArchiveEntry.Name);

                                var ver = new Manifestdata.Version()
                                {
                                    sourceUrl = rooturl + zipfile.Name,
                                    timestamp = DateTime.Now.ToString("yyyy-mm-ddThh:mm:ssZ"),
                                    targetAbi = "10.7.7.0",
                                    version = Instance.Version.ToString(),
                                    changelog = "Check github for changelog",
                                    checksum = GetMD5(zipfile.FullName)
                                };
                                Manifestdata manifestdata = new Manifestdata()
                                {
                                    name = Instance.Name,
                                    category = GetCategory(dll),
                                    description = Instance.Description,
                                    guid = Instance.Id.ToString(),
                                    overview = Instance.Description,
                                    owner = "n0tme",
                                    imageUrl = rooturl + zipfile.Name.Replace(".zip",".png"),
                                    versions = new System.Collections.Generic.List<Manifestdata.Version>() { ver },
                                };
                                liste.Add(manifestdata);

                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Failed on: " + zipArchiveEntry.Name);
                            Console.WriteLine(ex.Message);

                        }

                    }
                }
            }

            string json = System.Text.Json.JsonSerializer.Serialize<List<Manifestdata>>(liste);
            json = json.Replace("[", "[\n");
            File.WriteAllText(Path.Combine(me.Directory.Parent.Parent.Parent.FullName,"Manifest", "n0tFlix-Manifest-Template.json"), json);
        }
        public static string GetCategory(Assembly dll)
        {
            var type = typeof(ISubtitleProvider);
            var matched = dll.GetTypes().Where(p => type.IsAssignableFrom(p));
            if (matched.Count() > 0)
                return "n0tSubtitles";
            type = typeof(IChannel);
            matched = dll.GetTypes().Where(p => type.IsAssignableFrom(p));
            if (matched.Count() > 0)
                return "n0tChannels";


            return "n0tOthers";
        }
        public static string GetMD5(string FilePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(FilePath))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-","");
                }
            }
        }
        protected virtual Assembly LoadAssembly(MemoryStream peStream, MemoryStream pdbStream)
        {
            return System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromStream(peStream, pdbStream);
        }
    }
}
