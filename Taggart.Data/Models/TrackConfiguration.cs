namespace Taggart.Data.Models
{
    public partial class TrackConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Track>
    {
        public TrackConfiguration() : this("trx")
        {
        }

        public TrackConfiguration(string schema)
        {
            ToTable("tracks");
            HasKey(x => x.TrackId);
            Property(x => x.TrackId).HasColumnName("id")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.LibraryId).HasColumnName("library_id").IsRequired();
            Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(256);
            Property(x => x.Artist).HasColumnName("artist").IsOptional().HasMaxLength(256);
            Property(x => x.ExternalId).HasColumnName("external_id").IsRequired();
            Property(x => x.Bpm).HasColumnName("bpm").IsOptional();
            Property(x => x.Key).HasColumnName("key").IsOptional();
            Property(x => x.Comments).HasColumnName("comments").IsOptional();
            Property(x => x.Location).HasColumnName("location").IsRequired().HasMaxLength(256);


            HasRequired(x => x.Library).WithMany(x => x.Tracks).HasForeignKey(x => x.LibraryId);
        }
    }
}
