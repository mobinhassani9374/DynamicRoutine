using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace DynamicRoutine
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Data Source=WIN-SQL\\MSSQLSERVER2016;Initial Catalog=Mobin.Routine;Persist Security Info=True;User ID=sa; Password=exir@123; MultipleActiveResultSets=True";

            //var connectionString = "Data Source=.;Initial Catalog=Mobin.DynamicRoutine;Integrated Security=True";
            services.AddDbContext<Data.AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services
                .AddSingleton<IDbConnection>(new 
                SqlConnection(connectionString));

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Data.AppDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.Migrate();

            if (!db.Users.Any())
            {
                db.Users.Add(new Entities.User
                {
                    IsAdmin = true,
                    Password = "123456",
                    UserName = "admin",
                    FullName = "مبین حسنی"
                });

                db.Users.Add(new Entities.User
                {
                    IsAdmin = false,
                    Password = "123456",
                    UserName = "mobin1",
                    FullName = "مبین حسنی 1"
                });

                db.Users.Add(new Entities.User
                {
                    IsAdmin = false,
                    Password = "123456",
                    UserName = "mobin2",
                    FullName = "مبین حسنی 2"
                });


                db.Users.Add(new Entities.User
                {
                    IsAdmin = false,
                    Password = "123456",
                    UserName = "mobin3",
                    FullName = "مبین حسنی 3"
                });

                db.SaveChanges();
            }

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
