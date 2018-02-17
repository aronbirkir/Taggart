using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Taggart.ITunes
{
    public class MusicLibrary
    {
        readonly static private Regex VideoRegex = new Regex("video", RegexOptions.IgnoreCase);


        public MusicLibrary()
        {
            Albums = new Dictionary<Album, IList<Track>>();
        }

        #region Properties

        public IList<Track> this[Album album]
        {
            get { return Albums[album]; }
        }

        public IDictionary<Album, IList<Track>> Albums { get; set; }

        #endregion

        /// <summary>
        /// Creates a track by parsing its values from the given reader
        /// </summary>
        /// <param name="reader">The reader to use for parsing the track information. The reader should be placed
        /// at the &lt;dict&gt; node that contains the track information</param>
        /// <returns>The parsed track</returns>
        public void AddTrack(XmlReader reader)
        {
            XDocument document = XDocument.Load(reader);
            if (document.Root == null)
            {
                return;
            }

            if (IsTrackTypeIgnored(document.Root))
            {
                return;
            }

            Album album = LoadAlbum(document.Root);
            if (album != null)
            {
                Albums[album].Add(LoadTrack(document.Root, album));
            }
        }

        static private bool IsTrackTypeIgnored(XContainer root)
        {
            // Ignore podcasts
            if (String.Equals(ExtractString(root, "Genre"), "Podcast", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            // Ignore videos
            return VideoRegex.IsMatch(ExtractString(root, "Kind") ?? String.Empty);
        }

        private Album LoadAlbum(XContainer root)
        {
            string artist = ExtractString(root, "Album Artist") ?? ExtractString(root, "Artist");
            string name = ExtractString(root, "Album");
            int year = ExtractInt(root, "Year");

            if (artist == null || name == null)
            {
                string location = ExtractString(root, "Location");
                if (!String.IsNullOrEmpty(location))
                {
                    Console.Error.WriteLine("Ignoring: {0}", location);
                }

                // If there's no location, this is probably a playlist node
                return null;
            }

            var album = Albums.Keys.SingleOrDefault(existing =>
                existing.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) &&
                existing.Artist.Equals(artist, StringComparison.CurrentCultureIgnoreCase) &&
                existing.Year.Equals(year));

            if (album == null)
            {
                album = new Album
                {
                    Artist = artist,
                    Name = name,
                    Year = year
                };

                Albums.Add(album, new List<Track>());
            }

            return album;
        }

        static private Track LoadTrack(XContainer root, Album album)
        {
            return new Track
            {
                Album = album,
                Artist = ExtractString(root, "Artist"),
                BitRate = ExtractInt(root, "Bit Rate"),
                Name = ExtractString(root, "Name"),
                SampleRate = ExtractInt(root, "Sample Rate"),
                TotalTime = ExtractInt(root, "Total Time"),
                TrackNumber = ExtractInt(root, "Track Number")
            };
        }

        static private string ExtractString(XContainer root, string attributeName)
        {
            var keyNode = root.Nodes().FirstOrDefault(node => node.NodeType == XmlNodeType.Element && ((XElement)node).Value == attributeName);
            if (keyNode == null)
            {
                return null;
            }

            var valueNode = (XElement)keyNode.NextNode;
            if (valueNode == null || String.IsNullOrEmpty(valueNode.Value))
            {
                return null;
            }

            return valueNode.Value.Replace("%20", " ").Trim();
        }

        static private int ExtractInt(XContainer root, string attributeName)
        {
            string value = ExtractString(root, attributeName);
            return String.IsNullOrEmpty(value) ? 0 : Int32.Parse(value);
        }

    }
}
