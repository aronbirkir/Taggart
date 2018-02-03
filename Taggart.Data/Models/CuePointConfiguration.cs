namespace Taggart.Data.Models
{
    public class CuePointConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<CuePoint>
    {
        public CuePointConfiguration() : this("trx")
        {
        }

        public CuePointConfiguration(string schema)
        {
            ToTable("cuepoints");
            HasKey(x => new { x.TrackId, x.Number });

            Property(x => x.TrackId).HasColumnName("track_id").IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Number).HasColumnName("number").IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.StartTime).IsRequired();

            HasRequired(x => x.Track).WithMany(x => x.CuePoints).HasForeignKey(x => x.TrackId);
        }
    }
}
