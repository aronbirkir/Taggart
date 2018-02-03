using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace Taggart
{
    public interface ITrackLibrary
    {
        string Name { get; }
        string Version { get; }

        IList<TrackInfo> Tracks { get; }

        IList<PlayListInfo> Playlists { get; }
    }

    public class TrackLibraryFactory
    {
        public static ITrackLibrary CreateFromFile(string fileName)
        {
            return new RekordBoxLibrary(fileName);
        }
    }

    public class RekordBoxLibrary : ITrackLibrary
    {
        private IList<TrackInfo> tracks;
        private IList<PlayListInfo> playlists;

        public RekordBoxLibrary(string fileName)
        {
            tracks = new List<TrackInfo>();
            playlists = new List<PlayListInfo>();
            using (var reader = XmlReader.Create(fileName))
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
                    var track = new TrackInfo();
                    if (reader.MoveToFirstAttribute())
                    {
                        do
                        {
                            track.SetProperty(reader.Name, reader.Value);

                        } while (reader.MoveToNextAttribute());

                        tracks.Add(track);
                    }
                    reader.MoveToContent();
                    if (reader.ReadToDescendant("TEMPO"))
                    {

                    }
                    var cuePoints = new List<CuePoint>();
                    while (reader.ReadToNextSibling("POSITION_MARK"))
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
                    if (cuePoints.Count > 0)
                    {
                        track.SetCuePoints(cuePoints.ToArray(), false);
                    }
                } while (reader.ReadToNextSibling("TRACK"));
            }
        }


        public string Name { get; set; }

        public string Version { get; set; }

        public IList<TrackInfo> Tracks => tracks;

        public IList<PlayListInfo> Playlists => playlists;


    }

    public class TrackInfo : INotifyPropertyChanged
    {
        bool isDirty;
        CuePoint[] cuePoints;

        public TrackInfo()
        {
            Properties = new Dictionary<string, string>();
        }

        public int TrackId { get; set; }

        public string Name { get; set; }

        public string Artist { get; set; }

        public long Length { get; set; }

        public string FileName { get; set; }

        public CuePoint[] CuePoints
        {
            get { return cuePoints; }
        }

        public Dictionary<string, string> Properties
        {
            get; set;
        }

        public void SetCuePoints(ICollection<CuePoint> newCuePoints, bool setDirty = true)
        {
            cuePoints = newCuePoints.ToArray();
            if (setDirty)
            {
                isDirty = true;
                NotifyPropertyChanged(nameof(CuePoints));
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        public bool IsDirty()
        {
            return isDirty;
        }

        internal void SetProperty(string name, string value)
        {
            switch (name)
            {
                case "TrackID": TrackId = int.Parse(value); break;
                case "Name": Name = value; break;
                case "Artist": Artist = value; break;
                case "Size": Length = long.Parse(value); break;
                case "Location": FileName = Uri.UnescapeDataString(value.Replace(@"file://localhost/", "")); break;
            }
            Properties.Add(name, value);
        }
    }

    public class CuePoint
    {
        public string Name { get; set; }

        public int Time { get; set; }

        public int Number { get; set; }
    }

    public class TrackField<T>
    {

    }

    public class PlayListInfo
    {

    }

}
