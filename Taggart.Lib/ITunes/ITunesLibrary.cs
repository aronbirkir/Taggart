using System.IO;
using System.Xml;

namespace Taggart.ITunes
{
    public class ITunesLibrary : TrackLibrary
    {
        public ITunesLibrary(string fileName)
        {
            using (var reader = XmlReader.Create(fileName, new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore }))
            {
                ReadLibrary(reader);
            }
        }

        public ITunesLibrary(TextReader libraryReader)
        {
            using (var reader = XmlReader.Create(libraryReader, new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore }))
            {
                ReadLibrary(reader);
            }
        }

        private void ReadLibrary(XmlReader reader)
        {
            reader.ReadToFollowing("dict");
            using (var libReader = reader.ReadSubtree())
            {
                libReader.Read();
                while (libReader.Read())
                {
                    if (libReader.Name == "key")
                    {
                        var key = libReader.Value;
                        if (key != "Tracks" || key != "Playlists")
                        {
                            libReader.Read();
                            var type = libReader.Name;
                            var value = libReader.Value;
                        }
                        else if (key == "Tracks")
                        {
                            libReader.Read();
                            using (var tracksReader = libReader.ReadSubtree())
                            {

                            }
                        }
                        else if (key == "Playlists")
                        {
                            libReader.Read();
                            using (var playlistReader = libReader.ReadSubtree())
                            {

                            }
                        }
                    }

                }
            }
        }
    }
}
