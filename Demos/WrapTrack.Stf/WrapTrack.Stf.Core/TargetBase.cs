// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TargetBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the TargetBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.Core
{
    using System;

    using Mir.Stf.Utilities;
    using Mir.Stf.Utilities.Interfaces;

    using WrapTrack.Stf.Core.Configuration;

    /// <summary>
    /// The target base.
    /// </summary>
    public abstract class TargetBase
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private StfConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetBase"/> class.
        /// </summary>
        protected TargetBase()
        {
            CurrentUser = Environment.UserName.ToLower();
        }

        /// <summary>
        /// Gets or sets the stf container.
        /// </summary>
        public IStfContainer StfContainer { get; set; }

        /// <summary>
        /// Gets or sets the Stf logger.
        /// </summary>
        public IStfLogger StfLogger { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the version info.
        /// </summary>
        public Version VersionInfo { get; protected set; }

        /// <summary>
        /// Gets the current environment.
        /// </summary>
        public string CurrentEnvironment
        {
            get
            {
                return StfConfiguration.DefaultEnvironment;
            }
        }

        /// <summary>
        /// Gets the stf configuration.
        /// </summary>
        protected StfConfiguration StfConfiguration
        {
            get
            {
                return configuration ?? (configuration = StfContainer.Get<StfConfiguration>());
            }
        }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        protected string CurrentUser { get; set; }

        /// <summary>
        /// The set config.
        /// </summary>
        /// <typeparam name="T">
        /// The configuration object that needs to have config values set
        /// </typeparam>
        /// <returns>
        /// The configuration object where properties has been set by the stf configuration
        /// </returns>
        protected T SetConfig<T>() where T : class, new()
        {
            if (StfConfiguration == null)
            {
                return default(T);
            }

            var configObject = new T();

            StfConfiguration.LoadUserConfiguration(configObject);

            return configObject;
        }

        /// <summary>
        /// The get user info.
        /// </summary>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        protected User GetUserInfo()
        {
            return GetUserInfo(CurrentUser);
        }

        /// <summary>
        /// The get user info.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        protected User GetUserInfo(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return default(User);
            }

            var queryFormat = "Users.{0}.{1}";
            var userNameKey = string.Format(queryFormat, userName, "Username");
            var passwordKey = string.Format(queryFormat, userName, "Password");
            string user;
            string password;

            try
            {
                user = StfConfiguration.GetConfigValue(userNameKey);
                password = StfConfiguration.GetConfigValue(passwordKey);
            }
            catch (Exception exception)
            {
                StfLogger.LogDebug(string.Format("Did not find a user for {0}. Errormessage: {1}", userNameKey, exception.Message));
                throw;
            }
            
            return new User { UserName = user, Password = password };
        }
    }
}
