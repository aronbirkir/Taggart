namespace Taggart.Data.Models
{
    public class TrackMetaData
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual Track Track { get; set; }
    }
}
