using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
    public static class IdentityDataSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
			try
			{
                if (!roleManager.Roles.Any())
                {
                    var roles = new List<IdentityRole>()
                {
                    new IdentityRole() {Name = "SuperAdmin"},
                    new IdentityRole() {Name = "Admin"}
                };

                    foreach (var role in roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name).Result)
                        {
                            roleManager.CreateAsync(role).Wait();
                        }
                    }
                }

                if (!userManager.Users.Any())
                {
                    var superAdmin = new ApplicationUser
                    {
                        FirstName = "Gamal",
                        LastName = "Ehab",
                        UserName = "GamalEhab",
                        Email = "gamal@gmail.com",
                        PhoneNumber = "1234567890",
                    };

                    userManager.CreateAsync(superAdmin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(superAdmin, "SuperAdmin").Wait();

                    var admin = new ApplicationUser
                    {
                        FirstName = "Ali",
                        LastName = "Mohamed",
                        UserName = "AliMohamed",
                        Email = "ali@gmail.com",
                        PhoneNumber = "1234567865",
                    };

                    userManager.CreateAsync(admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }

                return true;
            }
			catch (Exception ex)
			{

                Console.WriteLine($"Seeding Failed: {ex}");
                return false;
			}
   
        }
    }
}
