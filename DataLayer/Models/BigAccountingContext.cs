using DataLayer.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class BigAccountingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server = .;DataBase = BigAccountingDB;Trusted_Connection = True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Dept_DeptorMap());
            modelBuilder.ApplyConfiguration(new Dept_CreditorMap());
        }

        public DbSet<Dept> Depts { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Deptor> Deptors { get; set; }
        public DbSet<Creditor> Creditors { get; set; }
        public DbSet<Dept_Deptor> Dept_Deptors { get; set; }
        public DbSet<Dept_Creditor> Dept_Creditors { get; set; }
    }
}
