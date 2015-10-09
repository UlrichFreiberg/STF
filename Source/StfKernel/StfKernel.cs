// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfKernel.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using Mir.Stf.Utilities;

namespace Mir.Stf
{
    /// <summary>
    /// The stf kernel.
    /// </summary>
    public class StfKernel : IDisposable
    {
        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfKernel"/> class.
        /// </summary>
        public StfKernel()
        {
            StfRoot = @"c:\temp\Stf"; // TODO: Skal komme et godt sted fra:-) Environment variable!

            StfLogDir = Path.Combine(StfRoot, @"Logs\");
            KernelLogger = new StfLogger { Configuration = { LogFileName = Path.Combine(StfLogDir, @"KernelLogger.html") } };
            PluginLoader = new StfPluginLoader(KernelLogger);
            StfConfiguration = new StfConfiguration(Path.Combine(StfRoot, @"Config\StfConfiguration.xml"));
            PluginLoader.RegisterInstance(typeof(StfConfiguration), StfConfiguration);
            PluginLoader.LoadStfPlugins(StfConfiguration.GetKeyValue("StfKernel.PluginPath"));
        }

        /// <summary>
        /// Gets or sets the stf root.
        /// </summary>
        public string StfRoot { get; set; }

        /// <summary>
        /// Gets the Stf logger.
        /// </summary>
        public StfLogger KernelLogger { get; private set; }

        /// <summary>
        /// Gets or sets the stf container.
        /// </summary>
        private StfPluginLoader PluginLoader { get; set; }

        /// <summary>
        /// Gets or sets the stf configuration.
        /// </summary>
        private StfConfiguration StfConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the stf log dir.
        /// </summary>
        private string StfLogDir { get; set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <typeparam name="T">
        /// The type to get
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        protected T Get<T>()
        {
            return PluginLoader.Get<T>();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (KernelLogger != null)
                    {
                        KernelLogger.LogDebug("Closing log files and disposing container");
                        KernelLogger.CloseLogFile();
                    }

                    if (PluginLoader != null)
                    {
                        PluginLoader.Dispose();
                    }
                }

                disposed = true;
            }
        }
    }
}
