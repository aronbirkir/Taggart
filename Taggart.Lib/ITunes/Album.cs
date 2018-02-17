using System;

namespace Taggart.ITunes
{
    public class Album : IComparable<Album>
    {
        #region Properties

        public string Name { get; set; }

        public string Artist { get; set; }

        public int Year { get; set; }

        #endregion


        public int CompareTo(Album other)
        {
            int result = String.Compare(Artist, other.Artist, StringComparison.CurrentCultureIgnoreCase);

            if (result == 0)
            {
                result = String.Compare(Name, other.Name, StringComparison.CurrentCultureIgnoreCase);

                if (result == 0)
                {
                    result = Year.CompareTo(other.Year);
                }
            }

            return result;
        }

        public bool Equals(Album obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.Name, Name) && Equals(obj.Artist, Artist) && obj.Year == Year;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Album)) return false;
            return Equals((Album)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Name != null ? Name.GetHashCode() : 0);
                result = (result * 397) ^ (Artist != null ? Artist.GetHashCode() : 0);
                result = (result * 397) ^ Year;
                return result;
            }
        }
    }
}
