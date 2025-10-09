﻿using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    public class MemberConfiguration : GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.CreatedAt)
                    .HasColumnName("JoinDate")
                    .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.HealthRecord)
                    .WithOne()
                    .HasForeignKey<HealthRecord>(x => x.Id);

            base.Configure(builder);
        }
    }
}
