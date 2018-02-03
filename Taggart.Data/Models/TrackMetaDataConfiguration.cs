namespace Taggart.Data.Models
{
    public partial class TrackMetaDataConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TrackMetaData>
    {
        public TrackMetaDataConfiguration() : this("trx")
        {
        }

        public TrackMetaDataConfiguration(string schema)
        {
            ToTable("track_meta_data");
            HasKey(t => new { t.TrackId, t.Name });
            Property(x => x.TrackId).HasColumnName("track_id")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(256);
            Property(x => x.Value).HasColumnName("value").IsRequired().HasMaxLength(256);

            HasRequired(x => x.Track).WithMany(x => x.MetaData).HasForeignKey(x => x.TrackId);
        }
    }
}
