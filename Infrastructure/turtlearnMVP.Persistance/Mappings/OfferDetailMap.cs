using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Mappings;

public class OfferDetailMap : IEntityTypeConfiguration<OfferDetail>
{
    public void Configure(EntityTypeBuilder<OfferDetail> builder)
    {
        #region HER MAP SINIFINDA OLMALI
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(c => c.UpdateUserId).IsRequired();
        builder.Property(c => c.UpdateDate).IsRequired();
        #endregion

        builder.Property(x=>x.OfferId).IsRequired();
        builder.Property(x=>x.CourseId).IsRequired();
        builder.Property(x => x.DefinitionDate).IsRequired();
        builder.Property(x=>x.EndDate).IsRequired();
        builder.ToTable("OfferDetails");
    }
}
