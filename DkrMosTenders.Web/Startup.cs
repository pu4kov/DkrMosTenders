using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DkrMosTenders.Web.Models;
using AutoMapper;
using DkrMosTenders.Model;

namespace DkrMosTenders.Web
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
            services.AddMvc();

            services.AddDbContext<TendersContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("TendersContext")));

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tender, TenderSummaryViewModel>()
                    .ForMember(vm => vm.Number,
                        o => o.MapFrom(t => t.DkrNumber))
                    .ForMember(vm => vm.Price,
                        o => o.MapFrom(t => t.MaxPrice))
                    .ForMember(vm => vm.Districts,
                        o => o.MapFrom(t => t.Objects
                            .GroupBy(tt => tt.Building.District)
                            .Select(d => d.Key.ShortName)))
                    .ForMember(vm => vm.Addresses,
                        o => o.MapFrom(t => t.Objects
                            .Select(b => b.Building.Address)))
                    .ForMember(vm => vm.Price,
                        o => o.MapFrom(t => t.MaxPrice));
            });
            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Tenders/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Tenders}/{action=Index}");
            });
        }
    }
}
