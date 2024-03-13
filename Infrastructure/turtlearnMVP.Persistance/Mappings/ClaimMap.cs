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
    public class ClaimMap : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.UpdateUserId).IsRequired();
            builder.Property(c => c.UpdateDate).IsRequired();

            builder.Property(c => c.ClaimTypeId).IsRequired();
            builder.Property(c => c.ClaimValue).IsRequired();
            builder.Property(c => c.ClaimValue).HasMaxLength(50);
          
        }
    }
}
