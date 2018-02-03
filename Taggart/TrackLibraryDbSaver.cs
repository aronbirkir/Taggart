using System.Collections.Generic;
using System.Linq;
using Taggart.Data;

namespace Taggart
{
    public static class TrackLibraryDbSaver
    {
        public static void Save(string fileName, ITrackLibrary library)
        {
            using (var db = new TrackLibraryContext())
            {
                var libraryRecord = db.Libraries.Where(x => x.File == fileName).FirstOrDefault();
                if (libraryRecord == null)
                {
                    libraryRecord = new Data.Models.Library
                    {
                        Name = library.Name,
                        File = fileName,
                        Version = library.Version
                    };
                    db.Libraries.Add(libraryRecord);
                    db.SaveChanges();
                }

                var chunkSize = 100;
                var chunkNumber = 0;
                var chunkCount = 0;
                do
                {
                    var trackChunk = library.Tracks.Skip(chunkSize * chunkNumber++).Take(chunkSize).ToArray();
                    var trackIds = trackChunk.Select(x => x.TrackId).ToArray();
                    var newTrackRecords = new List<Data.Models.Track>();
                    var trackRecords = db.Tracks.Where(x => x.LibraryId == libraryRecord.LibraryId && trackIds.Contains(x.ExternalId)).ToDictionary(x => x.ExternalId);
                    foreach (var track in trackChunk)
                    {
                        if (!trackRecords.ContainsKey(track.TrackId))
                        {

                            var trackRecord = new Data.Models.Track
                            {
                                LibraryId = libraryRecord.LibraryId,
                                ExternalId = track.TrackId,
                                Name = track.Name,
                                Location = track.FileName,
                                //Key = track.ke
                                Artist = track.Artist
                            };
                            trackRecord.MetaData = track.Properties.Select(x => new Data.Models.TrackMetaData
                            {
                                Name = x.Key,
                                Value = x.Value
                            }).ToList();

                            trackRecord.CuePoints = track.CuePoints.Select(x => new Data.Models.CuePoint
                            {
                                Number = x.Number,
                                StartTime = x.Time
                            }).ToList();

                            newTrackRecords.Add(trackRecord);
                        }
                        else
                        {
                            // Update track
                        }
                    }

                    db.Tracks.AddRange(newTrackRecords);
                    db.SaveChanges();

                } while (chunkCount == chunkSize);
            }
        }
    }
}
