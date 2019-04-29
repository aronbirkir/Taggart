using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Taggart.RekordBox
{
    public class RekordBoxLibrary : TrackLibrary
    {


        public RekordBoxLibrary(string fileName)
        {
            using (var reader = XmlReader.Create(fileName))
            {
                ReadLibrary(reader);
            }
        }

        public RekordBoxLibrary(TextReader libraryReader)
        {
            using (var reader = XmlReader.Create(libraryReader))
            {
                ReadLibrary(reader);
            }
        }

        private void ReadLibrary(XmlReader reader)
        {
            reader.ReadToFollowing("PRODUCT");
            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "Name": Name = reader.Value; break;
                    case "Version": Version = reader.Value; break;
                }
            }
            reader.ReadToFollowing("COLLECTION");
            if (reader.ReadToDescendant("TRACK"))
            {
                do
                {
                    using (var trackReader = reader.ReadSubtree())
                    {
                        Tracks.Add(ReadTrack(trackReader));
                    }
                } while (reader.ReadToNextSibling("TRACK"));
            }
            reader.ReadToFollowing("PLAYLISTS");
            if (reader.ReadToDescendant("NODE"))
            {
                do
                {
                    using (var playlistNodeReader = reader.ReadSubtree())
                    {
                        Playlists.Add(ReadPlaylistNode(playlistNodeReader));
                    }
                } while (reader.ReadToNextSibling("NODE"));
            }
        }

        private PlayListItem ReadPlaylistNode(XmlReader reader)
        {
            reader.Read();
            if (reader.MoveToFirstAttribute())
            {
                var entries = 0;
                var type = 0;
                string playListName = string.Empty;
                do
                {
                    switch (reader.Name)
                    {
                        case "Name": playListName = reader.Value; break;
                        case "Type": type = int.Parse(reader.Value); break;
                        case "Entries": entries = int.Parse(reader.Value); break;
                        case "Count": entries = int.Parse(reader.Value); break;
                        default: break; // do nothing
                    }
                } while (reader.MoveToNextAttribute());
                reader.MoveToContent();
                if (type == 0) // folder
                {
                    var playlistFolder = new PlaylistFolder(playListName);
                    using (var nodeReader = reader.ReadSubtree())
                    {
                        if (reader.ReadToDescendant("NODE"))
                        {
                            do
                            {
                                using (var playlistNodeReader = reader.ReadSubtree())
                                {
                                    playlistFolder.Playlists.Add(ReadPlaylistNode(playlistNodeReader));
                                }
                            } while (reader.ReadToNextSibling("NODE"));
                        }
                    }
                    return playlistFolder;
                }
                else if (type == 1) // playlist with tracks
                {
                    var playlist = new Playlist(playListName);
                    reader.ReadToDescendant("TRACK");
                    do
                    {
                        var trackId = 0;
                        if (reader.MoveToFirstAttribute())
                        {
                            do
                            {
                                switch (reader.Name)
                                {
                                    case "Key": trackId = int.Parse(reader.Value); break;
                                    default: break;
                                }
                            } while (reader.MoveToNextAttribute());
                        }
                        if (trackId > 0)
                        {
                            playlist.TrackIds.Add(trackId);
                        }
                    } while (reader.ReadToNextSibling("TRACK"));
                    return playlist;
                }
            }
            return null;
        }

        private TrackInfo ReadTrack(XmlReader reader)
        {
            reader.Read();
            var track = new TrackInfo();
            if (reader.MoveToFirstAttribute())
            {
                do
                {
                    track.SetProperty(reader.Name, reader.Value);

                } while (reader.MoveToNextAttribute());
            }

            var tempos = new List<Tempo>();
            var cuePoints = new List<CuePoint>();
            do
            {
                if (reader.Name == "TEMPO")
                {
                    var tempo = new Tempo();
                    while (reader.MoveToNextAttribute())
                    {
                        switch (reader.Name)
                        {
                            case "Inizio": tempo.Inizio = (int)(float.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture) * 1000); break;
                            case "Bpm": tempo.Bpm = float.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture); break;
                            case "Metro": tempo.Metro = reader.Value; break;
                            case "Battito": tempo.Battito = int.Parse(reader.Value); break;
                        }
                    }
                    tempos.Add(tempo);
                }
                else if (reader.Name == "POSITION_MARK")
                {
                    var cuePoint = new CuePoint();
                    while (reader.MoveToNextAttribute())
                    {
                        switch (reader.Name)
                        {
                            case "Name": cuePoint.Name = reader.Value; break;
                            case "Start": cuePoint.Time = (int)(float.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture) * 1000); break;
                            case "Num": cuePoint.Number = int.Parse(reader.Value); break;
                        }
                    }
                    cuePoints.Add(cuePoint);
                }

            } while (reader.Read());

            if (tempos.Count > 0)
            {
                track.SetTempos(tempos.ToArray(), false);
            }
            if (cuePoints.Count > 0)
            {
                track.SetCuePoints(cuePoints.ToArray(), false);
            }
            return track;
        }
    }
}
