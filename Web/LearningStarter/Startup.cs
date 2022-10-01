using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
            SeedShoppingList(dataContext);
            SeedEvents(dataContext);
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
                    FirstName = "Seeded",
                    LastName = "User",
                    Username = "admin",
                    Email = "owner@mail.com",
                    PhoneNumber = "1112235678",
                    Password = "password",
                    BirthDay = "01/01/2001",
                },
                new User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "JDough",
                    Email = "Jdougie23@mail.com",
                    PhoneNumber = "9824454747",
                    Password = "T0pSecR3t",
                    BirthDay = "12/25/1998",
                },
                new User
                {
                    FirstName = "Mike",
                    LastName = "Hunt",
                    Username = "HuntHertz",
                    Email = "mikehunthertz@gmail.com",
                    PhoneNumber = "985-867-5309",
                    Password = "TimmyTutoneLover69",
                    BirthDay = "09/30/2002",

                }
            };

            dataContext.Users.AddRange(seededUser);
            dataContext.SaveChanges();
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
                var seededGroup = new Group
                {
                    Name = "Group A",
                    Image = "ImageURL"                   
                };

                dataContext.Groups.Add(seededGroup);
                dataContext.SaveChanges();
            }
        }

        private void SeedUnits(DataContext dataContext)
        {
            if (!dataContext.Units.Any())
            {
                var seededUnit = new Unit
                {
                    Name = "Cup",
                    Abbreviation = "C"
                };

                dataContext.Units.Add(seededUnit);
                dataContext.SaveChanges();
            }
        }

        private void SeedIngredients(DataContext dataContext)
        {
            if (!dataContext.Ingredients.Any())
            {
                var unit = dataContext.Units.First();

                var seededIngredient = new Ingredient
                {
                    Name = "",
                    Image = "ImageURL",
                    Unit = unit,
                };

                dataContext.Ingredients.Add(seededIngredient);
                dataContext.SaveChanges();
            }
        }

        private void SeedMealTypes(DataContext dataContext)
        {
            if (!dataContext.MealTypes.Any())
            {
                var seededMealType = new MealType
                {
                    Name = "Dinner"
                };

                dataContext.MealTypes.Add(seededMealType);
                dataContext.SaveChanges();
            }
        }

        private void SeedRecipes(DataContext dataContext)
        {
            if (!dataContext.Recipes.Any())
            {
                var unit = dataContext.Units.First();
                var mealType = dataContext.MealTypes.First();
                var calendar = dataContext.Calendars.First();

                var seededRecipe = new Recipe
                {
                    Name = "Hamburger",
                    Image = "ImageURL",
                    Servings = 1,
                    Unit = unit,
                    MealType = mealType,
                    Calendar = calendar
                };

                dataContext.Recipes.Add(seededRecipe);
                dataContext.SaveChanges();

            }
        }

        private void SeedShoppingList(DataContext dataContext)
        {

            var numShoppinglist = dataContext.ShoppingLists.Count();

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

    
