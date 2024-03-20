using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Mappings;

public class OfferMap : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        #region HER MAP SINIFINDA OLMALI
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(c => c.UpdateUserId).IsRequired();
        builder.Property(c => c.UpdateDate).IsRequired();
        #endregion

        builder.Property(x=>x.Type).IsRequired();
        builder.Property(x=>x.Code).IsRequired();
        //Code guid olarak random atanacak ve 20 haneli olacak.
        builder.Property(x => x.Code).HasMaxLength(20);
        builder.Property(x=>x.Name).IsRequired();
        builder.Property(x=>x.Name).HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(300);
        builder.Property(x=>x.DiscountRate).IsRequired();
        builder.ToTable("Offers");
    }
}
