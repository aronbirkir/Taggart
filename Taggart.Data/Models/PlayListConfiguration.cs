namespace Taggart.Data.Models
{
    public partial class PlaylistConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Playlist>
    {
        public PlaylistConfiguration() : this("trx")
        {
        }

        public PlaylistConfiguration(string schema)
        {
            ToTable("playlists");
            HasKey(x => x.PlaylistId);
            Property(x => x.PlaylistId).HasColumnName("id")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.ExternalId).HasColumnName("external_id").IsRequired();
            Property(x => x.LibraryId).HasColumnName("library_id").IsRequired();
            Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(256);

            HasRequired(x => x.Library).WithMany(x => x.Playlists).HasForeignKey(x => x.LibraryId);
        }
    }
}
