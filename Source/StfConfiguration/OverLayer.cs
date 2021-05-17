// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OverLayer.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the OverLayer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The over layer of configuration trees.
    /// </summary>
    public class OverLayer
    {
        /// <summary>
        /// Two sections gets merged. 
        /// The core is extended with nodes/leaves from layer. 
        /// New nodes are added, 
        /// Keys gets values from layer
        /// </summary>
        /// <param name="core">
        /// The core - the destination for applying the layer.
        /// </param>
        /// <param name="layer">
        /// The layer to apply.
        /// </param>
        /// <returns>
        /// The <see cref="Section"/>.
        /// </returns>
        public Section OverLay(Section core, Section layer)
        {
            if (layer == null)
            {
                return core;
            }

            if (core == null)
            {
                return layer.MakeCopy();
            }

            if (string.CompareOrdinal(core.SectionName, layer.SectionName) != 0)
            {
                throw new Exception("Internal Error: SectionNames are not alike");
            }

            if (!string.IsNullOrEmpty(layer.DefaultSection))
            {
                core.DefaultSection = layer.DefaultSection;
            }

            foreach (var section in layer.Sections)
            {
                if (!core.Sections.ContainsKey(section.Key))
                {
                    AddSection(core, layer.Sections[section.Key]);
                }
                else
                {
                    OverLay(core.Sections[section.Key], layer.Sections[section.Key]);
                }
            }

            foreach (var key in layer.Keys)
            {
                if (!core.Keys.ContainsKey(key.Key))
                {
                    AddKey(core, layer.Keys[key.Key]);
                }

                core.Keys[key.Key].KeyValue = layer.Keys[key.Key].KeyValue;
                core.Keys[key.Key].SourceConfigFile = layer.Keys[key.Key].SourceConfigFile;
            }

            return core;
        }

        /// <summary>
        /// Adds a section from an over layered (new) section tree, 
        /// to the resulting tree
        /// </summary>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        private void AddSection(Section destination, Section source)
        {
            if (destination == null)
            {
                source.MakeCopy();
                return;
            }

            if (source == null)
            {
                destination.MakeCopy();
                return;
            }

            destination.Sections.Add(source.SectionName, source.MakeCopy());
            destination.Sections[source.SectionName].DefaultSection = source.DefaultSection;
        }

        /// <summary>
        /// Adds a key from an over layered (new) section tree, 
        /// to the resulting tree
        /// </summary>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        private void AddKey(Section destination, Key source)
        {
            if (destination == null || source == null)
            {
                return;
            }

            var newKey = new Key { KeyName = source.KeyName, KeyValue = source.KeyValue };

            destination.Keys.Add(newKey.KeyName, newKey);
        }
    }
}
