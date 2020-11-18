using EmployeeManagement.Data.EF.Interfaces;
using EmployeeManagement.Data.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.EF
{
   public  class EmployeeContext : DbContext
    {

        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(x => x.ID);
                entity.Property(x => x.ID).ValueGeneratedOnAdd();
                entity.HasOne(x => x.Manager).WithMany(x => x.Reportees).HasForeignKey(x => x.ManagerID);
                entity.HasOne(x => x.Department);

            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(x => x.ID);
                entity.Property(x => x.ID).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Department>().HasData(
            new Department
            {
                ID=1,
                Name = "HR"
            }, new Department
            {
                ID = 2,
                Name = "Admin"
            }, new Department
            {
                ID = 3,
                Name = "GE"
            });
            modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                ID = 1,
                FirstName = "Vineeth",
                LastName = "Viswanathan",
                DepartmentID = 1,
                DOJ = DateTime.Now,
                Address = "coimbatore"
            },
             new Employee
             {
                 ID = 2,
                 FirstName = "Krishna",
                 LastName = "v",
                 DepartmentID = 1,
                 DOJ = DateTime.Now,
                 ManagerID = 1,
                 Address = "coimbatore"
             },
               new Employee
               {
                   ID = 3,
                   FirstName = "Krishna",
                   LastName = "v",
                   DepartmentID = 1,
                   DOJ = DateTime.Now,
                   ManagerID = 2,
                   Address = "coimbatore"
               },
                 new Employee
                 {
                     ID = 4,
                     FirstName = "Krishna",
                     LastName = "v",
                     DepartmentID = 2,
                     DOJ = DateTime.Now,
                     Address = "coimbatore"
                 }
        );
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            BindAuditColumns();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            BindAuditColumns();
            return base.SaveChanges();
        }
        private void BindAuditColumns()
        {
            var addedEntryCollection = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

            // Get modified entries
            var modifiedEntryCollection = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            if (addedEntryCollection.Any() || modifiedEntryCollection.Any())
            {
                var userid = "1234";
                // Set audit fields of added entries
                foreach (var entry in addedEntryCollection)
                {

                    if (entry.Entity is IAuditableEntity entity)
                    {

                        entry.CurrentValues[nameof(IAuditableEntity.CreateBy)] = userid;
                        entry.CurrentValues[nameof(IAuditableEntity.CreateDate)] = DateTime.Now;

                    }
                }

                // Set audit fields of modified entries
                foreach (var entry in modifiedEntryCollection)
                {

                    if (entry.Entity is IAuditableEntity entity)
                    {

                        entry.CurrentValues[nameof(IAuditableEntity.UpdateBy)] = userid;
                        entry.CurrentValues[nameof(IAuditableEntity.UpdateDate)] = DateTime.Now;
                    }

                }
            }

        }

    }
}

