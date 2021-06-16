using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using Nuve.Core;
using Nuve.Core.Storage;

namespace Nuve
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var hostBuilder = WebHost.CreateDefaultBuilder<Startup>(args);
            hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var optionsFile = Environment.ExpandEnvironmentVariables("%PROGRAMDATA%/Nuve/appsettings.json");
                config.AddJsonFile(optionsFile, optional: true, reloadOnChange: false);
            });

            hostBuilder.ConfigureLogging((h, l) =>
            {
                l.AddNLog();
            });

            hostBuilder.ConfigureServices((host, sservices) =>
            {
                var section = host.Configuration.GetSection(nameof(StorageSettings));
                sservices.Configure<StorageSettings>(section)
                .PostConfigure<StorageSettings>(q =>
                {
                    q.Location = Environment.ExpandEnvironmentVariables(q.Location);
                });

                sservices.AddSingleton<ILogger>(p => p.GetService<ILogger<Program>>());
                sservices.AddSingleton(new JsonSerializer());
                sservices.AddSingleton<IEntryReader, JsonWriterReader>();
                sservices.AddSingleton<IEntryWriter, JsonWriterWriter>();

                sservices.AddSingleton<FileStorage>();
                sservices.AddSingleton(q => new CachedStorage(q.GetService<FileStorage>()));
                sservices.AddSingleton<IVersionService>(q => q.GetService<CachedStorage>());
                sservices.AddSingleton<FileStorage, FileStorage>();
                sservices.AddSingleton<IStorage>(q => q.GetService<CachedStorage>());
            });

            return hostBuilder;
        }
    }
}
