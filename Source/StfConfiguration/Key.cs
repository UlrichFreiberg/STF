// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Key.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the Key type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The configuration key. Holding a record of originating config file
    /// </summary>
    public class Key
    {
        /// <summary>
        /// Gets or sets the KeyName of the key.
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// Gets or sets the value of the key.
        /// </summary>
        public string KeyValue { get; set; }

        /// <summary>
        /// Gets or sets the value of the source config file 
        /// that originally had this entry. 
        /// That is needed as we overlay config files.
        /// </summary>
        public string SourceConfigFile { get; set; }

        /// <summary>
        /// Override of ToString.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var txt = $"KeyName:{this.KeyName}, KeyValue:{this.KeyValue}";
            return txt;
        }
    }
}
