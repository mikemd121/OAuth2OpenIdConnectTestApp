using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth2OpenIdConnectTestApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = "oidc";
                options.DefaultSignInScheme = "Cookies";
            })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    options.Authority = "https://accounts.google.com";
                    options.RequireHttpsMetadata = false;
                    options.ClientId = "170919348406-u45632t926c4oeodsjnq1dmu179kpqeu.apps.googleusercontent.com";
                    options.ClientSecret = "aJ0L5Y7RXVPo4nqIBuPtyJLg";
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;
                    options.CallbackPath = "/Home/HomeIndex/";

                    options.Events = new OpenIdConnectEvents()
                    {
                        OnRedirectToIdentityProvider = (context) =>
                        {
                            if (context.Request.Path != "/Login/External")
                            {
                                context.Response.Redirect("/Login/LoginPage");
                                context.HandleResponse();
                            }

                            return Task.FromResult(0);
                        }
                    };

                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });



            //app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            //{
            //    ClientId = "CLIENT_ID_GOES_HERE",
            //    ClientSecret = "CLIENT_SECRET_GOES_HERE",
            //    Authority = "https://accounts.google.com",
            //    ResponseType = OpenIdConnectResponseType.Code,
            //    GetClaimsFromUserInfoEndpoint = true,
            //    SaveTokens = true,
            //    Events = new OpenIdConnectEvents()
            //    {
            //        OnRedirectToIdentityProvider = (context) =>
            //        {
            //            if (context.Request.Path != "/account/external")
            //            {
            //                context.Response.Redirect("/account/login");
            //                context.HandleResponse();
            //            }

            //            return Task.FromResult(0);
            //        }
            //    }
            //});
        }
    }
}
