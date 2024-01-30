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
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.TableId).IsRequired();
            builder.Property(c => c.RecordId).IsRequired();

            builder.Property(c => c.StudentId).IsRequired();

            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(250);

            builder.Property(c => c.Rating).IsRequired();

            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(c => c.UpdateUserId).IsRequired();

            builder.Property(c => c.UpdateDate).IsRequired();


            builder.ToTable("Comments");
        }
    }
}
