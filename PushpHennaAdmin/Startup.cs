using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PushpHennaAdmin;
using PushpHennaAdmin.Filter;
using System;
using System.IO;
using System.Text;

namespace PushpHennaAdmin
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
            //try
            //{ 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                        .AddJwtBearer(options =>
                                        {
                                            options.TokenValidationParameters = new TokenValidationParameters
                                            {
                                                ValidateIssuer = true,
                                                ValidateAudience = true,
                                                ValidateLifetime = true,
                                                ValidateIssuerSigningKey = true,
                                                ValidIssuer = Configuration["Jwt:Issuer"],
                                                ValidAudience = Configuration["Jwt:Issuer"],
                                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                                            };
                                        });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("JwtAuth", policy =>
                    policy.Requirements.Add(new JWTREQUIREMENT("YES")));
            });
            services.AddSingleton<IAuthorizationHandler, JWTAuthorizationHandler>();

            #region convert to html into pdf
            //var wkHtmlToPdfPath = Path.Combine(Configuration.GetSection("Liblocation").Value);// (Directory.GetCurrentDirectory(), "libwkhtmltox.dll");
            //CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            //context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
            //services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            #endregion
            services.AddMvc();
            services.AddCors();
            //}
            //catch(Exception ex)
            //{

            //    var logPath = System.IO.Path.GetTempFileName();
            //    using (var writer = File.CreateText(Directory.GetCurrentDirectory() + "/log.txt"))
            //    {
            //        writer.WriteLine(ex.Message); //or .Write(), if you wish
            //    }
            //}
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{ FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ProductImage")), RequestPath = "/ProductImage" });
            app.UseStaticFiles(new StaticFileOptions
            { FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Document/Invoice")), RequestPath = "/Document/Invoice" });
            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
          
            app.UseMvc();


        }
    }
}
