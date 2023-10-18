using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("image");

            builder.HasKey(r => r.ImageId);

            // Properties
            builder.Property(r => r.ImageId)
            .IsRequired()
            .HasColumnName("image_id");

            builder.Property(r => r.Url)
            .IsRequired()
            .HasColumnName("image_url");

            builder.HasData
            (
                new
                {
                 ImageId =1,
                 Url ="https://www.tooltyp.com/wp-content/uploads/2014/10/1900x920-8-beneficios-de-usar-imagenes-en-nuestros-sitios-web.jpg "
                }

            );
        }
    }
}