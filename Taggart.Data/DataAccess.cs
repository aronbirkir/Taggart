using System.Collections.Generic;
using System.Linq;
using Taggart.Data.Models;

namespace Taggart.Data
{
    public class DataAccess
    {
        public static ICollection<LibraryInfo> GetLibrariesInfo(TrackLibraryContext db)
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

        public static IEnumerable<TrackInfo> GetLibraryTracks(TrackLibraryContext db, int libraryId)
        {
            var q = from t in db.Tracks
                    where t.LibraryId == libraryId
                    orderby t.TrackId
                    select new TrackInfo
                    {
                        TrackId = t.TrackId,
                        LibraryId = t.LibraryId,
                        Name = t.Name,
                        Artist = t.Artist,
                        Album = t.Album,
                        Key = t.Key,
                        Bpm = t.Bpm,
                        Comments = t.Comments,
                        ExternalId = t.ExternalId,
                        Location = t.Location
                    };
            return q.ToList();

        }

        public static void DeleteLibrary(TrackLibraryContext db, int libraryId)
        {
            // Remove tracks
            var lib = db.Libraries.FirstOrDefault(x => x.LibraryId == libraryId);
            db.Libraries.Remove(lib);
            db.SaveChanges();
        }
    }
}
