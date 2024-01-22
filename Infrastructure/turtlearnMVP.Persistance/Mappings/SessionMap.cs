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
    public class SessionMap : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.CourseId).IsRequired();

            builder.Property(s => s.Queue).IsRequired();
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Name).HasMaxLength(150);

            builder.Property(s => s.StartDate).IsRequired();

            builder.Property(s => s.Link).IsRequired();//str mi link mi olmalı?

            builder.Property(s => s.Description).HasMaxLength(350);

            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(c => c.UpdateUserId).IsRequired();
            builder.Property(c => c.UpdateDate).IsRequired();

            builder.ToTable("Sessions");
        }
    }
}
