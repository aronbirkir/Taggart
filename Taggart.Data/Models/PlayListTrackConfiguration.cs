namespace Taggart.Data.Models
{
    public class PlaylistTrackConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PlaylistTrack>
    {
        public PlaylistTrackConfiguration() : this("trx")
        {
        }

        public PlaylistTrackConfiguration(string schema)
        {
            ToTable("playlist_tracks");
            HasKey(x => new { x.PlayListId, x.TrackId });

            Property(x => x.PlayListId).HasColumnName("playlist_id").IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.TrackId).HasColumnName("track_id").IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None); ;
            Property(x => x.Ordinal).HasColumnName("ordinal").IsRequired();


            HasRequired(x => x.Playlist).WithMany(x => x.PlaylistTracks).HasForeignKey(x => x.PlayListId);
            HasRequired(x => x.Track).WithMany(x => x.PlaylistTracks).HasForeignKey(x => x.TrackId);
        }
    }
}
