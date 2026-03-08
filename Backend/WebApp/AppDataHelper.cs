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
            
            var lewisCarrollId = Guid.NewGuid();
            context.Authors.Add(new Author()
            {
                Id = lewisCarrollId,
                Name = "Lewis Carroll",
                Description = "Lewis Carroll (1832–1898) was the pen name of Charles Lutwidge Dodgson, an English writer, mathematician, and photographer. He is best known for his children's fantasy novels 'Alice's Adventures in Wonderland' and 'Through the Looking-Glass'."
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
                    ("test@test.com", "Test", "User", "test", "user"),
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
            if (configuration.GetValue<bool>("DataInitialization:SeedData"))
            {
                using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                if (userManager != null)
                {
                    var adminUser = userManager.FindByEmailAsync("test@test.com").Result;
                    if (adminUser != null)
                    {
                        var lewisCarroll = context.Authors.FirstOrDefault(a => a.Name == "Lewis Carroll");
                        var englishLanguage = context.Languages.FirstOrDefault(l => l.Name == "English");
                        var arialFont = context.FontFaces.FirstOrDefault(f => f.Name == "arial");
                        
                        if (lewisCarroll != null && englishLanguage != null && arialFont != null)
                        {
                            var preference = new Preference()
                            {
                                Id = Guid.NewGuid(),
                                FontFaceId = arialFont.Id,
                                FontSize = 16
                            };
                            context.Preferences.Add(preference);
                            
                            var aliceContent = @"Alice was beginning to get very tired of sitting by her sister on the bank, and of having nothing to do: once or twice she had peeped into the book her sister was reading, but it had no pictures or conversations in it, 'and what is the use of a book,' thought Alice 'without pictures or conversations?'
So she was considering in her own mind (as well as she could, for the hot day made her feel very sleepy and stupid), whether the pleasure of making a daisy-chain would be worth the trouble of getting up and picking the daisies, when suddenly a White Rabbit with pink eyes ran close by her.
There was nothing so very remarkable in that; nor did Alice think it so very much out of the way to hear the Rabbit say to itself, 'Oh dear! Oh dear! I shall be late!' (when she thought it over afterwards, it occurred to her that she ought to have wondered at this, but at the time it all seemed quite natural); but when the Rabbit actually took a watch out of its waistcoat-pocket, and looked at it, and then hurried on, Alice started to her feet, for it flashed across her mind that she had never before seen a rabbit with either a waistcoat-pocket, or a watch to take out of it, and burning with curiosity, she ran across the field after it, and fortunately was just in time to see it pop down a large rabbit-hole under the hedge.
In another moment down went Alice after it, never once considering how in the world she was to get out again.
The rabbit-hole went straight on like a tunnel for some way, and then dipped suddenly down, so suddenly that Alice had not a moment to think about stopping herself before she found herself falling down a very deep well.
Either the well was very deep, or she fell very slowly, for she had plenty of time as she went down to look about her and to wonder what was going to happen next. First, she tried to look down and make out what she was coming to, but it was too dark to see anything; then she looked at the sides of the well, and noticed that they were filled with cupboards and book-shelves; here and there she saw maps and pictures hung upon pegs. She took down a jar from one of the shelves as she passed; it was labelled 'ORANGE MARMALADE', but to her great disappointment it was empty: she did not like to drop the jar for fear of killing somebody, so managed to put it into one of the cupboards as she fell past it.
'Well!' thought Alice to herself, 'after such a fall as this, I shall think nothing of tumbling down stairs! How brave they'll all think me at home! Why, I wouldn't say anything about it, even if I fell off the top of the house!' (Which was very likely true.)
Down, down, down. Would the fall never come to an end? 'I wonder how many miles I've fallen by this time?' she said aloud. 'I must be getting somewhere near the centre of the earth. Let me see: that would be four thousand miles down, I think.'
Down, down, down. There was nothing else to do, so Alice soon began talking again. 'Dinah'll miss me very much to-night, I should think!' (Dinah was the cat.) 'I hope they'll remember her saucer of milk at tea-time. Dinah my dear! I wish you were down here with me! There are no mice in the air, I'm afraid, but you might catch a bat, and that's very like a mouse, you know. But do cats eat bats, I wonder?' And here Alice began to get rather sleepy, and went on saying to herself, in a dreamy sort of way, 'Do cats eat bats? Do cats eat bats?' and sometimes, 'Do bats eat cats?' for, you see, as she couldn't answer either question, it didn't much matter which way she put it. She felt that she was dozing off, and had just begun to dream that she was walking hand in hand with Dinah, and saying to her very earnestly, 'Now, Dinah, tell me the truth: did you ever eat a bat?' when suddenly, thump! thump! down she came upon a heap of sticks and dry leaves, and the fall was over.
Alice was not a bit hurt, and she jumped up on to her feet in a moment: she looked up, but it was all dark overhead; before her was another long passage, and the White Rabbit was still in sight, hurrying down it. There was not a moment to be lost: away went Alice like the wind, and was just in time to hear it say, as it turned a corner, 'Oh my ears and whiskers, how late it's getting!' She was close behind it when she turned the corner, but the Rabbit was no longer to be seen: she found herself in a long, low hall, which was lit up by a row of lamps hanging from the roof.
There were doors all round the hall, but they were all locked; and when Alice had been all the way down one side and up the other, trying every door, she walked sadly down the middle, wondering how she was ever to get out again.
Suddenly she came upon a little three-legged table, all made of solid glass; there was nothing on it except a tiny golden key, and Alice's first thought was that it might belong to one of the doors of the hall; but, alas! either the locks were too large, or the key was too small, but at any rate it would not open any of them. However, on the second time round, she came upon a low curtain she had not noticed before, and behind it was a little door about fifteen inches high: she tried the little golden key in the lock, and to her great delight it fitted!"
                            var aliceBook = new Book()
                            {
                                Id = Guid.NewGuid(),
                                Title = "Alice's Adventures in Wonderland",
                                Content = aliceContent,
                                SymbolsTotal = aliceContent.Length,
                                CurrentSymbol = 0,
                                IsFinished = false,
                                AuthorId = lewisCarroll.Id,
                                LanguageId = englishLanguage.Id,
                                PreferenceId = preference.Id,
                                AppUserId = adminUser.Id
                            };
                            context.Books.Add(aliceBook);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }

    
