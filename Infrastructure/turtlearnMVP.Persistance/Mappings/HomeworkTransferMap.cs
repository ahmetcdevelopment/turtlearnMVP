using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Mappings
{
    public class HomeworkTransferMap : IEntityTypeConfiguration<HomeworkTransfer>
    {
        public void Configure(EntityTypeBuilder<HomeworkTransfer> builder)
        {
            builder.HasKey(ht => ht.Id);
            builder.Property(ht => ht.Id).ValueGeneratedOnAdd();

            builder.Property(ht => ht.HomeworkId).IsRequired();

            builder.Property(ht => ht.StudentId).IsRequired();

            builder.Property(ht => ht.Status).IsRequired();

            builder.Property(ht => ht.Attachment).IsRequired(); //isrequired olmalı mı?

            builder.Property(ht => ht.TransferDate).IsRequired();

            builder.Property(h => h.Description).HasMaxLength(350);

            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(c => c.UpdateUserId).IsRequired();
            builder.Property(c => c.UpdateDate).IsRequired();

            builder.ToTable("HomeworkTransfers");
        }
    }
}
