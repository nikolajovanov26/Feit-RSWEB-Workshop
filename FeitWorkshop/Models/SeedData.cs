using FeitWorkshop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeitWorkshop.Models
{
    public class SeedData
    {
        /*
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<FeitWorkshopUser>>();

            IdentityResult roleResult;
            //Add admin role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }

            //Add Student / teaacher role
            var studentRole = await RoleManager.RoleExistsAsync("Student");
            if (!studentRole) { await RoleManager.CreateAsync(new IdentityRole("Student")); }
            var teacherRole = await RoleManager.RoleExistsAsync("Teacher");
            if (!teacherRole) { await RoleManager.CreateAsync(new IdentityRole("Teacher")); }

            FeitWorkshopUser user = await UserManager.FindByEmailAsync("admin@feit.com");
            if (user == null)
            {
                var User = new FeitWorkshopUser();
                User.Email = "admin@feit.com";
                User.UserName = "admin@feit.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //add default user to role admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
        }
        /*
        /*
        public static void Initialize(FeitWorkshopContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{StudentId="185/2017",FirstName="Nikola", LastName="Jovanov", AcquiredCredits=10, CurrentSemestar=8},
            new Student{StudentId="60/2017",FirstName="Borce", LastName="Jovanov", AcquiredCredits=10, CurrentSemestar=8},
            new Student{StudentId="165/2019",FirstName="Aleksandar", LastName="Karamanov", AcquiredCredits=10, CurrentSemestar=8}
            };
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{Title="RSWEB", Credits=6, Semester = 6},
            new Course{Title="WebApp", Credits=6, Semester = 6},
            new Course{Title="M3", Credits=6, Semester = 3}
            };
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentId=1,CourseId=1,Semester=6,Year=3,Grade=7 },
            new Enrollment{StudentId=2,CourseId=1,Semester=6,Year=3,Grade=7},
            new Enrollment{StudentId=3,CourseId=1,Semester=6,Year=3,Grade=7}
            };
            context.SaveChanges();

            var teachers = new Teacher[]
            {
            new Teacher{FirstName="Ilco", LastName="Stojanovski", HireDate=DateTime.Parse("2016-09-15")},
            new Teacher{FirstName="Grozdan", LastName="Vckov", HireDate=DateTime.Parse("2016-09-15")}
            };
            context.SaveChanges();

            internal static void Initialize(IServiceProvider services)
        {
            throw new NotImplementedException();
        }
        }
        */
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FeitWorkshopContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<FeitWorkshopContext>>()))
            {
                //CreateUserRoles(serviceProvider).Wait();

                if (context.Students.Any() || context.Teachers.Any() || context.Courses.Any())
                {
                    return;
                }

                context.Students.AddRange(
                    new Student { StudentId = "185/2017", FirstName = "Nikola", LastName = "Jovanov", AcquiredCredits = 10, CurrentSemestar = 8 },
                    new Student { StudentId = "60/2017", FirstName = "Borce", LastName = "Jovanov", AcquiredCredits = 10, CurrentSemestar = 8 },
                    new Student { StudentId = "165/2019", FirstName = "Aleksandar", LastName = "Karamanov", AcquiredCredits = 10, CurrentSemestar = 8 }
                );
                context.SaveChanges();

                context.Courses.AddRange(
                    new Course { Title = "RSWEB", Credits = 6, Semester = 6 },
                    new Course { Title = "WebApp", Credits = 6, Semester = 6 },
                    new Course { Title = "M3", Credits = 6, Semester = 3 }
                );
                context.SaveChanges();

                context.Enrollments.AddRange(
                    new Enrollment { StudentId = 1, CourseId = 1, Semester = 6, Year = 3, Grade = 7 },
                    new Enrollment { StudentId = 2, CourseId = 1, Semester = 6, Year = 3, Grade = 7 },
                    new Enrollment { StudentId = 3, CourseId = 1, Semester = 6, Year = 3, Grade = 7 }
                );
                context.SaveChanges();

                context.Teachers.AddRange(
                    new Teacher { FirstName = "Ilco", LastName = "Stojanovski", HireDate = DateTime.Parse("2016-09-15") },
                    new Teacher { FirstName = "Grozdan", LastName = "Vckov", HireDate = DateTime.Parse("2016-09-15") }
                );
                context.SaveChanges();
            }
        }
    }
}
