using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebPlaces2020.CLI.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using WebPlaces2020.Validator;
using WebPlaces2020.CLI.Context;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityServer4;
using IdentityServer4.Test;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WebPlaces2020.CLI
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
            IMvcBuilder mvcBuilder = services.AddMvc();

            services.AddIdentity<IdtUser, IdtRole>().AddEntityFrameworkStores<IdtDbContext>();
            services.AddDbContext<IdtDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().AddFluentValidation();
            services.AddDbContext<CompteContext>(opt => opt.UseInMemoryDatabase("AccountsList"));
            services.AddDbContext<PlaceContext>(opt => opt.UseInMemoryDatabase("PlacesList"));
            //services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));
            services.AddControllersWithViews();
            services.AddTransient<IValidator<Place>, PlaceValidator>();
            services.AddTransient<IValidator<Compte>, AccountValidator>();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                 .AddIdentityServer()
                 .AddDeveloperSigningCredential()
                 .AddInMemoryClients(new List<Client>
                 {
                    new Client
                    {
                        ClientId = "console",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha512())
                        },
                        AllowedScopes =
                        {
                            "api1"
                        }
                    }
                 }).AddInMemoryApiResources(new List<ApiResource>
                 {
                    new ApiResource("api1", "test api")
                 })
                 .AddInMemoryClients(new List<Client>
                 {
                    new Client
                    {
                        ClientId = "mvcweb",
                        ClientName = "MVC Web",
                        AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                        ClientSecrets =
                        { 
                            new Secret("secret".Sha256())
                        },
                        RedirectUris = { "http://localhost:44325/signin-oidc"},
                        PostLogoutRedirectUris = { "http://localhost:44325/signout-callback-oidc"},
                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "api1"
                        }
                    }
                 }).AddInMemoryApiResources(new List<ApiResource>
                 {
                    new ApiResource("api1", "test api")
                 })
                 .AddInMemoryIdentityResources(new List<IdentityResource>
                 { new IdentityResources.OpenId(),
                 new IdentityResources.Profile(),
                 })
                 .AddTestUsers (new List<TestUser>
                 {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "user1",
                    Password = "password",

                    Claims = new[]
                    {
                        new Claim("name", "User1"),
                        new Claim("website", "http://www.ephec.be"),
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "user2",
                    Password = "password",

                    Claims = new[]
                    {
                        new Claim("name", "User2"),
                        new Claim("website", "http://www.ephec.be"),
                    }
                }

                 });

            services.AddAuthentication(defaultScheme: "Bearer")
        .AddIdentityServerAuthentication(authenticationScheme: "Bearer", configureOptions: options =>
        {
            options.Authority = "http://localhost:44325";
            options.RequireHttpsMetadata = false;
            options.ApiName = "api1";
        });

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.SignInScheme = "Cookies";

        options.Authority = "http://localhost:44325";
        options.RequireHttpsMetadata = false;
        options.ResponseType = "code id_token";
        options.GetClaimsFromUserInfoEndpoint = true;

        options.ClientId = "mvcweb";
        options.ClientSecret = "secret";
        options.SaveTokens = true;
        options.Scope.Add("api1");
    });

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
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }); 

            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();

        }
    }
}
