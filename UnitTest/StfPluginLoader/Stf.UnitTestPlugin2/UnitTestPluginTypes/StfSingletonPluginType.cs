// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfSingletonPluginType.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The stf singleton plugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Unittests.UnitTestPluginTypes
{
    /// <summary>
    /// The stf singleton plugin type.
    /// </summary>
    public class StfSingletonPluginType : IStfSingletonPluginType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfSingletonPluginType"/> class.
        /// </summary>
        public StfSingletonPluginType()
        {
            SingletonInteger = 1;
            SingletonBool = false;
        }

        /// <summary>
        /// Gets or sets the singleton integer.
        /// </summary>
        public int SingletonInteger { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether singleton bool.
        /// </summary>
        public bool SingletonBool { get; set; }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(IStfSingletonPluginType other)
        {
            if (other == null)
            {
                return false;
            }

            var retVal = other.SingletonBool == SingletonBool && other.SingletonInteger == SingletonInteger;

            return retVal;
        }
    }
}
