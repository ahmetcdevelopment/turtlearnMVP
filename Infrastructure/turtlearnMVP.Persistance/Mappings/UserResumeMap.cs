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
    public class UserResumeMap : IEntityTypeConfiguration<UserResume>
    {
        public void Configure(EntityTypeBuilder<UserResume> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.UpdateUserId).IsRequired();
            builder.Property(c => c.UpdateDate).IsRequired();

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ResumeType).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(150);
            builder.Property(x => x.Link).HasMaxLength(100);
            builder.ToTable("UserResumes");
        }
    }
}
