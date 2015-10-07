// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfKernel.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace Stf
{
    using Utilities;

    /// <summary>
    /// The stf kernel.
    /// </summary>
    public class StfKernel
    {
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
    }
}
