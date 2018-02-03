namespace Taggart.Data.Models
{
    public class CuePoint
    {
        public int TrackId { get; set; }

        public int Number { get; set; }

        public float StartTime { get; set; }

        public virtual Track Track { get; set; }
    }
}
