using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Taggart.ITunes
{
    public class ITunesLibraryParser
    {
        public ITunesLibraryParser(ParserOptions options)
        {
            Options = options;
        }

        #region Properties

        public ParserOptions Options { get; private set; }

        public MusicLibrary Library { get; private set; }

        static private XmlReaderSettings ReaderSettings
        {
            get
            {
                return new XmlReaderSettings
                {
                    IgnoreWhitespace = true,
                    DtdProcessing = DtdProcessing.Parse
                };
            }
        }

        private XmlReader Reader { get; set; }

        #endregion

        public void Run()
        {
            LoadLibrary();
            new LibraryWriter(Library).WriteHtml(Options.OutputPath);
        }

        private void LoadLibrary()
        {
            Library = new MusicLibrary();
            using (Stream stream = File.OpenRead(Options.InputPath))
            {
                using (Reader = XmlReader.Create(stream, ReaderSettings))
                {
                    MoveToTracks();

                    while (Reader.Read())
                    {
                        if (!(Reader.NodeType == XmlNodeType.Element && String.Equals(Reader.Name, "dict")))
                        {
                            continue;
                        }

                        Library.AddTrack(Reader.ReadSubtree());
                    }
                }
            }

            Console.WriteLine("Loaded {0} albums", Library.Albums.Count);
        }

        /// <summary>
        /// Moves the reader to the dictionary of tracks. This is found at: plist/dict/dict
        /// </summary>
        private void MoveToTracks()
        {
            MoveTo("plist");
            MoveTo("dict");
            MoveTo("dict");
        }

        /// <summary>
        /// Moves the reader to the nominated node. If the node was not found, an exception is thrown.
        /// </summary>
        /// <param name="node">Name of the node to move to</param>
        private void MoveTo(string node)
        {
            if (!Reader.ReadToDescendant(node))
            {
                throw new ParserException("Could not read to descendent: {0}", node);
            }
        }
    }

    public class ParserException : Exception
    {
        public ParserException(string message, params object[] args) : base(String.Format(message, args))
        {
        }
    }

    public class ParserOptions
    {
        public ParserOptions(string[] arguments)
        {
            ParseArguments(arguments);
        }


        #region Properties

        /// <summary>
        /// Get/Set the full path to the file to be parsed
        /// </summary>
        public string InputPath { get; set; }

        /// <summary>
        /// Get/Set the full path to the file to be written
        /// </summary>
        public string OutputPath { get; set; }

        #endregion


        private void ParseArguments(string[] arguments)
        {
            if (arguments == null || arguments.Length < 2)
            {
                throw new ArgumentException("Not enough arguments supplied");
            }

            InputPath = arguments[0];
            if (!File.Exists(InputPath))
            {
                throw new ArgumentException(String.Format("The input file does not exist: {0}", InputPath));
            }

            OutputPath = arguments[1];
        }
    }

    public class LibraryWriter
    {
        private const string XSpace = " ";

        private const string Css = @"
            div.artist {
                padding: 0.5em 1em;
                margin-top: 1em;
                border: solid 1px #333;
                background-color: #f1f1f1;
            }

            table.album {
                margin: 0 auto 1em;
                width: 90%
            }
            ";


        public LibraryWriter(MusicLibrary library)
        {
            Library = library;
        }


        #region Properties

        public MusicLibrary Library { get; private set; }

        private XmlWriter Writer { get; set; }

        private string LastArtist { get; set; }

        static private XmlWriterSettings WriterSettings
        {
            get
            {
                return new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    Indent = true,
                    IndentChars = "    ",
                    CloseOutput = true
                };
            }
        }

        #endregion


        public void WriteHtml(string outputFile)
        {
            using (Stream stream = File.Open(outputFile, FileMode.Create))
            {
                using (Writer = XmlWriter.Create(stream, WriterSettings))
                {
                    WriteLibrary();
                }
            }
        }

        private void WriteLibrary()
        {
            WriteHead();
            WriteBody();
            WriteFoot();
        }

        private void WriteHead()
        {
            Writer.WriteDocType("HTML", "-//W3C//DTD XHTML 1.0 Transitional//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd", null);
            Writer.WriteStartElement("html", "http://www.w3.org/1999/xhtml");

            Writer.WriteStartElement("head");
            Writer.WriteElementString("title", "iTunes Music Library");

            Writer.WriteStartElement("style");
            Writer.WriteString(Css);
            Writer.WriteEndElement();

            Writer.WriteEndElement();
        }

        private void WriteBody()
        {
            Writer.WriteStartElement("body");

            var source = Library.Albums.Keys
                .OrderBy(album => album.Artist)
                .ThenBy(album => album.Year)
                .ThenBy(album => album.Name);

            foreach (var album in source)
            {
                WriteAlbum(album);
            }

            Writer.WriteEndElement();
        }

        private void WriteAlbum(Album album)
        {
            if (!String.Equals(album.Artist, LastArtist, StringComparison.CurrentCultureIgnoreCase))
            {
                WriteArtist(album.Artist);
            }

            Writer.WriteStartElement("table");
            Writer.WriteAttributeString("class", "album");
            Writer.WriteAttributeString("cellpadding", "0");
            Writer.WriteAttributeString("cellspacing", "0");

            WriteAlbumHeader(album);

            bool isFirstTrack = true;
            foreach (Track track in Library[album])
            {
                WriteTrack(track, isFirstTrack);
                isFirstTrack = false;
            }

            Writer.WriteEndElement();

            LastArtist = album.Artist;
        }

        private void WriteArtist(string artist)
        {
            Writer.WriteStartElement("div");
            Writer.WriteAttributeString("class", "artist");

            Writer.WriteString(artist);

            Writer.WriteEndElement();
        }

        private void WriteAlbumHeader(Album album)
        {
            Writer.WriteStartElement("tr");
            Writer.WriteStartElement("th");
            Writer.WriteAttributeString("colspan", "4");

            StringBuilder builder = new StringBuilder();
            builder.Append(album.Name);
            if (album.Year > 0)
            {
                builder.Append(String.Format(" ({0})", album.Year));
            }

            Writer.WriteString(builder.ToString());

            Writer.WriteEndElement();
            Writer.WriteEndElement();
        }

        private void WriteTrack(Track track, bool isFirstTrack)
        {
            Writer.WriteStartElement("tr");

            WriteTrackElement(track.TrackNumber, 5, isFirstTrack);
            WriteTrackElement(track.Name, 45, isFirstTrack);
            WriteTrackElement(track.Artist, 35, isFirstTrack);

            if (track.TotalTime > 0)
            {
                var trackTime = TimeSpan.FromMilliseconds(track.TotalTime);
                string timeString = String.Format("{0}:{1:00}", trackTime.Minutes, trackTime.Seconds);

                WriteTrackElement(timeString, 15, isFirstTrack);

            }
            else
            {
                WriteTrackElement(null, 15, isFirstTrack);
            }

            Writer.WriteEndElement();
        }

        private void WriteTrackElement(int element, int width, bool isFirstTrack)
        {
            string stringValue = element == 0 ? XSpace : element.ToString();
            WriteTrackElement(stringValue, width, isFirstTrack);
        }

        private void WriteTrackElement(string element, int width, bool isFirstTrack)
        {
            string value = String.IsNullOrEmpty(element) ? XSpace : element;
            if (!isFirstTrack)
            {
                Writer.WriteElementString("td", value);

            }
            else
            {
                Writer.WriteStartElement("td");
                Writer.WriteAttributeString("width", String.Format("{0}%", width));
                Writer.WriteString(element);
                Writer.WriteEndElement();
            }
        }

        private void WriteFoot()
        {
            Writer.WriteEndElement();
        }
    }


}