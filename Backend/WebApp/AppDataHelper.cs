using System.Security.Claims;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using AutoMapper;
using Base.Domain.Identity;
using DAL.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public static class AppDataHelper
{
    public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        // dependency injection scope
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        //DB instance
        using var context = serviceScope
            .ServiceProvider.GetService<AppDbContext>();

        if (context == null)
        {
            throw new ApplicationException("Problem in services. No db context.");
        }


        //!those rows were written in case if user does not have access to the server command line
        // can't connect - wrong address
        // can't connect - wrong user/pass
        // can connect - but no database
        // can connect - there is database

        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }

        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }

        if (configuration.GetValue<bool>("DataInitialization:SeedDefaultValues"))
        {
            // uow.Authors.Add(new Author()
            // {
            //     Books = null,
            //     Description = "",
            //     Id = Guid.NewGuid(),
            //     Name = "UNDEFINED"
            // });
            // var g = Guid.NewGuid();
            // uow.FontFace.Add(new FontFace()
            // {
            //     Id = g,
            //     Name = "Times New Roman"
            // });  
            //
            // uow.Preferences.Add(new Preference()
            // {
            //     FontFaceId = g,
            //     FontSize = 16,
            //     Id = Guid.NewGuid(),
            // });
            //
            //

            context.Authors.Add(new Author()
            {
                Id = new Guid(),
                Name = "Toomas",
                Description = "..."
            });
            
            var languages = new string[] {"English", "Russian", "Spanish", "Estonian"};
            foreach (var l in languages)
            {
                context.Languages.Add(new Language()
                {
                    Id = Guid.NewGuid(),
                    Name = l
                });                
            }
            
            var fonts = new string[] {"arial", "sans serif"};
            foreach (var l in fonts)
            {
                context.FontFaces.Add(new FontFace()
                {
                    Id = Guid.NewGuid(),
                    Name = l
                });                
            }

            var tags = new string[] {"Fiction", "History", "Popular", "Horror"};
            foreach (var l in tags)
            {
                context.Tags.Add(new Tag()
                {
                    Id = Guid.NewGuid(),
                    Name = l
                });                
            }
            
            var hTypes = new string[] {"quote", "note", "dictionary"};
            foreach (var l in hTypes)
            {
                context.HighlightedTypes.Add(new HighlightedType()
                {
                    Id = Guid.NewGuid(),
                    Name = l
                });                
            }

            context.SaveChanges();
        }

            if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
            {
                using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

                if (userManager == null || roleManager == null)
                {
                    throw new NullReferenceException("userManager or roleManager cannot be null!");
                }

                var roles = new (string name, string displayName)[]
                {
                    ("admin", "System administrator"),
                    ("user", "Normal system user")
                };

                foreach (var roleInfo in roles)
                {
                    var role = roleManager.FindByNameAsync(roleInfo.name).Result;
                    if (role == null)
                    {
                        var identityResult = roleManager.CreateAsync(new AppRole()
                        {
                            Name = roleInfo.name,
                            DisplayName = roleInfo.displayName
                        }).Result;
                        if (!identityResult.Succeeded)
                        {
                            throw new ApplicationException("Role creation failed");
                        }
                    }
                }

                var users = new (string username, string firstName, string lastName, string password, string roles)[]
                {
                    ("admin@itcollege.ee", "Admin", "College", "Dot.house1", "user,admin"),
                    ("fkaver@itcollege.ee", "Fake", "Käver", "Dot.house1", "user,admin"),
                    ("user@itcollege.ee", "User", "College", "Dot.house1", "user"),
                    ("newuser@itcollege.ee", "User No Roles", "College", "Kala.Dot.house1", ""),
                };

                foreach (var userInfo in users)
                {
                    var user = userManager.FindByEmailAsync(userInfo.username).Result;
                    if (user == null)
                    {
                        user = new AppUser()
                        {
                            Email = userInfo.username,
                            FirstName = userInfo.firstName,
                            LastName = userInfo.lastName,
                            UserName = userInfo.username,
                            EmailConfirmed = true,
                        };
                        var identityResult = userManager.CreateAsync(user, userInfo.password).Result;
                        identityResult = userManager.AddClaimAsync(user, new Claim("aspnet.firstname", user.FirstName))
                            .Result;
                        identityResult = userManager.AddClaimAsync(user, new Claim("aspnet.lastname", user.LastName))
                            .Result;

                        if (!identityResult.Succeeded)
                        {
                            throw new ApplicationException("Cannot create user!");
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(userInfo.roles))
                    {
                        var identityResultRole = userManager.AddToRolesAsync(user,
                            userInfo.roles.Split(",").Select(r => r.Trim())
                        ).Result;
                    }
                }
            }
            /*if (configuration.GetValue<bool>("DataInitialization:SeedData"))
            {
                var f = new FooBar
                {
                    Name =
                    {
                        ["en"] = "english",
                        ["et"] = "estonian",
                        ["ru"] = "russian",
                    }
                };
                context.FooBars.Add(f);
                context.SaveChanges();
            }*/
        }
    }

    
