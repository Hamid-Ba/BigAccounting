using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Mapping
{
    public class Dept_DeptorMap : IEntityTypeConfiguration<Dept_Deptor>
    {
        public void Configure(EntityTypeBuilder<Dept_Deptor> builder)
        {
            builder.HasKey(k => new { k.DeptID, k.DeptorID });

            builder.HasOne(d => d.Dept).
                WithMany(dd => dd.Dept_Deptors).
                HasForeignKey(d => d.DeptID);

            builder.HasOne(d => d.Deptor).
                WithMany(dd => dd.Dept_Deptors).
                HasForeignKey(d => d.DeptorID);
        }
    }
}
