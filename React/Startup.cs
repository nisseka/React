using React.Data;
using React.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;

namespace React
{
    public class Startup
    {
	public IConfiguration Configuration { get; }

	public Startup(IConfiguration configuration)
	{
	    Configuration = configuration;
	}

	// This method gets called by the runtime. Use this method to add services to the container.
	// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
	public void ConfigureServices(IServiceCollection services)
	{
	    services.AddDistributedMemoryCache();

	    services.AddSession(options =>
	    {
		options.IdleTimeout = TimeSpan.FromMinutes(10);
		options.Cookie.HttpOnly = true;
		options.Cookie.IsEssential = true;
	    });

	    services.AddDbContext<DatabaseDbContext>(
		options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

	    services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
		.AddDefaultUI()
		.AddDefaultTokenProviders()
		.AddEntityFrameworkStores<DatabaseDbContext>();

	    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
	    services.AddReact();

	    // Make sure a JS engine is registered, or you will get an error!
	    services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
	      .AddV8();

	    services.AddControllersWithViews();

	    services.AddRazorPages();
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
	    if (env.IsDevelopment())
	    {
		app.UseDeveloperExceptionPage();
	    }

	    app.UseReact(config =>
	    {
		// If you want to use server-side rendering of React components,
		// add all the necessary JavaScript files here. This includes
		// your components as well as all of their dependencies.
		// See http://reactjs.net/ for more information. Example:
		//config
		//  .AddScript("~/js/First.jsx")
		//  .AddScript("~/js/Second.jsx");

		// If you use an external build too (for example, Babel, Webpack,
		// Browserify or Gulp), you can improve performance by disabling
		// ReactJS.NET's version of Babel and loading the pre-transpiled
		// scripts. Example:
		//config
		//  .SetLoadBabel(false)
		//  .AddScriptWithoutTransform("~/js/bundle.server.js");
	    });

	    app.UseStaticFiles();

	    app.UseRouting();

	    app.UseAuthentication();
	    app.UseAuthorization();

	    app.UseSession();

	    app.UseEndpoints(endpoints =>
	    {
		endpoints.MapControllerRoute(
		    name: "default",
		    pattern: "{controller=Home}/{action=Index}/{id?}"
		    );

		endpoints.MapRazorPages();
	    });
	}
    }
}
