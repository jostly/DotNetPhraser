using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using Phraser.Services;
using System.Reflection;
using System.IO;

namespace Phraser
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
			services.AddMvc(config =>
			{
				config.RespectBrowserAcceptHeader = true;
				config.ReturnHttpNotAcceptable = true;
				config.OutputFormatters.Add(new PlainTextOutputFormatter());
			});

			var adjectives = ReadWordList("Phraser.adjectives.txt");
			var animals = ReadWordList("Phraser.animals.txt");

			services.AddSingleton<IPhraseService>(new PhraseService(adjectives, animals));

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseMvc();
		}

		private WordList ReadWordList(string resource)
		{
			var stream = typeof(Startup).GetTypeInfo().Assembly.GetManifestResourceStream(resource);
            using (var reader = new StreamReader(stream)) 
			{
				return new WordList(reader);
			}
		}
	}

	public class PlainTextOutputFormatter : TextOutputFormatter
	{
		public PlainTextOutputFormatter()
		{
			SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
			SupportedEncodings.Add(Encoding.UTF8);
			SupportedEncodings.Add(Encoding.Unicode);
		}

		public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
		{
			var response = context.HttpContext.Response;
			using (var writer = context.WriterFactory(response.Body, selectedEncoding))
			{
				return writer.WriteAsync(context.Object.ToString());
			}
		}
	}

}
