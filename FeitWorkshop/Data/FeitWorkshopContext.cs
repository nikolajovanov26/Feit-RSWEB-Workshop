using FeitWorkshop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeitWorkshop.Data
{
    public class FeitWorkshopContext : DbContext
    {
        public FeitWorkshopContext(DbContextOptions<FeitWorkshopContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");



            modelBuilder.Entity<Enrollment>()
                .HasOne<Student>(p => p.Student)
                .WithMany(p => p.Courses)
                .HasForeignKey(p => p.StudentId);


            modelBuilder.Entity<Enrollment>()
                .HasOne<Course>(d => d.Course)
                .WithMany(d => d.Students)
                .HasForeignKey(d => d.CourseId);

            modelBuilder.Entity<Course>()
                .HasOne<Teacher>(p => p.FirstTeacher)
                .WithMany(p => p.FirstTeacher)
                .HasForeignKey(p => p.FirstTeacherId);

            modelBuilder.Entity<Course>()
               .HasOne<Teacher>(p => p.SecondTeacher)
               .WithMany(p => p.SecondTeacher)
               .HasForeignKey(p => p.SecondTeacherId);


        }
    }
}