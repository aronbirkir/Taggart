namespace Taggart.Data.Models
{
    public class TrackInfo
    {
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
    }
}
