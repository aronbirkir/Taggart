using System.Collections.Generic;

namespace Taggart.Data.Models
{
    public class Library
    {
        public Library()
        {
            Tracks = new List<Track>();
            Playlists = new List<Playlist>();
        }
        public int LibraryId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string File { get; set; }

        public ICollection<Track> Tracks { get; set; }

        public ICollection<Playlist> Playlists { get; set; }
    }
}
