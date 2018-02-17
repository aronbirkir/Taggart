using System.Collections.Generic;
using System.Linq;
using Taggart.Data.Models;

namespace Taggart.Data
{
    public class DataAccess
    {
        public static ICollection<LibraryInfo> GetLibrariesInfo()
        {
            using (var db = new TrackLibraryContext())
            {
                var q = from l in db.Libraries
                        join t in db.Tracks on l.LibraryId equals t.LibraryId into tJoin
                        from t in tJoin.DefaultIfEmpty()
                        group t by l into grp
                        select new LibraryInfo
                        {
                            LibraryId = grp.Key.LibraryId,
                            Name = grp.Key.Name,
                            File = grp.Key.File,
                            TrackCount = grp.Count()
                        };
                return q.ToList();
            }
        }

        public static IEnumerable<TrackInfo> GetLibraryTracks(int libraryId)
        {
            using (var db = new TrackLibraryContext())
            {
                var q = from t in db.Tracks
                        where t.LibraryId == libraryId
                        orderby t.TrackId
                        select new TrackInfo
                        {

                        };
                return q.ToList();
            }
        }
    }
}
