using System.Collections.Generic;

namespace Taggart.Data.Models
{
    public class Track
    {
        public Track()
        {
            PlaylistTracks = new List<PlaylistTrack>();
        }

        public int TrackId { get; set; }

        public int LibraryId { get; set; }

        public int ExternalId { get; set; }

        public string Name { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public float Bpm { get; set; }

        public string Key { get; set; }

        public string Comments { get; set; }

        public string Location { get; set; }

        public virtual Library Library { get; set; }
        public virtual ICollection<TrackMetaData> MetaData { get; set; }

        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }

        public virtual ICollection<CuePoint> CuePoints { get; set; }
    }
}
