using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Mappings;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        #region HER MAP SINIFINDA OLMALI
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(c => c.UpdateUserId).IsRequired();
        builder.Property(c => c.UpdateDate).IsRequired();
        #endregion

        builder.Property(x=>x.Status).IsRequired();
        builder.Property(x=>x.UserId).IsRequired();
        builder.Property(x=>x.AmountPaid).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x=>x.Email).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.Property(x => x.PhoneNumber).HasMaxLength(16);
        builder.Property(x => x.EmailConfirmCode).IsRequired();
        // Mail yoluyla gönderilecek doğrulama kodu 6 haneli olmalıdır.
        builder.Property(x => x.EmailConfirmCode).HasMaxLength(6);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x=>x.LastName).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.ToTable("Orders");
    }
}
