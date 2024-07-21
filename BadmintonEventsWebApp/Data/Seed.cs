using BadmintonEventsWebApp.Data.Enums;
using BadmintonEventsWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BadmintonEventsWebApp.Data
{
    /// <summary>
    /// NUGET INSTALLATION
    /// - Microsoft.EntityFrameworkCore
    /// - Microsoft.EntityFrameworkCore.SqlServer
    /// - Microsoft.EntityFrameworkCore.Tools
    /// 
    /// PACKAGE MANAGER CONSOLE
    /// - Add-Migration InitialCreate
    /// - Update-Database
    /// 
    /// DEVELOPER POWERSHELL
    /// - dotnet run seeddata
    /// </summary>
    public class Seed
    {
        public static void SeedData( IApplicationBuilder applicationBuilder )
        {
            using ( var serviceScope = applicationBuilder.ApplicationServices.CreateScope() )
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if ( !context.Clubs.Any() )
                {
                    context.Clubs.AddRange( new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Badminton Club 1",
                            Image = "https://images.firstpost.com/wp-content/uploads/2020/05/Badminton-generic-AFP-640.jpg?im=Resize,width=720,aspect=fit,type=normal",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.Group,
                            Address = new Address()
                            {
                                Street = "130 Kingsway, Madeley WA 6065",
                                City = "Perth",
                                State = "WA"
                            }
                         },
                        new Club()
                        {
                            Title = "Badminton Club 2",
                            Image = "https://images.firstpost.com/wp-content/uploads/2020/05/Badminton-generic-AFP-640.jpg?im=Resize,width=720,aspect=fit,type=normal",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.OneOnOne,
                            Address = new Address()
                            {
                                Street = "130 Kingsway, Madeley WA 6065",
                                City = "Perth",
                                State = "WA"
                            }
                        },
                        new Club()
                        {
                            Title = "Badminton Club 3",
                            Image = "https://images.firstpost.com/wp-content/uploads/2020/05/Badminton-generic-AFP-640.jpg?im=Resize,width=720,aspect=fit,type=normal",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.Group,
                            Address = new Address()
                            {
                                Street = "130 Kingsway, Madeley WA 6065",
                                City = "Perth",
                                State = "WA"
                            }
                        },
                        new Club()
                        {
                            Title = "Badminton Club 3",
                            Image = "https://images.firstpost.com/wp-content/uploads/2020/05/Badminton-generic-AFP-640.jpg?im=Resize,width=720,aspect=fit,type=normal",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.OneOnOne,
                            Address = new Address()
                            {
                                Street = "130 Kingsway, Madeley WA 6065",
                                City = "Michigan",
                                State = "WA"
                            }
                        }
                    } );
                    context.SaveChanges();
                }
                //Races
                if ( !context.Tournaments.Any() )
                {
                    context.Tournaments.AddRange( new List<Tournament>()
                    {
                        new Tournament()
                        {
                            Title = "Badminton Tournament 1",
                            Image = "https://images.firstpost.com/wp-content/uploads/2020/05/Badminton-generic-AFP-640.jpg?im=Resize,width=720,aspect=fit,type=normal",
                            Description = "This is the description of the first race",
                            MatchCategory = MatchCategory.MensDouble,
                            AgeCategory = AgeCategory.Open,
                            Address = new Address()
                            {
                                Street = "130 Kingsway, Madeley WA 6065",
                                City = "Perth",
                                State = "WA"
                            }
                        },
                        new Tournament()
                        {
                            Title = "Badminton Tournament 2",
                            Image = "https://images.firstpost.com/wp-content/uploads/2020/05/Badminton-generic-AFP-640.jpg?im=Resize,width=720,aspect=fit,type=normal",
                            Description = "This is the description of the first race",
                            MatchCategory = MatchCategory.MensSingle,
                            AgeCategory = AgeCategory.Open,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Street = "130 Kingsway, Madeley WA 6065",
                                City = "Perth",
                                State = "WA"
                            }
                        }
                    } );
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync( IApplicationBuilder applicationBuilder )
        {
            using ( var serviceScope = applicationBuilder.ApplicationServices.CreateScope() )
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if ( !await roleManager.RoleExistsAsync( UserRoles.Admin ) )
                    await roleManager.CreateAsync( new IdentityRole( UserRoles.Admin ) );
                if ( !await roleManager.RoleExistsAsync( UserRoles.User ) )
                    await roleManager.CreateAsync( new IdentityRole( UserRoles.User ) );

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "wilsonleodeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync( adminUserEmail );
                if ( adminUser == null )
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "wilsonleodev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "130 Kingsway, Madeley WA 6065",
                            City = "Perth",
                            State = "WA"
                        }
                    };
                    await userManager.CreateAsync( newAdminUser, "Coding@1234?" );
                    await userManager.AddToRoleAsync( newAdminUser, UserRoles.Admin );
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync( appUserEmail );
                if ( appUser == null )
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "130 Kingsway, Madeley WA 6065",
                            City = "Perth",
                            State = "WA"
                        }
                    };
                    await userManager.CreateAsync( newAppUser, "Coding@1234?" );
                    await userManager.AddToRoleAsync( newAppUser, UserRoles.User );
                }
            }
        }
    }
}
