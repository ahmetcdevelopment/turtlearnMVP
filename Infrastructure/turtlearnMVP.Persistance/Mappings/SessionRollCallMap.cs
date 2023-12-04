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
    public class SessionRollCallMap : IEntityTypeConfiguration<SessionRollCall>
    {
        public void Configure(EntityTypeBuilder<SessionRollCall> builder)
        {
            builder.HasKey(src => src.Id);
            builder.Property(src => src.Id).ValueGeneratedOnAdd();

            builder.Property(src => src.CourseId).IsRequired();
            builder.Property(src => src.UserId).IsRequired();
            builder.Property(src => src.TimeToJoin).IsRequired();
            builder.Property(src => src.SessionId).IsRequired();

            builder.Property(src => src.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(src=>src.UpdateUserId).IsRequired();
            builder.Property(src => src.UpdateDate).IsRequired();

            builder.ToTable("SessionRollCalls");
        }
    }
}
