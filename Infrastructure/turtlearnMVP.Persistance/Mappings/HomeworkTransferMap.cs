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
            throw new NotImplementedException();
        }
    }
}
