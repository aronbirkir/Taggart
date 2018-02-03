namespace Taggart.Data.Models
{
    public class PlaylistTrack
    {
        public int PlayListId { get; set; }
        public int TrackId { get; set; }
        public int Ordinal { get; set; }

        public virtual Track Track{ get; set; }

        public virtual Playlist Playlist { get; set; }

    }
}
