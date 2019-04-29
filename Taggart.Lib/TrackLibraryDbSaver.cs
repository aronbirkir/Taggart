using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Taggart.Data;

namespace Taggart
{
    public static class TrackLibraryDbSaver
    {
        private static Regex keyPattern = new Regex(@"^([0-1]?[0-9][AB]\/[0-1]?[0-9][AB]|[0-1]?[0-9][AB])(?:\s*-\s*)?(Energy\s+)?(\d+)?(.*)?", RegexOptions.Compiled);

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
                    chunkCount = trackChunk.Length;
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
                                Artist = track.Artist,
                                Location = track.FileName,
                                Comments = track.Comments,
                                Key = track.Key,
                                Bpm = track.Bpm ?? 0

                            };
                            trackRecord.MetaData = track.Properties.Select(x => new Data.Models.TrackMetaData
                            {
                                Name = x.Key,
                                Value = x.Value
                            }).ToList();


                            trackRecord.CuePoints = track.CuePoints?.Select(x => new Data.Models.CuePoint
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
