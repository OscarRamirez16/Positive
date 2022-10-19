using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace eFacturacionColombia_V2.Util
{
    public static class ZipHelper
    {
        public static byte[] Compress(byte[] data, string entryName)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var entry = archive.CreateEntry(entryName);
                    using (var entryStream = entry.Open())
                    {
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            var text = Encoding.UTF8.GetString(data);
                            streamWriter.Write(text);
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }

        public static byte[] Compress(Dictionary<string, byte[]> files)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var entry = archive.CreateEntry(file.Key);
                        using (var entryStream = entry.Open())
                        {
                            using (var streamWriter = new StreamWriter(entryStream))
                            {
                                var text = Encoding.UTF8.GetString(file.Value);
                                streamWriter.Write(text);
                            }
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }
    }
}