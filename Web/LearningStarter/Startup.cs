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
            SeedMealTypes(dataContext);
            SeedUnits(dataContext);
            SeedGroups(dataContext);
            SeedCalendars(dataContext);
            SeedIngredients(dataContext);
            SeedRecipes(dataContext);
            
        }

        private void SeedCalendars(DataContext dataContext)
        {
            if (!dataContext.Ingredients.Any())
            {
                var group = dataContext.Groups.First();

                var seededCalendar = new Calendar
                {
                    Group = group
                };

                dataContext.Calendars.Add(seededCalendar);
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
                var calendar = dataContext.Calendars.First();

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

        public void SeedUsers(DataContext dataContext)
            {
                var numUsers = dataContext.Users.Count();

                if (numUsers == 0)
                {
                    var seededUser = new User
                    {
                        FirstName = "Seeded",
                        LastName = "User",
                        Username = "admin",
                        Password = "password"
                    };

                    dataContext.Users.Add(seededUser);
                    dataContext.SaveChanges();
                }
            }
        
    }
}