namespace Taggart.Data.Models
{

    public partial class LibraryConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Library>
    {
        public LibraryConfiguration() : this("trx")
        {
        }

        public LibraryConfiguration(string schema)
        {
            ToTable("libraries");
            HasKey(x => x.LibraryId);

            Property(x => x.LibraryId)
                .HasColumnName("id")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
               .HasColumnName("name")
               .IsRequired()
               .HasMaxLength(200);


            Property(x => x.Version)
               .HasColumnName("version")
               .IsOptional();

            Property(x => x.File)
               .HasColumnName("file")
               .IsRequired();
        }
    }
}
