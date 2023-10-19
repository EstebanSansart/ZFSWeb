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

            builder.HasOne(x => x.Event)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.EventoId);
            

            builder.HasData
            (
                new[]
              {
        new
        {
            ImageId = 1,
            Url = "https://www.tooltyp.com/wp-content/uploads/2014/10/1900x920-8-beneficios-de-usar-imagenes-en-nuestros-sitios-web.jpg",
            EventoId = 1
        },
        new
        {
            ImageId = 2,
            Url = "https://images.ctfassets.net/hrltx12pl8hq/5KiKmVEsCQPMNrbOE6w0Ot/341c573752bf35cb969e21fcd279d3f9/hero-img_copy.jpg?fit=fill&w=600&h=400",
            EventoId=1
        },
        // Agrega más objetos aquí según sea necesario
    });
            

        }
    }
}