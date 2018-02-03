using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SQLite.CodeFirst;
using Taggart.Data.Models;

namespace Taggart.Data
{
    public class TrackLibraryContext : DbContext
    {
        public TrackLibraryContext() : base("TrackLibraryContext")
        {

        }

        public TrackLibraryContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        public TrackLibraryContext(DbConnection connection, bool contextsOwnsConnection) : base(connection, contextsOwnsConnection)
        {

        }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> PlayLists { get; set; }

        public DbSet<TrackMetaData> MetaData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var sqliteConnectionInitializer = new SqliteDropCreateDatabaseWhenModelChanges<TrackLibraryContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new LibraryConfiguration());
            modelBuilder.Configurations.Add(new TrackConfiguration());
            modelBuilder.Configurations.Add(new PlaylistConfiguration());
            modelBuilder.Configurations.Add(new TrackMetaDataConfiguration());
            modelBuilder.Configurations.Add(new PlaylistTrackConfiguration());
            modelBuilder.Configurations.Add(new CuePointConfiguration());
        }
    }
}
