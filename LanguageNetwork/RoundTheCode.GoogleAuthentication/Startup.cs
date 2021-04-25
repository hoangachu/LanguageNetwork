using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic.CompilerServices;
using RoundTheCode.GoogleAuthentication.Controllers;

namespace RoundTheCode.GoogleAuthentication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static string connectionString = "";
        public static Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment { get; set; }
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;
            _hostingEnvironment = env;
            connectionString = Configuration.GetConnectionString("DefaultConnection").ToString();
        }

   

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/LoginUser";
                  
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "35619218500-ihg8c9gmom4ccq8j4af8hjqcdt5dsdrk.apps.googleusercontent.com";
                    options.ClientSecret = "0UcrIJipXkBafsAvnH3sDpQj";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "353143932808462";
                    options.ClientSecret = "94f22f24e4048719a9b48ee64bcc0631";
                })

                    .AddCookie("SecurityScheme", options =>
                    {
                        options.LoginPath = "/Account/google-login";
                        options.AccessDeniedPath = new PathString("/Account/google-login");
                        options.Cookie = new CookieBuilder
                        {
                            //Domain = "",
                            HttpOnly = true,
                            Name = ".aspNetCoreDemo.Security.Cookie",
                            Path = "/",
                            SameSite = SameSiteMode.Lax,
                            SecurePolicy = CookieSecurePolicy.SameAsRequest
                        };
                        options.Events = new CookieAuthenticationEvents
                        {
                            OnSignedIn = context =>
                            {
                                Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                                    "OnSignedIn", context.Principal.Identity.Name);
                                return Task.CompletedTask;
                            },
                            OnSigningOut = context =>
                            {
                                Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                                    "OnSigningOut", context.HttpContext.User.Identity.Name);
                                return Task.CompletedTask;
                            },
                            OnValidatePrincipal = context =>
                            {
                                Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                                    "OnValidatePrincipal", context.Principal.Identity.Name);
                                return Task.CompletedTask;
                            }
                        }

                        ;
                        //options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                        options.LoginPath = new PathString("/Account/LoginUser");
                        options.ReturnUrlParameter = "RequestPath";
                        options.SlidingExpiration = true;
                    })
                ;
            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "admin";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan(0,1772, 0);    // Thời gian tồn tại của Session
            });

            services.AddHttpContextAccessor();
       
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
