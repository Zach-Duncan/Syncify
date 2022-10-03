using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using LearningStarter.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LearningStarter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.AddHsts(options =>
            {
                options.MaxAge = TimeSpan.MaxValue;
                options.Preload = true;
                options.IncludeSubDomains = true;
            });

            services.AddDbContext<DataContext>(options =>
            {
                // options.UseInMemoryDatabase("FooBar");
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //TODO
            services.AddMvc();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });

            services.AddAuthorization();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Learning Starter Server",
                    Version = "v1",
                    Description = "Description for the API goes here.",
                });

                c.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
                c.MapType(typeof(IFormFile), () => new OpenApiSchema { Type = "file", Format = "binary" });
            });

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "learning-starter-web/build";
            });

            services.AddHttpContextAccessor();

            // configure DI for application services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            dataContext.Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            }); ;

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Learning Starter Server API V1");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(x => x.MapControllers());

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "learning-starter-web";
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3001");
                }
            });
            SeedUsers(dataContext);
            SeedMemberRoles(dataContext);
            SeedProfileColors(dataContext);
            SeedMealTypes(dataContext);
            SeedUnits(dataContext);
            SeedGroups(dataContext);
            SeedCalendars(dataContext);
            SeedIngredients(dataContext);
            SeedRecipes(dataContext);
            SeedShoppingList(dataContext);
            SeedEvents(dataContext);
            SeedGroupMembers(dataContext);
            
        }

        private void SeedGroupMembers(DataContext dataContext)
        {
            if (!dataContext.GroupMembers.Any())
            {
                var memberRoles = dataContext.MemberRoles.ToList();
                var users = dataContext.Users.ToList();
                var groups = dataContext.Groups.ToList();

                var seededGroupMembers = new List<GroupMember>
                {
                    new GroupMember
                    {
                        MemberRole = memberRoles.First(x => x.Name == StringEnums.MemberRoles.GroupLeader),
                        User = users.FirstOrDefault(),
                        Group = groups.First(x => x.Name == StringEnums.GroupNames.GroupA)
                    },

                    new GroupMember
                    {
                        MemberRole = memberRoles.First(x => x.Name == StringEnums.MemberRoles.Member),
                        User = users.FirstOrDefault(),
                        Group = groups.First(x => x.Name == StringEnums.GroupNames.GroupB)
                    },

                    new GroupMember
                    {
                        MemberRole = memberRoles.First(x => x.Name == StringEnums.MemberRoles.Unassigned),
                        User = users.FirstOrDefault(),
                        Group = groups.First(x => x.Name == StringEnums.GroupNames.GroupC)
                    },
                };

                dataContext.GroupMembers.AddRange(seededGroupMembers);
                dataContext.SaveChanges();
            }
        }

        private void SeedMemberRoles(DataContext dataContext)
        {
            if (!dataContext.MemberRoles.Any())
            {
                var seededMemberRoles = new List<MemberRole>
                {
                    new MemberRole
                    {
                        Name = StringEnums.MemberRoles.Administrator
                    },
                    new MemberRole
                    {
                        Name = StringEnums.MemberRoles.GroupLeader
                    },
                    new MemberRole
                    {
                        Name = StringEnums.MemberRoles.Member
                    },
                    new MemberRole
                    {
                        Name = StringEnums.MemberRoles.Unassigned
                    }
                };
                dataContext.MemberRoles.AddRange(seededMemberRoles);
                dataContext.SaveChanges();
            }
        }

        private void SeedProfileColors(DataContext dataContext)
        {
            if (!dataContext.ProfileColors.Any())
            {
                var seededProfileColors = new List<ProfileColor>
                {
                    new ProfileColor
                    {
                        Colors = StringEnums.ProfileColors.Red
                    },
                    new ProfileColor
                    {
                        Colors = StringEnums.ProfileColors.Blue
                    },
                    new ProfileColor
                    {
                        Colors = StringEnums.ProfileColors.Green
                    },
                    new ProfileColor
                    {
                        Colors = StringEnums.ProfileColors.Yellow
                    },
                    new ProfileColor
                    {
                        Colors = StringEnums.ProfileColors.Pink
                    }
                };

                dataContext.ProfileColors.AddRange(seededProfileColors);
                dataContext.SaveChanges();
            }
        }

        public void SeedUsers(DataContext dataContext)
        {
            var numUsers = dataContext.Users.Count();

            if (dataContext.Users.Any())
            {
                return;
            }

            var seededUser = new List<User>
                {
                    new User
                    {
                    FirstName = StringEnums.UserFirstNames.One,
                    LastName = StringEnums.UserLastNames.One,
                    Username = StringEnums.UserUserNames.One,
                    Email = StringEnums.UserEmails.One,
                    PhoneNumber = StringEnums.UserPhoneNumbers.One,
                    Password = StringEnums.UserPasswords.One,
                    BirthDay = StringEnums.UserBirthdays.One,
                },

                new User
                {
                    FirstName = StringEnums.UserFirstNames.Two,
                    LastName = StringEnums.UserLastNames.Two,
                    Username = StringEnums.UserUserNames.Two,
                    Email = StringEnums.UserEmails.Two,
                    PhoneNumber = StringEnums.UserPhoneNumbers.Two,
                    Password = StringEnums.UserPasswords.Two,
                    BirthDay = StringEnums.UserBirthdays.Two,
                },

                new User
                {
                    FirstName = StringEnums.UserFirstNames.Three,
                    LastName = StringEnums.UserLastNames.Three,
                    Username = StringEnums.UserUserNames.Three,
                    Email = StringEnums.UserEmails.Three,
                    PhoneNumber = StringEnums.UserPhoneNumbers.Three,
                    Password = StringEnums.UserPasswords.Three,
                    BirthDay = StringEnums.UserBirthdays.Three,
                }
            };

            dataContext.Users.AddRange(seededUser);
            dataContext.SaveChanges();
        }


        private void SeedCalendars(DataContext dataContext)
        {
            if (!dataContext.Calendars.Any())
            {
                var groups = dataContext.Groups.ToList();

                var seededCalendars = new List<Calendar>
                {
                    new Calendar
                    {
                        Group = groups.First(x => x.Name == StringEnums.GroupNames.GroupA)
                    },

                    new Calendar
                    {
                        Group = groups.First(x => x.Name == StringEnums.GroupNames.GroupB)
                    },

                    new Calendar
                    {
                        Group = groups.First(x => x.Name == StringEnums.GroupNames.GroupC)
                    },

                    new Calendar
                    {
                        Group = groups.First(x => x.Name == StringEnums.GroupNames.GroupD)
                    },

                };

                dataContext.Calendars.AddRange(seededCalendars);
                dataContext.SaveChanges();
            }
        }

        private void SeedGroups(DataContext dataContext)
        {
            if (!dataContext.Groups.Any())
            {
                var seededGroups = new List<Group>
                {
                    new Group
                    {
                        Name = StringEnums.GroupNames.GroupA,
                        Image = StringEnums.Images.Image
                    },

                    new Group
                    {
                        Name = StringEnums.GroupNames.GroupB,
                        Image = StringEnums.Images.Image
                    },

                    new Group
                    {
                        Name = StringEnums.GroupNames.GroupC,
                        Image = StringEnums.Images.Image
                    },

                    new Group
                    {
                        Name = StringEnums.GroupNames.GroupD,
                        Image = StringEnums.Images.Image
                    }
                                      
                };

                dataContext.Groups.AddRange(seededGroups);
                dataContext.SaveChanges();
            }
        }

        private void SeedUnits(DataContext dataContext)
        {
            if (!dataContext.Units.Any())
            {
                var seededUnits = new List<Unit>
                {
                    new Unit
                    {
                        Name = StringEnums.UnitNames.Pounds,
                        Abbreviation = StringEnums.UnitAbbreviations.Pounds
                    },

                    new Unit
                    {
                        Name = StringEnums.UnitNames.Packages,
                        Abbreviation = StringEnums.UnitAbbreviations.Packages
                    },

                    new Unit
                    {
                        Name = StringEnums.UnitNames.Individual,
                        Abbreviation = StringEnums.UnitAbbreviations.Individual
                    },

                    new Unit
                    {
                        Name = StringEnums.UnitNames.Cups,
                        Abbreviation = StringEnums.UnitAbbreviations.Cups
                    },

                    new Unit
                    {
                        Name = StringEnums.UnitNames.Teaspoons,
                        Abbreviation = StringEnums.UnitAbbreviations.Teaspoons
                    },

                    new Unit
                    {
                        Name = StringEnums.UnitNames.Tablespoons,
                        Abbreviation = StringEnums.UnitAbbreviations.Tablespoons
                    },
                };

                dataContext.Units.AddRange(seededUnits);
                dataContext.SaveChanges();
            }
        }

        private void SeedIngredients(DataContext dataContext)
        {
            if (!dataContext.Ingredients.Any())
            {
                var units = dataContext.Units.ToList();

                var seededIngredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Name = StringEnums.IngredientNames.HamburgerMeat,
                        Image = StringEnums.Images.Image,
                        Unit = units.First(x => x.Abbreviation == StringEnums.UnitAbbreviations.Pounds)
                    },

                    new Ingredient
                    {
                        Name = StringEnums.IngredientNames.RanchSeasoningMix,
                        Image = StringEnums.Images.Image,
                        Unit = units.First(x => x.Abbreviation == StringEnums.UnitAbbreviations.Packages)
                    },

                    new Ingredient
                    {
                        Name = StringEnums.IngredientNames.HamburgerBuns,
                        Image = StringEnums.Images.Image,
                        Unit = units.First(x => x.Abbreviation == StringEnums.UnitAbbreviations.Individual)
                    },
                };

                dataContext.Ingredients.AddRange(seededIngredients);
                dataContext.SaveChanges();
            }
        }

        private void SeedMealTypes(DataContext dataContext)
        {
            if (!dataContext.MealTypes.Any())
            {
                var seededMealTypes = new List<MealType>
                {
                   new MealType
                   {
                     Name = StringEnums.MealTypes.Breakfast
                   },

                    new MealType
                    {
                    Name = StringEnums.MealTypes.Brunch
                    },

                    new MealType
                    {
                    Name = StringEnums.MealTypes.Lunch
                    },

                    new MealType
                    {
                    Name = StringEnums.MealTypes.Snack
                    },

                    new MealType
                    {
                    Name = StringEnums.MealTypes.Dinner
                    },
                };

                dataContext.MealTypes.AddRange(seededMealTypes);
                dataContext.SaveChanges();
            }
        }

        private void SeedRecipes(DataContext dataContext)
        {
            if (!dataContext.Recipes.Any())
            {
                
                var mealTypes = dataContext.MealTypes.ToList();
                var calendar = dataContext.Calendars.FirstOrDefault();
                //var group = dataContext.Groups.First();

                var seededRecipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Name = StringEnums.RecipeNames.Hamburger,
                        Image = StringEnums.Images.Image,
                        Servings = 4,
                        MealType = mealTypes.First(x => x.Name == StringEnums.MealTypes.Lunch),
                        Calendar = calendar
            
                        
                    },

                    new Recipe
                    {
                        Name = StringEnums.RecipeNames.Burritoes,
                        Image = StringEnums.Images.Image,
                        Servings = 6,
                        MealType = mealTypes.First(x => x.Name == StringEnums.MealTypes.Dinner),
                        Calendar = calendar

                    },

                    new Recipe
                    {
                        Name = StringEnums.RecipeNames.SloppyJoes,
                        Image = StringEnums.Images.Image,
                        Servings = 4,
                        MealType = mealTypes.First(x => x.Name == StringEnums.MealTypes.Brunch),
                        Calendar = calendar
                    },

                    new Recipe
                    {
                        Name = StringEnums.RecipeNames.Spaghetti,
                        Image = StringEnums.Images.Image,
                        Servings = 4,
                        MealType = mealTypes.First(x => x.Name == StringEnums.MealTypes.Breakfast),
                        Calendar = calendar
                    },

                    new Recipe
                    {
                        Name = StringEnums.RecipeNames.PeanutButterAndJelly,
                        Image = StringEnums.Images.Image,
                        Servings = 1,
                        MealType = mealTypes.First(x => x.Name == StringEnums.MealTypes.Snack),
                        Calendar = calendar
                    },

                    new Recipe
                    {
                        Name = StringEnums.RecipeNames.Toast,
                        Image = StringEnums.Images.Image,
                        Servings = 1,
                        MealType = mealTypes.First(x => x.Name == StringEnums.MealTypes.Breakfast),
                        Calendar = calendar
                    }
                };

                dataContext.Recipes.AddRange(seededRecipes);
                dataContext.SaveChanges();

            }
        }

        private void SeedShoppingList(DataContext dataContext)
        {

            var numShoppinglist = dataContext.ShoppingLists.ToList();

            if (dataContext.ShoppingLists.Any())
            {
                return;
            }

            var seededShoppingList = new List<ShoppingList>
            {
                new ShoppingList
                {
                    Name = "Bread",
                },
                new ShoppingList
                {
                    Name = "Bananas",
                },
                new ShoppingList
                {
                    Name = "Peanut Butter",
                }
            };

            
            dataContext.ShoppingLists.AddRange(seededShoppingList);
            dataContext.SaveChanges();

        }
        private void SeedEvents(DataContext dataContext)
        {
            if (!dataContext.Events.Any())
            {
                var seededEvents = new Event
                {
                    Name = "Cole's Birthday Bash!",
                    EventDetails = "Chillin at the Blue Moon, 10:00pm",
                    CreatedDate = DateTime.Now,
                };

                dataContext.Events.AddRange(seededEvents);
                dataContext.SaveChanges();
            }
        }
    }
}

    
