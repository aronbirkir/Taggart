namespace Taggart.ITunes
{
    public class Track
    {
        public Track()
        {
            Album = new Album();
        }

        #region Properties

        public Album Album { get; set; }

        public int TrackNumber { get; set; }

        public string Name { get; set; }

        public string Artist { get; set; }

        public int TotalTime { get; set; }

        public int BitRate { get; set; }

        public int SampleRate { get; set; }

        #endregion


        public bool Equals(Track obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.Album, Album) && obj.TrackNumber == TrackNumber && Equals(obj.Name, Name) && Equals(obj.Artist, Artist) && obj.TotalTime == TotalTime && obj.BitRate == BitRate && obj.SampleRate == SampleRate;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Track)) return false;
            return Equals((Track)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Album != null ? Album.GetHashCode() : 0);
                result = (result * 397) ^ TrackNumber;
                result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                result = (result * 397) ^ (Artist != null ? Artist.GetHashCode() : 0);
                result = (result * 397) ^ TotalTime.GetHashCode();
                result = (result * 397) ^ BitRate;
                result = (result * 397) ^ SampleRate;
                return result;
            }
        }
    }
}
