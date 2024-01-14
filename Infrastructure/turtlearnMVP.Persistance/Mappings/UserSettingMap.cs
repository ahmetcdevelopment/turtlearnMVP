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
    public class UserSettingMap : IEntityTypeConfiguration<UserSetting>
    {
        public void Configure(EntityTypeBuilder<UserSetting> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.UpdateUserId).IsRequired();
            builder.Property(c => c.UpdateDate).IsRequired();

            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.TypeId).IsRequired();
            builder.Property(c => c.Key).IsRequired();
            builder.Property(c => c.Value).IsRequired();
            builder.Property(c => c.Value).HasMaxLength(250);

            builder.ToTable("UserSettings");
        }
    }
}
