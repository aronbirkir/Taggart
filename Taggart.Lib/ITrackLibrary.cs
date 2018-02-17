using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Taggart.RekordBox;

namespace Taggart
{
    public interface ITrackLibrary
    {
        string Name { get; }
        string Version { get; }

        IList<TrackInfo> Tracks { get; }

        IList<PlayListItem> Playlists { get; }
    }

    public class TrackLibraryFactory
    {
        public static ITrackLibrary CreateFromFile(string fileName)
        {
            return new RekordBoxLibrary(fileName);
        }
    }

    public class TrackLibrary : ITrackLibrary
    {
        private IList<TrackInfo> tracks;
        private IList<PlayListItem> playlists;

        public TrackLibrary()
        {
            tracks = new List<TrackInfo>();
            playlists = new List<PlayListItem>();
        }

        public string Name { get; set; }

        public string Version { get; set; }

        public IList<TrackInfo> Tracks => tracks;

        public IList<PlayListItem> Playlists => playlists;
    }





    public class TrackInfo : INotifyPropertyChanged
    {
        bool isDirty;
        CuePoint[] cuePoints;
        Tempo[] tempos;

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

        public Tempo[] Tempos
        {
            get { return tempos; }
        }

        public Dictionary<string, string> Properties
        {
            get; set;
        }
        public bool? FileExists { get; private set; }
        public int? OtherTrackId { get; private set; }

        public void SetFileExists(bool flag, int? trackId)
        {
            FileExists = flag;
            NotifyPropertyChanged(nameof(FileExists));
            if (trackId.HasValue)
            {
                OtherTrackId = trackId.Value;
                NotifyPropertyChanged(nameof(OtherTrackId));
            }
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

        public void SetTempos(ICollection<Tempo> newTempos, bool setDirty = true)
        {
            tempos = newTempos.ToArray();
            if (setDirty)
            {
                isDirty = true;
                NotifyPropertyChanged(nameof(Tempos));
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
                default:
                    Properties.Add(name, value);
                    break;
            }
        }
    }

    public class Tempo
    {
        public int Inizio { get; set; }

        public float Bpm { get; set; }

        public string Metro { get; set; }

        public int Battito { get; set; }
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

    public abstract class PlayListItem
    {
        protected PlayListItem(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }

    public class PlaylistFolder : PlayListItem
    {
        public PlaylistFolder(string name) : base(name)
        {
            Playlists = new List<PlayListItem>();
        }
        public List<PlayListItem> Playlists { get; private set; }

        public override string ToString()
        {
            return $"Folder {Name} ({Playlists.Count} lists) ";
        }
    }

    public class Playlist : PlayListItem
    {
        public Playlist(string name) : base(name)
        {
            TrackIds = new List<int>();
        }
        public List<int> TrackIds { get; set; }

        public override string ToString()
        {
            return $"Playlist {Name} ({TrackIds.Count} tracks) ";
        }
    }
}
