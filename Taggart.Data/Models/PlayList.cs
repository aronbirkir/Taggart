using System.Collections.Generic;

namespace Taggart.Data.Models
{
    public class Playlist
    {
        public Playlist()
        {
            PlaylistTracks = new List<PlaylistTrack>();
        }

        public int PlaylistId { get; set; }
        public int LibraryId { get; set; }

        public int ExternalId { get; set; }
        public string Name { get; set; }
        public virtual Library Library { get; set; }
        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}
