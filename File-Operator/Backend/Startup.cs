using Backend.Option_models;
using Microsoft.Extensions.Configuration;
using System;

namespace Backend
{
    /// <summary>
    /// Performs operations that prepare everything that would be needed by the application to run.
    /// </summary>
    static class Startup
    {
        /// <summary>
        /// Reads the configuration files, loads their data in application memory, sets any default settings and sets up shutdown hooks.
        /// </summary>
        internal static void InitConfig()
        {
            var databaseParameters = DatabaseParameters.instance;

            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
            configuration.GetSection(nameof(DatabaseParameters)).Bind(databaseParameters);

            SetupShutdownHooks();
        }

        /// <summary>
        /// Sets up shutdown hooks, i.e. events to trigger when the application is shutting down.
        /// </summary>
        static void SetupShutdownHooks()
        {
            var domain = AppDomain.CurrentDomain;
            domain.UnhandledException += CleanResources;
            domain.ProcessExit += CleanResources;
            domain.DomainUnload += CleanResources;
        }

        /// <summary>
        /// Cleans (disposes, closes, finalises and etc.) all resources that must be taken care of in the
        /// event of an unexpected process shutdown.
        /// </summary>
        /// <param name="sender">Unused parameter.</param>
        /// <param name="e">Unused parameter.</param>
        static void CleanResources(object sender, EventArgs e)
        {

        }
    }
}
