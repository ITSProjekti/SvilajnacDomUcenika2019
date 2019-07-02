using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using DomUcenikaSvilajnac.DAL.Context;
using AutoMapper;
using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.DAL.RepoPattern;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Reflection;

namespace DomUcenikaSvilajnac
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
            var physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            var manifestEmbeddedProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly());

            var compositeProvider = new CompositeFileProvider(physicalProvider, manifestEmbeddedProvider);
            //Linija za zaobilazenje Cross origin http request zastite koju imaju browseri
            services.AddCors();
      
            //liniju koju smo dodali kako bismo ispisali listu postanskih brojeva u opstini kontroleru
            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });

            services.AddAutoMapper();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<UcenikContext>(options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocaldb;Initial Catalog=DomUcenikaSvilajnac31;Integrated Security=True;Connect Timeout=30"));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddSingleton(compositeProvider);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Ovde se postavljaju kofiguracije za dozvole u proboju zastite CORS, dozvoljeno svima da pristupe web api
            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());

            app.UseStaticFiles();

            

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")),
                RequestPath = "/Files"
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot") ),
                RequestPath = "/Files"
            });
            app.UseMvc();
            
            
        }
    }
}
