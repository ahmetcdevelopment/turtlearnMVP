using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Mappings;

public class OrderDetailMap : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        #region HER MAP SINIFINDA OLMALI
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(c => c.UpdateUserId).IsRequired();
        builder.Property(c => c.UpdateDate).IsRequired();
        #endregion
        // İndirim kodu şimdilik 20 haneli olarak belirlendi daha sonrasnda değiştirilebilir.
        builder.Property(x=>x.OfferCode).HasMaxLength(20);
        builder.Property(x=>x.CourseId).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x=>x.Currency).IsRequired();
        builder.Property(x=>x.Amount).IsRequired();
        builder.Property(x=>x.DiscountRate).IsRequired();
        //Kursun ismi
        builder.Property(x=>x.Name).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x=>x.TaxPrice).IsRequired();
        builder.Property(x => x.TaxRate).IsRequired();
        builder.ToTable("OrderDetails");
    }
}
