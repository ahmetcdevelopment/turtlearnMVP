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
    public class HomeworkMap : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).ValueGeneratedOnAdd();

            builder.Property(h => h.SessionId).IsRequired();

            builder.Property(h => h.Title).IsRequired();
            builder.Property(h => h.Title).HasMaxLength(200);

            builder.Property(h => h.StartDate).IsRequired();
            builder.Property(h => h.EndDate).IsRequired();

            builder.Property(h => h.Description).HasMaxLength(350);

            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(c => c.UpdateUserId).IsRequired();
            builder.Property(c => c.UpdateDate).IsRequired();

            builder.ToTable("Homeworks");
        }
    }
}
