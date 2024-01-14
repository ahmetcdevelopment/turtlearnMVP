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
    public class ChatMap : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.UpdateUserId).IsRequired();
            builder.Property(c => c.UpdateDate).IsRequired();

            builder.Property(x => x.Picture).HasMaxLength(255);
            builder.Property(x=>x.Privacy).IsRequired();
            builder.Property(x=>x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100);

            builder.ToTable("Chats");
        }
    }
}
