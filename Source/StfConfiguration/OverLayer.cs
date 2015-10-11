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

            if (string.CompareOrdinal(core.SectionName, core.SectionName) != 0)
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

                this.OverLay(core.Sections[section.Key], layer.Sections[section.Key]);
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
        /// <returns>
        /// The <see cref="Section"/>.
        /// </returns>
        private Section AddSection(Section destination, Section source)
        {
            if (destination == null)
            {
                return source.MakeCopy();
            }

            if (source == null)
            {
                return destination.MakeCopy();
            }

            var newSection = new Section
                                 {
                                     SectionName = source.SectionName,
                                     DefaultSection = source.DefaultSection
                                 };
            if (newSection == null)
            {
                throw new Exception("Couldnt get a Section");
            }

            newSection.Sections.Add(source.SectionName, source.MakeCopy());
            destination.Sections.Add(newSection.SectionName, newSection);

            return newSection;
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
        /// <returns>
        /// The <see cref="Key"/>.
        /// </returns>
        private Key AddKey(Section destination, Key source)
        {
            if (destination == null)
            {
                return null;
            }

            if (source == null)
            {
                return null;
            }

            var newKey = new Key { KeyName = source.KeyName, KeyValue = source.KeyValue };

            destination.Keys.Add(newKey.KeyName, newKey);
            return newKey;
        }
    }
}
