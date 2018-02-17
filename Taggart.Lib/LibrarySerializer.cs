using System.Globalization;
using System.IO;
using System.Xml;

namespace Taggart
{

    public interface ILibrarySerializer
    {
        void Serialize(ITrackLibrary library, string fileName);
    }

    public class RekordBoxLibrarySerializer
    {
        public static void Serialize(ITrackLibrary library, TextWriter textWriter)
        {
            //var directory = Path.GetDirectoryName(fileName);
            //var tempFile = Path.Combine(directory, Path.GetFileNameWithoutExtension(fileName) + "_tmp" + Path.GetExtension(fileName));

            using (var writer = new XmlTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented; // optional, if you want it to look nice
                writer.WriteStartDocument();
                writer.WriteStartElement("DJ_PLAYLISTS");
                writer.WriteAttributeString("Version", "1.0.0");

                writer.WriteStartElement("PRODUCT");
                writer.WriteAttributeString("Name", "rekordbox");
                writer.WriteAttributeString("Version", library.Version);
                writer.WriteAttributeString("Company", "Pioneer DJ");

                writer.WriteStartElement("COLLECTION");
                writer.WriteAttributeString("Entries", library.Tracks.Count.ToString());
                foreach (var track in library.Tracks)
                {
                    writer.WriteStartElement("TRACK");
                    writer.WriteAttributeString("TrackId", track.TrackId.ToString());
                    writer.WriteAttributeString("Name", track.Name);
                    writer.WriteAttributeString("Artist", track.Artist);
                    writer.WriteAttributeString("Location", $"file://localhost/{track.FileName}");
                    foreach (var property in track.Properties)
                    {
                        writer.WriteAttributeString(property.Key, property.Value);
                    }
                    // TEMPO element
                    foreach (var tempo in track.Tempos)
                    {
                        writer.WriteStartElement("TEMPO");
                        writer.WriteAttributeString("Inizio", ((float)tempo.Inizio / 1000).ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("Bpm", tempo.Bpm.ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("Metro", tempo.Metro);
                        writer.WriteAttributeString("Battito", tempo.Battito.ToString());
                        writer.WriteEndElement();
                    }

                    // POSITION_MARK elements for cuepoings
                    foreach (var cuePoint in track.CuePoints)
                    {
                        writer.WriteStartElement("POSITION_MARK");
                        writer.WriteAttributeString("Name", cuePoint.Name);
                        writer.WriteAttributeString("Type", "0");
                        writer.WriteAttributeString("Start", ((float)cuePoint.Time / 1000).ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("Num", cuePoint.Number.ToString());
                        writer.WriteAttributeString("Red", "40");
                        writer.WriteAttributeString("Green", "226");
                        writer.WriteAttributeString("Blue", "20");
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteStartElement("PLAYLISTS");
                foreach (var playlistItem in library.Playlists)
                {
                    WritePlaylistItem(writer, playlistItem);
                }
                writer.WriteEndElement(); // PLAYLISTS

                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        private static void WritePlaylistItem(XmlTextWriter w, PlayListItem item)
        {
            w.WriteStartElement("NODE");
            if (item is PlaylistFolder)
            {
                var playlistFolder = item as PlaylistFolder;
                w.WriteAttributeString("Type", "0");
                w.WriteAttributeString("Name", item.Name);
                w.WriteAttributeString("KeyType", "0");
                w.WriteAttributeString("Count", playlistFolder.Playlists.Count.ToString());
                foreach (var child in playlistFolder.Playlists)
                {
                    WritePlaylistItem(w, child);
                }
            }
            else if (item is Playlist)
            {
                var playlist = item as Playlist;
                w.WriteAttributeString("Type", "1");
                w.WriteAttributeString("Name", item.Name);
                w.WriteAttributeString("KeyType", "0");
                w.WriteAttributeString("Entries", playlist.TrackIds.Count.ToString());
                foreach (var trackId in playlist.TrackIds)
                {
                    w.WriteStartElement("TRACK");
                    w.WriteAttributeString("Key", trackId.ToString());
                    w.WriteEndElement();
                }

            }
            w.WriteEndElement(); // NODE

        }
    }
}
