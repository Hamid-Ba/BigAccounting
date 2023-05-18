using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Mapping
{
    public class Dept_CreditorMap : IEntityTypeConfiguration<Dept_Creditor>
    {
        public void Configure(EntityTypeBuilder<Dept_Creditor> builder)
        {
            builder.HasKey(k => new { k.DeptID, k.CreditorID });

            builder.HasOne(d => d.Dept).
                WithMany(dc => dc.Dept_Creditors).
                HasForeignKey(d => d.DeptID);

            builder.HasOne(c => c.Creditor).
                WithMany(dc => dc.Dept_Creditors).
                HasForeignKey(c => c.CreditorID);
        }
    }
}
